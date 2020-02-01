using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    private Movement truck;
    private Movement bike;
    private Inputs input;

    public void Research()
    {
        truck = GameObject.Find("Truck").GetComponent<Movement>();
        bike = GameObject.Find("Bike").GetComponent<Movement>();
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        input = GetComponent<Inputs>();
        Research();
    }

    public void SetInTruck()
    {
        truck.SetInput(input);
        transform.position = truck.transform.position;
        truck.transform.SetParent(transform);
        bike.transform.SetParent(null);
    }

    public void SetOnBike()
    {
        bike.SetInput(input);
        transform.position = bike.transform.position;
        bike.transform.SetParent(transform);
        truck.transform.SetParent(null);
    }
}

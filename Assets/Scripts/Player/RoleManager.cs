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
        GameObject truckObject = GameObject.Find("Truck");
        GameObject bikeObject = GameObject.Find("Bike");

        if (truckObject == null || bikeObject == null)
        {
            Debug.LogError("Truck or Bike is null");
        }

        truck = truckObject.GetComponent<Movement>();
        bike = bikeObject.GetComponent<Movement>();
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

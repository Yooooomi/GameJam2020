using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    public Vector3 arrowOffset;

    private Movement truck;
    private Movement bike;
    private Inputs input;

    private Movement currentVehicle;
    private ArrowManager arrowManager;

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
        arrowManager = GetComponent<ArrowManager>();
    }

    private void Update()
    {
        if (currentVehicle)
        {
            arrowManager.arrowContainer.transform.position = currentVehicle.transform.position + arrowOffset;
        }
    }

    public void ClearVehicle()
    {
        if (currentVehicle != null)
        {
            currentVehicle.SetInput(null);
            currentVehicle.transform.SetParent(null);
        }
    }

    public void SetInTruck()
    {
        currentVehicle = truck;
        truck.SetInput(input);
        transform.position = truck.transform.position;
        truck.transform.SetParent(transform);
    }

    public void SetOnBike()
    {
        currentVehicle = bike;
        bike.SetInput(input);
        transform.position = bike.transform.position;
        bike.transform.SetParent(transform);
    }
}

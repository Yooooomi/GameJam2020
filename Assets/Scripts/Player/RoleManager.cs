using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    private Movement truck;
    private Movement bike;
    private Inputs input;

    private Movement currentVehicle;
    private PlayerStats stats;

    public void Research()
    {
        GameObject truckObject = GameObject.Find("Truck");
        GameObject bikeObject = GameObject.Find("Bike");
        stats = GetComponent<PlayerStats>();

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
        truck.GetComponent<Truck>().SetCdrReduction(stats.truckCooldownReduction);
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

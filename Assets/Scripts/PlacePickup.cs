using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePickup : MonoBehaviour
{
    [SerializeField] private Pickups.PickupType pickupType;

    public Pickups.PickupType GetPickupType()
    {
        return pickupType;
    }

    public void DoFunctionPlace()
    {
        gameObject.SetActive(false);
        Debug.Log("DoFunctionPlace random function");
    }
}

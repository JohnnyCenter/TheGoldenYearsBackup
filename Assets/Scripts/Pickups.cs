using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PickupType pickupType;
    public bool canAddToInventory;

    public enum PickupType
    {
        //ADD ALL PICKUPS HERE
        painting_d1,
        painting_d2,
        coffeeMug_d3,
        painting_d3,
        painting_d4
    }

    public PickupType GetPickupType()
    {
        return pickupType;
    }
}

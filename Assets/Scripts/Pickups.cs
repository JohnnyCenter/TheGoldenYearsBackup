using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PickupType pickupType;
    public bool canAddToInventory;

    public enum PickupType
    {
        greenBlock_d1,
        key_d2,
        coffeeMug_d3
    }

    public PickupType GetPickupType()
    {
        return pickupType;
    }

    public bool CheckIfInventory()
    {
        return canAddToInventory;
    }
}

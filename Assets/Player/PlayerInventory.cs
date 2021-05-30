using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public List<Pickups.PickupType> pickupList;
    [SerializeField] PlayerRaycast p_ray;
    [SerializeField] Transform p_rightHand;
    [HideInInspector] public GameObject newHandObj;
    public PlacePickup placePickup;

    private void Awake()
    {
        pickupList = new List<Pickups.PickupType>();
    }

    public void AddPickup(Pickups.PickupType pickupType)
    {
        pickupList.Add(pickupType);
    }
    
    public void RemovePickup()
    {
        pickupList.Clear();
    }

    public bool ContainsPickup(Pickups.PickupType pickupType)
    {
        return pickupList.Contains(pickupType);

    }

    public void OnTriggerPickup()
    {
        Pickups pickup = p_ray.pickup_obj.GetComponent<Pickups>();
        if (pickup != null)
        {
            AddPickup(pickup.GetPickupType());
            //GameObject newHandObj = Instantiate(pickup.gameObject, p_rightHand.position, pickup.transform.rotation);
            p_ray.pickup_obj.transform.position = p_rightHand.position;
            placePickup = p_ray.GetComponent<PlacePickup>();

            p_ray.pickup_obj.transform.SetParent(p_rightHand);
            p_ray.pickup_obj.GetComponent<Collider>().enabled = false;
        }
    }

    public void OnTriggerPlaceObject()
    {
        if(placePickup != null)
        {
            Debug.Log("placePickup is not null");
            if (ContainsPickup(placePickup.GetPickupType()))
            {
                Debug.Log("Should call the funtction in PlayerRay");
                p_ray.PlaceObject();
                placePickup.DoFunctionPlace();
            }
        }
    }
}

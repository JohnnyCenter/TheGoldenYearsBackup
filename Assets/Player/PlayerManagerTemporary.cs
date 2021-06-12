using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerTemporary : MonoBehaviour
{
    [SerializeField] Movement p_movement;
    [SerializeField] PlayerLook p_look;
    [SerializeField] PlayerRaycast p_raycast;

    public List<GameObject> objectsSeen = new List<GameObject>();
    [SerializeField] GameObject canvasText_interact;

    private void Update()
    {
        if (objectsSeen.Count > 0)
        {
            if (!p_look.canInspectObj)
            {
                canvasText_interact.SetActive(true);
            }
            else
            {
                canvasText_interact.SetActive(false);
            }
        } else
        {
            canvasText_interact.SetActive(false);
        }
    }

    public void DisablePlayerAll()
    {
        p_look.TurnOffMouseControls();
        p_raycast.rayIsDisabled = true;
        p_movement.enabled = false;
    }

    public void DisablePlayerMoveLook()
    {
        p_look.TurnOffMouseControls();
        p_movement.enabled = false;
    }
    public void EnablePlayerRay()
    {
        p_raycast.rayIsDisabled = false;
    }
    public void EnablePlayerAll()
    {
        p_look.TurnOnMouseControls();
        p_movement.enabled = true;
        p_raycast.rayIsDisabled = false;

    }
}

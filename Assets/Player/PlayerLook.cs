using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] float sensX = 8f;
    [SerializeField] float sensY = .5f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    [SerializeField] 
    bool isDisabled;
    //[HideInInspector] 
    public bool canInspectObj;
    [HideInInspector] public GameObject inspectObj;
    Vector3 targetRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (!isDisabled)
        {
            transform.Rotate(Vector3.up, mouseX * Time.fixedDeltaTime);

            playerCamera.eulerAngles = targetRotation;
        }
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;

        if (canInspectObj)
        {
            inspectObj.transform.Rotate(Vector3.up, mouseX * Time.fixedDeltaTime);
        }
    }

    public void RecieveInput (Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensX;
        mouseY = mouseInput.y * sensY;
    }

    public void TurnOnMouseControls()
    {

        isDisabled = false;
        canInspectObj = false;
    }

    public void TurnOffMouseControls()
    {
        isDisabled = true;
    }
}

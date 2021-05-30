using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
#pragma warning disable 649

    Vector2 horizontalInput;
    [SerializeField] CharacterController cont;
    [SerializeField] float speed = 8f;

    private void Update()
    {
        Vector3 horizontalVel = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        cont.Move(horizontalVel * Time.deltaTime);
    }

    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
}
 
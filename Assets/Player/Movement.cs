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
        SFXManager.PlaySFX(SFXManager.Sound.SFX_FootstepsParquet, transform.position);
        /*if (moving) //Bool for checking movement and surface needed.
        {
            if (!carpet)
            {
                SFXManager.PlaySFX(SFXManager.Sound.SFX_FootstepsParquet);
            }
            else
            {
                SFXManager.PlaySFX(SFXManager.Sound.SFX_FootstepsCarpet);
            }
        }*/
    }

    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
}
 
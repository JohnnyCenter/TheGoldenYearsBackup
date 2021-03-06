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
        if (!Mathf.Approximately(cont.velocity.magnitude, 0))
        {
            SoundManager.PlaySFX(SoundManager.Sound.SFX_FootstepsParquet, transform.position);
        }
        //SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, 1);


        /*if (moving) //Bool for checking movement and surface needed.
        {
            if (!carpet)
            {
                SoundManager.PlaySFX(SoundManager.Sound.SFX_FootstepsParquet);
            }
            else
            {
                SoundManager.PlaySFX(SoundManager.Sound.SFX_FootstepsCarpet);
            }
        }*/
    }

    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] Movement movement;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerRaycast playerRaycast;
    [SerializeField] PauseMenu pauseMenu;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMove;

    Vector2 horizontalInput;
    Vector3 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMove = controls.GroundMovement;

        groundMove.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMove.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMove.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        groundMove.Interact.performed += _ => playerRaycast.InteractPressed();
        groundMove.RightClick.performed += _ => playerRaycast.RightClickPressed();

        groundMove.Pause.performed += _ => pauseMenu.PauseGame();
    }

    private void Update()
    {
        movement.RecieveInput(horizontalInput);
        playerLook.RecieveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}

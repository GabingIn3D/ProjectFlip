using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInputManager : MonoBehaviour
{
    [SerializeField] MovePlayer movement;
    [SerializeField] MouseLook cameraLook;
    DefaultControls inputActions;
    DefaultControls.PlayerActions groundMovement;

    Vector2 horizontalInput;
    Vector2 lookInput;

    private void Awake()
    {
        inputActions = new DefaultControls();
        groundMovement = inputActions.Player;
        groundMovement.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.LookX.performed += ctx => lookInput.x = ctx.ReadValue<float>();
        groundMovement.LookY.performed += ctx => lookInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        cameraLook.ReceiveInput(lookInput);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}

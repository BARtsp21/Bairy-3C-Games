using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector2> OnMoveInput;

    public Action<bool> OnSprintInput;

    public Action OnJumpInput;

    public Action OnClimbInput;

    public Action OnCancelClimb;
    private void Update()
    {
        CheckJumpInput();
        CheckMovementInput();
        CheckCrouchInput();
        CheckClimbInput();
        CheckChangePOVInput();
        CheckGlideInput();
        CheckPunchInput();
        CheckCancelInput();
        CheckMainMenuInput();
        CheckSprintInput();
    }
    private void CheckJumpInput()
    {
        bool IsPressJumpInput = Input.GetKeyDown(KeyCode.Space);
        if (IsPressJumpInput)
        {
            OnJumpInput();
            
        }
    }

    private void CheckMovementInput()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 InputAxis = new Vector2(horizontalAxis, verticalAxis);
        if (OnMoveInput != null)
        {
            OnMoveInput(InputAxis);
        }
    }

    private void CheckCrouchInput()
    {
        bool IsPressCrouchInput = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
        if (IsPressCrouchInput)
        {
            Debug.Log("Crouch");
        }
    }

    private void CheckChangePOVInput()
    {
        bool IsPressChangePOVInput = Input.GetKeyDown(KeyCode.Q);
        if (IsPressChangePOVInput)
        {
            Debug.Log("Change POV");
        }
    }

    private void CheckClimbInput()
    {
        bool IsPressClimbInput = Input.GetKeyDown(KeyCode.E);
        if (IsPressClimbInput)
        {
            OnClimbInput();
        }
    }

    private void CheckGlideInput()
    {
        bool IsPressGlideInput = Input.GetKeyDown(KeyCode.G);
        if (IsPressGlideInput)
        {
            Debug.Log("Glide");
        }
    }

    private void CheckCancelInput()
    {
        bool IsPressCancelInput = Input.GetKeyDown(KeyCode.C);
        if (IsPressCancelInput)
        {
            if (OnCancelClimb != null)
            {
                OnCancelClimb();
            }
        }
    }

    private void CheckPunchInput()
    {
        bool IsPressPunchInput = Input.GetKeyDown(KeyCode.Mouse0);
        if (IsPressPunchInput)
        {
            Debug.Log("Punch");
        }
    }

    private void CheckMainMenuInput()
    {
        bool IsMainMenuInput = Input.GetKeyDown(KeyCode.Escape);
        if (IsMainMenuInput)
        {
            Debug.Log("Main Menu");
        }
    }

    private void CheckSprintInput()
    {
        bool IsHoldSprintInput = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (IsHoldSprintInput)
        {
            OnSprintInput(true);
            
        }
        else
        {
            OnSprintInput(false);
            
        }
    }

    
}

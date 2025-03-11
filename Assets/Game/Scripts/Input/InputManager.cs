using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action OnChangePOV;
    public Action<Vector2> OnMoveInput;

    public Action<bool> OnSprintInput;

    public Action OnJumpInput;

    public Action OnClimbInput;

    public Action OnCancelClimb;

    public Action OnCrouchInput;

    public Action OnGlideInput;

    public Action OnCancelGlide;

    public Action OnPunchInput;

    public Action OnMainMenuInput;
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
            OnCrouchInput();
        }
    }

    private void CheckChangePOVInput()
    {
        bool IsPressChangePOVInput = Input.GetKeyDown(KeyCode.Q);
        if (IsPressChangePOVInput)
        {
            if (OnChangePOV != null)
            {
                OnChangePOV();
            }
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
            if (OnGlideInput != null)
            {
                OnGlideInput();
            }
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
            if (OnCancelClimb != null)
            {
                OnCancelGlide();
            }
        }
    }

    private void CheckPunchInput()
    {
        bool IsPressPunchInput = Input.GetKeyDown(KeyCode.Mouse0);
        if (IsPressPunchInput)
        {
         OnPunchInput();
           
        }
    }

    private void CheckMainMenuInput()
    {
        bool IsMainMenuInput = Input.GetKeyDown(KeyCode.Escape);
        if (IsMainMenuInput)
        {
            if (OnMainMenuInput != null)
            {
                OnMainMenuInput();
            }
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

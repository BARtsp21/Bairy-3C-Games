using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
   private float _walkSpeed;

   [SerializeField]
   private float _sprintSpeed;
   [SerializeField]
   private float _walkSprintTransition;

   [SerializeField]
   private float _Jumpforce;
    [SerializeField]
   private Transform _groundDetector;
   
    [SerializeField]
   private float _detectorRadius;
   
    [SerializeField]
   private LayerMask _groundLayer;
  
   [SerializeField]
   private Vector3 _UpperStepOffset;

   [SerializeField]
   private float _stepCheckerDistance;
    [SerializeField]
   private float _stepForce;

   [SerializeField]

    private Transform _climbDetector;

    [SerializeField]

    private float _climbCheckDistance;

    [SerializeField]

    private LayerMask _climbableLayer;

    [SerializeField]

    private Vector3 _climbOffset;

    [SerializeField]
    private float _climbSpeed;



    [SerializeField]
   private InputManager _input;

      [SerializeField]

   private float _rotaionSmoothTime = 0.1f;

   private Rigidbody _rigidbody;

   private float _rotationSmoothVelocity;

   private float _speed;

   private bool _IsGrounded;

   private PlayerStance _playerStance;

    private void Awake()
    {
       _rigidbody = GetComponent<Rigidbody>();
       _speed = _walkSpeed;
       _playerStance = PlayerStance.Stand;
       
        
    }

    private void Start()
    {
        _input.OnMoveInput += Move;
        _input.OnSprintInput += Sprint;
        _input.OnJumpInput += Jump;
        _input.OnClimbInput += StartClimb;
        _input.OnCancelClimb += CancelClimb;
        
    }

    private void OnDestroy()
    {
        _input.OnMoveInput -= Move; 
        _input.OnSprintInput -= Sprint;
        _input.OnJumpInput -= Jump;   
        _input.OnClimbInput -= StartClimb; 
        _input.OnCancelClimb -= CancelClimb; 
    }

    private void Update()
    {
        CheckIsGrounded();
        CheckStep();
    }

    private void Move(Vector2 axisDirection)
   {
        Vector3 movementDirection = Vector3.zero;
        bool IsPlayerStanding = _playerStance == PlayerStance.Stand;
        bool IsPlayerClimbing = _playerStance == PlayerStance.Climb;
        if (IsPlayerStanding)
        {
            if (axisDirection.magnitude >= 0.1)
            {
                float rotationAngle = Mathf.Atan2(axisDirection.x, axisDirection.y) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _rotationSmoothVelocity, _rotaionSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                _rigidbody.AddForce(movementDirection * Time.deltaTime * _speed);
            }
        }
        else if (IsPlayerClimbing)
        {
            Vector3 horizontal = axisDirection.x * transform.right;
            Vector3 vertical = axisDirection.y * transform.up;
            movementDirection = horizontal + vertical;
            _rigidbody.AddForce(movementDirection *Time.deltaTime * _climbSpeed);
        }
   }

   private void Sprint(bool isSprint)
   {
        if (isSprint)
        {
            if (_speed < _sprintSpeed)
            {
             _speed = _speed + _walkSprintTransition * Time.deltaTime;
            }
        }
        else
        {
            if (_speed > _walkSpeed)
            {
            _speed = _speed - _walkSprintTransition * Time.deltaTime;
            }
        }
   }

   private void Jump()
   {
     if (_IsGrounded)
     {
            Vector3 JumpDirection = Vector3.up;
            _rigidbody.AddForce(JumpDirection * _Jumpforce * Time.deltaTime);
     }  
        
   }

   private void CheckIsGrounded()
   {
     _IsGrounded = Physics.CheckSphere(_groundDetector.position, _detectorRadius, _groundLayer);
   }

   private void CheckStep()
   {
        bool IsHitLowerStep = Physics.Raycast(_groundDetector.position, transform.forward, _stepCheckerDistance);
        bool IsHitUpperStep = Physics.Raycast(_groundDetector.position + _UpperStepOffset, transform.forward, _stepCheckerDistance);

        if (IsHitLowerStep && !IsHitUpperStep)
        {
            _rigidbody.AddForce(0, _stepForce * Time.deltaTime, 0);
        }
   }
   private void StartClimb()
   {
        bool IsInFrontofClimbingWall = Physics.Raycast(_climbDetector.position, transform.forward, out RaycastHit hit, _climbCheckDistance, _climbableLayer);
        bool IsNotClimbing = _playerStance != PlayerStance.Climb;
        if (IsInFrontofClimbingWall && _IsGrounded && IsNotClimbing)
        {
            Vector3 offset = (transform.forward * _climbOffset.z) + (Vector3.up * _climbOffset.y);
            transform.position = hit.point - offset;
            _playerStance = PlayerStance.Climb;
            _rigidbody.useGravity = false;
        }
   }

   private void CancelClimb()
   {
    if (_playerStance == PlayerStance.Climb)
    {
        _playerStance = PlayerStance.Stand;
        _rigidbody.useGravity = true;
        transform.position -= transform.forward *1f;
    }
   }

   }

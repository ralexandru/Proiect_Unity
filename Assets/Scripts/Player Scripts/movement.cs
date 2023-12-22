using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(100, 100);
    [SerializeField]
    private joystick MoveJoystick;
    [SerializeField]
    private joystick RotateJoystick;
    [SerializeField]
    private CharacterController Player;
    [SerializeField]
    private Transform playerRotationTransform;
    [SerializeField]
    private Transform cameraTransform;
    public GameObject enterPin;

    private Finger MoveFinger;
    private Finger RotateFinger;

    private Vector2 MoveAmount = Vector2.zero;
    private Vector2 RotateAmount = Vector2.zero;

    private float gravity = 9.8f;
    private float verticalVelocity = 0f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += Touch_onFingerDown;
        ETouch.Touch.onFingerUp += Touch_onFingerUp;
        ETouch.Touch.onFingerMove += Touch_onFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= Touch_onFingerDown;
        ETouch.Touch.onFingerUp -= Touch_onFingerUp;
        ETouch.Touch.onFingerMove -= Touch_onFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();
        HandleRotation();
    }

    private void HandleGravity()
    {
        if (!Player.isGrounded)
        {
            // Apply gravity manually
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            // Reset vertical velocity when grounded
            verticalVelocity = -gravity * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = MoveAmount.x * 0.5f;
        float verticalInput = MoveAmount.y * 0.5f;

        Vector3 movement = playerRotationTransform.TransformDirection(new Vector3(horizontalInput, verticalVelocity, verticalInput));
        Player.Move(movement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float rotationSpeed = 0.5f; // Adjust the rotation speed as needed
        Vector3 rotation = new Vector3(0, RotateAmount.x * rotationSpeed, 0);
        playerRotationTransform.Rotate(rotation);

        // Apply vertical rotation to the camera
        float currentVerticalRotation = cameraTransform.localEulerAngles.x - RotateAmount.y * rotationSpeed;
        float clampedVerticalRotation = Mathf.Clamp(currentVerticalRotation, -90, 360);
        cameraTransform.localEulerAngles = new Vector3(clampedVerticalRotation, 0, 0);
    }

    private void Touch_onFingerMove(Finger MovedFinger)
    {
        Vector2 currentTouchPosition = MovedFinger.screenPosition;
        Debug.Log("Finger Down or Move or Up Event");
        if (MovedFinger == MoveFinger)
        {
            UpdateMoveJoystick(currentTouchPosition, MovedFinger);
        }
        else if (MovedFinger == RotateFinger)
        {
            UpdateRotateJoystick(currentTouchPosition, MovedFinger);
        }
    }

    private void UpdateMoveJoystick(Vector2 touchPosition, Finger MovedFinger)
    {
        MoveAmount = (touchPosition - MoveJoystick.RectTransform.anchoredPosition) / (JoystickSize.x / 2f);
        if (MovedFinger == MoveFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;
            if (Vector2.Distance(currentTouch.screenPosition, MoveJoystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - MoveJoystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - MoveJoystick.RectTransform.anchoredPosition;
            }
            MoveJoystick.Knob.anchoredPosition = knobPosition;
        }
    }

    private void UpdateRotateJoystick(Vector2 touchPosition, Finger MovedFinger)
    {
        RotateAmount = (touchPosition - RotateJoystick.RectTransform.anchoredPosition) / (JoystickSize.x / 2f);
        Vector2 knobPosition;
        float maxMovement = JoystickSize.x / 2f;
        ETouch.Touch currentTouch = MovedFinger.currentTouch;
        if (Vector2.Distance(currentTouch.screenPosition, RotateJoystick.RectTransform.anchoredPosition) > maxMovement)
        {
            knobPosition = (currentTouch.screenPosition - RotateJoystick.RectTransform.anchoredPosition).normalized * maxMovement;
        }
        else
        {
            knobPosition = currentTouch.screenPosition - RotateJoystick.RectTransform.anchoredPosition;
        }
        RotateJoystick.Knob.anchoredPosition = knobPosition;
    }

    private void Touch_onFingerUp(Finger LostFinger)
    {
        Debug.Log("Finger Down or Move or Up Event");
        if (LostFinger == MoveFinger)
        {
            MoveFinger = null;
            MoveJoystick.Knob.anchoredPosition = Vector2.zero;
            MoveJoystick.gameObject.SetActive(false);
            MoveAmount = Vector2.zero;
        }
        else if (LostFinger == RotateFinger)
        {
            RotateFinger = null;
            RotateJoystick.Knob.anchoredPosition = Vector2.zero;
            RotateJoystick.gameObject.SetActive(false);
            RotateAmount = Vector2.zero;
        }
    }

    private void Touch_onFingerDown(Finger TouchedFinger)
    {
        if (!enterPin.activeInHierarchy)
        {
            if (TouchedFinger.screenPosition.x <= Screen.width / 2f)
            {
                MoveFinger = TouchedFinger;
                MoveAmount = Vector2.zero;
                MoveJoystick.gameObject.SetActive(true);
                MoveJoystick.RectTransform.sizeDelta = JoystickSize;
                MoveJoystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            }
            else
            {
                RotateFinger = TouchedFinger;
                RotateAmount = Vector2.zero;
                RotateJoystick.gameObject.SetActive(true);
                RotateJoystick.RectTransform.sizeDelta = JoystickSize;
                RotateJoystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            }
        }
    }

    private Vector2 ClampStartPosition(Vector2 position)
    {
        if (position.x < JoystickSize.x / 2)
        {
            position.x = JoystickSize.x / 2;
        }
        if (position.y < JoystickSize.y / 2)
        {
            position.y = JoystickSize.y / 2;
        }
        else if (position.y > Screen.height - JoystickSize.y / 2)
        {
            position.y = Screen.height - JoystickSize.y / 2;
        }
        return position;
    }
}

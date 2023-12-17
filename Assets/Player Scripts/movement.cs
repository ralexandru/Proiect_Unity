using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
public class movement : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(100, 100);
    [SerializeField]
    private joystick Joystick;
    [SerializeField]
    private NavMeshAgent Player;

    private Finger MovementFinger;
    private Vector2 MovementAmount = new Vector2(10f,20f);

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

    private void Touch_onFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;
            if(Vector2.Distance(currentTouch.screenPosition, Joystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
            Debug.Log(MovementAmount);
        }
    }

    private void Touch_onFingerUp(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void Touch_onFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 position)
    {
        if (position.x < JoystickSize.x / 2)
        {
            position.x = JoystickSize.x / 2;
        }
        if(position.y < JoystickSize.y / 2)
        {
            position.y = JoystickSize.y / 2;
        }
        else if (position.y > Screen.height - JoystickSize.y / 2)
        {
            position.y = Screen.height - JoystickSize.y / 2;
        }
        return position;
    }

    private void Update()
    {
        Vector3 scaledMovement = Player.speed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);
        Player.transform.LookAt(Player.transform.position + scaledMovement, Vector3.up);
        Player.Move(scaledMovement);
    }
}

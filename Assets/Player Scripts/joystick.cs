using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour
{
    [HideInInspector]
    public RectTransform RectTransform;
    public RectTransform Knob;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotator : MonoBehaviour
{
    public float rotationSpeed = 0.05f;
    public float rotationAngle = 45f;

    void Update()
    {
        // Roteste camera de la stanga la dreapta si inapoi continuu
        float rotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
        transform.rotation = Quaternion.Euler(0f, rotation+180f, 0f);
    }
}

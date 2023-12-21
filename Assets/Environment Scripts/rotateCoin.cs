using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCoin : MonoBehaviour
{
    // Set the rotation speed in degrees per second
    public float rotationSpeed = 30f;

    void Update()
    {
        // Call the RotateObject function in the Update method or as needed
        RotateObject();
    }

    void RotateObject()
    {
        // Rotate the object around its up axis (Y-axis) based on the rotationSpeed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Call your custom method here
            YourCustomMethod();
        }
    }

    void YourCustomMethod()
    {
        // Your custom logic when the player's collider touches this object's collider
        gameObject.SetActive(false);
        levelOptions2.coinsFound += 1;
    }
}

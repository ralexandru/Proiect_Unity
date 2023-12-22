using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCoin : MonoBehaviour
{
    public SpiderController spiderController;
    // Rotation speed in degrees per second
    public float rotationSpeed = 30f;

    void Update()
    {
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
            // If player collided with the coin, collect coin
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        gameObject.SetActive(false);
        levelOptions2.coinsFound += 1;
        spiderController.IncreaseSpiderSpeed(1.05f);
    }
}

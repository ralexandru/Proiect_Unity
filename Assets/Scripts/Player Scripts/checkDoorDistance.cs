using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDoorDistance : MonoBehaviour
{
    public Transform player;  
    public Transform door; 
    public Transform portal; 
    public float activationDistance = 3f; 

    public GameObject buttonCanvasGroup;
    public GameObject portalCanvasGroup;
    private void Start()
    {
        // Initially, hide the buttons
        HideButton(buttonCanvasGroup);
        HideButton(portalCanvasGroup);

    }

    private void Update()
    {
        // Check the distance between the player and the door
        float distanceToDoor = Vector3.Distance(player.position, door.position);
        // Check the distance between the player and the portal
        float distanceToPortal = Vector3.Distance(player.position, portal.position);
        // Show or hide the button based on the distance
        if (distanceToDoor <= activationDistance)
        {
            ShowButton(buttonCanvasGroup);
        }
        else
        {
            HideButton(buttonCanvasGroup);
        }
        if (distanceToPortal <= activationDistance)
        {
            ShowButton(portalCanvasGroup);
        }
        else
        {
            HideButton(portalCanvasGroup);
        }
    }

    private void ShowButton(GameObject canvas)
    {
        canvas.SetActive(true);
    }

    private void HideButton(GameObject canvas)
    {
        canvas.SetActive(false);
    }
}

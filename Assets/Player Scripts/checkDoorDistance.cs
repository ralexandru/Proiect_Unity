using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDoorDistance : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Transform door; // Reference to the door's transform
    public Transform portal; // Reference to the portal's transform
    public float activationDistance = 3f;  // The distance at which the button becomes visible

    public GameObject buttonCanvasGroup;
    public GameObject portalCanvasGroup;
    private void Start()
    {
        // Get the CanvasGroup component from the button
       // buttonCanvasGroup = GetComponent<CanvasGroup>();

        // Initially, hide the button
        HideButton(buttonCanvasGroup);
        HideButton(portalCanvasGroup);

    }

    private void Update()
    {
        // Check the distance between the player and the door
        float distanceToDoor = Vector3.Distance(player.position, door.position);
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

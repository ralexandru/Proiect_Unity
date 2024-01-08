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
        // Initial butoanele sunt ascunse
        HideButton(buttonCanvasGroup);
        HideButton(portalCanvasGroup);

    }

    private void Update()
    {
        // Verifica distanta de la jucator la usa
        float distanceToDoor = Vector3.Distance(player.position, door.position);
        // Verifica distanta de la portal la jucator
        float distanceToPortal = Vector3.Distance(player.position, portal.position);
        // Afiseaza sau ascunde butoanele in functie de distanta
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

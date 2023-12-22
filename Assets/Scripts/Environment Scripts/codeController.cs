using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class codeController : MonoBehaviour
{
    string correctCode = levelOptions.coduri[0];
    public GameObject codeEnter;
    public float openSpeed = 1f;
    public float openDistance = 2f;
    bool doorsOpen = false;
    private Vector3 initialLeftDoorPosition;
    private Vector3 initialRightDoorPosition;
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorSignal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void enterCode()
    {
        TextMeshProUGUI code = GameObject.Find("code").GetComponent<TextMeshProUGUI>();
        if (code.text.Length < 4)
        {
            code.text += EventSystem.current.currentSelectedGameObject.name;
        }
        if (code.text.Length == 4)
        {
            if (code.text == correctCode)
            {
                code.text = "CORRECT!";
                doorsOpen= true;
                OpenDoors();
            }
            else
                code.text = "INCORRECT!";
        }
    }
    
    public void OpenDoors()
    {
        doorLeft.SetActive(false);
        doorRight.SetActive(false);
        doorSignal.SetActive(false);
        //Debug.Log("Doors are opening");
        //Transform leftDoor = GameObject.Find("Left_Door_Final").transform;
        //Transform rightDoor = GameObject.Find("Right_Door_Final").transform;
        //initialLeftDoorPosition = leftDoor.position;
        //initialRightDoorPosition = rightDoor.position;
        //// Calculate the new positions based on the openSpeed
        //float step = openSpeed * Time.deltaTime;
        //Vector3 targetLeftPosition = initialLeftDoorPosition - Vector3.right * openDistance; // Adjust the target position as needed
        //Vector3 targetRightPosition = initialRightDoorPosition + Vector3.right * openDistance;

        //// Move the doors towards the target positions to create a smooth movement
        //leftDoor.position = Vector3.MoveTowards(leftDoor.position, targetLeftPosition, step);
        //rightDoor.position = Vector3.MoveTowards(rightDoor.position, targetRightPosition, step);


        //// Check if the doors have fully opened
        //if (Vector3.Distance(leftDoor.position, targetLeftPosition) < 0.1f)
        //{
        //    doorsOpen = false; // Set doorsOpen to false when fully opened
        //}
        
    }
    public void deleteCode()
    {
        TextMeshProUGUI code = GameObject.Find("code").GetComponent<TextMeshProUGUI>();

        if (code.text.Length > 0)
        {
            code.text = code.text.Substring(0, code.text.Length - 1);
        }
        if(code.text.Length > 4)
        {
            code.text = "";
        }
    }

    public void hideEnterCodeUI()
    {
        codeEnter.SetActive(false);
    }
    public void showEnterCodeUI()
    {
        codeEnter.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

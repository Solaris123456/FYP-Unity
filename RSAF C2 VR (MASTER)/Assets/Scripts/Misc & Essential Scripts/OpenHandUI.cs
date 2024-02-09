using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering;
using Valve.VR;

public class OpenHandUI : MonoBehaviour
{
    public GameObject objectToActivate;
    private InputBridge input;
    public bool isButtonPressed = false;
    //public float debounceDelay = 0.5f;
    public GameObject[] objectstohide;
    public bool buttoninput = false;

    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    private void Update()
    {
        // Check for X button press
        /*if (input.XButton && !isButtonPressed)
        {
            foreach (GameObject target in objectstohide)
            {
                target.SetActive(false);
            }
            StartCoroutine(DebounceButtonPress());
        }*/
        if (!input.XButton && isButtonPressed == true)
        {
            if (buttoninput == true)
            {
                buttoninput = false;
            }
            else if (buttoninput == false)
            {
                buttoninput = true;
            }
            else
            {
                Debug.Log("question not asked");
            }
            Debug.Log("question 1");
            isButtonPressed = false;
        }

        if (input.XButton && isButtonPressed == false)
        {
            if (buttoninput == false)
            {
                foreach (GameObject target in objectstohide)
                {
                    if (target)
                    {
                        target.SetActive(false);
                    }
                }
                objectToActivate.SetActive(true);
                isButtonPressed = true;
                Debug.Log("answer 1");
                //ButtonPressed();
            }

            else if (buttoninput == true)
            {
                objectToActivate.SetActive(false);
                isButtonPressed = true;
                Debug.Log("answer 2");
                //ButtonPressedagain();
            }
            else
            {
                Debug.Log("not answered");
            }
        }
    }

    /*private IEnumerator DebounceButtonPress()
    {
        // Set the button as pressed
        isButtonPressed = true;

        // Toggle the game object activation
        objectToActivate.SetActive(!objectToActivate.activeSelf);

        // Wait for the debounce delay
        yield return new WaitForSeconds(debounceDelay);

        // Set the button as released
        isButtonPressed = false;
    }*/
    /*public void ButtonPressed()
    {
        objectToActivate.SetActive(true);
    }
    public void ButtonPressedagain()
    {
        objectToActivate.SetActive(false);
    }*/
}
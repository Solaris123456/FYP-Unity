using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHandDebug : MonoBehaviour
{
    public GameObject objectToActivate;
    private InputBridge input;
    private bool isButtonPressed;
    private float buttonPressedTime;
    public float holdTime = 5f;
    public GameObject[] objectsToHide;

    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    private void Update()
    {
        // Check for A button press
        if (input.AButtonDown && !isButtonPressed)
        {
            buttonPressedTime = Time.time;
            isButtonPressed = true;
            StartCoroutine(HoldButtonCheck());
        }
        else if (input.AButtonUp && isButtonPressed)
        {
            isButtonPressed = false;
            StopCoroutine(HoldButtonCheck());
        }
    }

    private IEnumerator HoldButtonCheck()
    {
        while (isButtonPressed && Time.time - buttonPressedTime < holdTime)
        {
            yield return null;
        }

        if (isButtonPressed && Time.time - buttonPressedTime >= holdTime)
        {
            foreach (GameObject target in objectsToHide)
            {
                target.SetActive(false);
            }
            objectToActivate.SetActive(!objectToActivate.activeSelf);
        }
    }
}

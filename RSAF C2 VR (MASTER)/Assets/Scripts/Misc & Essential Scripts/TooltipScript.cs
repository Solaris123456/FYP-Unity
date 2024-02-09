using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipScript : MonoBehaviour
{
    public GameObject[] tooltipTargets;
    public GameObject acceptChecker;

    private bool isButtonPressed;
    private float buttonPressedTime;
    private InputBridge input;


    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    private void Update()
    {
        // Check if button A is pressed
        if (input.AButtonDown && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonPressedTime = Time.time;
        }

        // Check if button A is released
        if (input.AButtonUp && isButtonPressed)
        {
            isButtonPressed = false;
        }

        // Check if accept checker is active for more than 2 seconds and button A is pressed
        if (acceptChecker.activeSelf && isButtonPressed && Time.time - buttonPressedTime >= 2f)
        {
            // Disable tooltip targets
            foreach (GameObject tooltipTarget in tooltipTargets)
            {
                tooltipTarget.SetActive(false);
            }

            // Disable accept checker
            acceptChecker.SetActive(false);
        }
    }
}
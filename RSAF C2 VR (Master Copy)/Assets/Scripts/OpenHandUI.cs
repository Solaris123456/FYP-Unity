using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHandUI : MonoBehaviour
{
    public GameObject objectToActivate;
    private InputBridge input;
    private bool isButtonPressed;
    public float debounceDelay = 0.5f;
    public GameObject[] objectstohide;

    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    private void Update()
    {
        // Check for X button press
        if (input.XButton && !isButtonPressed)
        {
            foreach (GameObject target in objectstohide)
            {
                target.SetActive(false);
            }
            StartCoroutine(DebounceButtonPress());
        }
    }

    private IEnumerator DebounceButtonPress()
    {
        // Set the button as pressed
        isButtonPressed = true;

        // Toggle the game object activation
        objectToActivate.SetActive(!objectToActivate.activeSelf);

        // Wait for the debounce delay
        yield return new WaitForSeconds(debounceDelay);

        // Set the button as released
        isButtonPressed = false;
    }
}
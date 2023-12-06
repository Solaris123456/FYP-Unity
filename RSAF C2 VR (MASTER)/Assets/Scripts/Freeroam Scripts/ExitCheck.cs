using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    public GameObject Exit;
    public GameObject[] FlammableTargets;
    public GameObject[] SmokeTargets; 
    public GameObject[] SparkTargets;

    private void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if any FlammableTarget GameObjects are active
        foreach (GameObject target in FlammableTargets)
        {
            if (target.activeSelf)
            {
                Debug.Log("Fires not suppressed, unable to leave until situation is resolved");
                return; // Return early if any FlammableTarget is active
            }
        }

        /*foreach (GameObject target in SmokeTargets)
        {
            if (target.activeSelf)
            {
                Debug.Log("Fires not suppressed, unable to leave until situation is resolved");
                return; // Return early if any FlammableTarget is active
            }
        }

        foreach (GameObject target in SparkTargets)
        {
            if (target.activeSelf)
            {
                Debug.Log("Fires not suppressed, unable to leave until situation is resolved");
                return; // Return early if any FlammableTarget is active
            }
        } */

        // If all FlammableTarget GameObjects are inactive, enable the Exit GameObject
        Exit.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        Exit.SetActive(false); // Disable the Exit GameObject when the trigger is exited
    }
}
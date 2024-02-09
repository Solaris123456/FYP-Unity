using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSubtitleInterface : MonoBehaviour
{
    public GameObject[] objectsToManage;
    public GameObject menu;

    private bool conditionMet;
    private bool[] previousStates;

    private void Start()
    {
        // Initialize the previousStates array with the current active/inactive states
        previousStates = new bool[objectsToManage.Length];
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            previousStates[i] = objectsToManage[i].activeSelf;
        }
    }

    private void Update()
    {
        if (menu.activeSelf && !conditionMet)
        {
            // Condition is met, set objects to inactive and store their current states
            SetObjectsActive(false);
            StoreCurrentStates();
            conditionMet = true;
        }
        else if (!menu.activeSelf && conditionMet)
        {
            // Condition is revoked, restore previous active/inactive states
            SetObjectsActive(previousStates);
            conditionMet = false;
        }
    }

    private void StoreCurrentStates()
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            previousStates[i] = objectsToManage[i].activeSelf;
        }
    }

    private void SetObjectsActive(bool active)
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            objectsToManage[i].SetActive(active);
        }
    }

    private void SetObjectsActive(bool[] activeStates)
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            objectsToManage[i].SetActive(activeStates[i]);
        }
    }
}

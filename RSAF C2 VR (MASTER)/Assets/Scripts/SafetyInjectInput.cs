using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

//using NativeWebSocket;
using UnityEngine.Events;

[System.Serializable]
public class SafetyInjector
{
    public int NumberOfRandomTargets = 1;
    public GameObject SafetyInjectParentGameObject;
    public string targetRandomOjectBaseName;
    public string [] PathUndertargetObjectInOrder;
    public string[] AllPreviousChildObjectName;
    public bool pathActivationsStatus;
}
public class SafetyInjectInput : MonoBehaviour
{
    public SafetyInjector[] safetyInjectors; 
    public bool activated;
    //public GameObject CeilingBoards;
    //public GameObject Lights;
    public Fm200PressureCheck fm200PressureCheck;
    // Start is called before the first frame update
    public void SafeInjectStart()
    {
        fm200PressureCheck.PressureCheckBegin();

        // Check if the selectedOption has a child object named "ChildObject"
        int i = 1;
        Transform childObject = safetyInjectors[i].SafetyInjectParentGameObject.transform.Find("BurnFlag");
        if (childObject != null)
        {
            childObject.gameObject.SetActive(true);
            Debug.Log("Burn Flag activated");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

using System.Globalization;

using Valve.VR;
//using NativeWebSocket;
using UnityEngine.Events;
using UnityEditor.Animations;
using UnityEngine.UIElements;
using EPOOutline.Demo;
using UnityEditor.PackageManager;
using System.Diagnostics.CodeAnalysis;

[System.Serializable]
public class SafetyInjector
{
    public int MinNumberOfRandomTargets = 1;
    public int MaxNumberOfRandomTargets = 1;
    public GameObject SafetyInjectParentGameObject;
    public string targetRandomObjectBaseName;
    public string[] AllPreviousChildObjectNameInPath;
    public string[] PathUnderTargetObjectToActivate;
    public bool pathActivationsStatus;
    public int TrueTargetCount; //link to other script to check if user has completed the same number of targets
}

public class SafetyInjectInput : MonoBehaviour
{
    public SafetyInjector[] safetyInjectors; 
    public bool activated;
    private Transform Temporary;
    private Transform childObject;
    private Transform TargetObject;
    private int RNG;
    private int count;
    private int numoftargets;
    //public GameObject CeilingBoards;
    //public GameObject Lights;
    // Start is called before the first frame update
    public void SafeInjectStart()
    {
        // Check if the selectedOption has a child object named "ChildObject"
        activated = true;
        
        for (int safetynum = 0; safetynum < safetyInjectors.Length; safetynum++)
        {
            Temporary = safetyInjectors[safetynum].SafetyInjectParentGameObject.transform;

            if (safetyInjectors[safetynum].AllPreviousChildObjectNameInPath.Length > 0)
            {
                for (int x = 0; x < safetyInjectors[safetynum].AllPreviousChildObjectNameInPath.Length; x++)
                {
                    childObject = Temporary.Find(safetyInjectors[safetynum].AllPreviousChildObjectNameInPath[x]);
                    Temporary = childObject;
                }
            }

            count = Temporary.childCount;
            childObject = null;

            if (safetyInjectors[safetynum].MinNumberOfRandomTargets >= safetyInjectors[safetynum].MaxNumberOfRandomTargets)
            {
                numoftargets = safetyInjectors[safetynum].MinNumberOfRandomTargets;
            }
            else
            {
                numoftargets = Random.Range(safetyInjectors[safetynum].MinNumberOfRandomTargets, safetyInjectors[safetynum].MaxNumberOfRandomTargets);
            }

            if (numoftargets < 1)
            {
                numoftargets = 1;
            }

            safetyInjectors[safetynum].TrueTargetCount = numoftargets;

            for (int i = 0; i < numoftargets; i++)
            {
                RNG = Random.Range(0, count * 2);
                childObject = Temporary.Find(safetyInjectors[safetynum].targetRandomObjectBaseName + " (" + RNG + ") ");

                while (childObject == null)
                {
                    RNG = Random.Range(0, count * 2);
                    childObject = Temporary.Find(safetyInjectors[safetynum].targetRandomObjectBaseName + " (" + RNG + ")");
                }
                TargetObject = childObject;

                if (safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length > 0)
                {
                    for (int x = 0; x < safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length; x++)
                    {
                        childObject = TargetObject.Find(safetyInjectors[safetynum].PathUnderTargetObjectToActivate[x]);
                        TargetObject = childObject;
                    }
                }

                TargetObject.gameObject.SetActive(safetyInjectors[safetynum].pathActivationsStatus);
                childObject = null;
                TargetObject = null;
            }
            Temporary = null;
        }
    }
}

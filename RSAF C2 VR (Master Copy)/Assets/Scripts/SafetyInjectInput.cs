using System.Collections.Generic;
using UnityEngine;

//using NativeWebSocket;


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
    public int TrueTargetCount; //for debugging 
    public List<GameObject> selectedTargets = new List<GameObject>();
}

public class SafetyInjectInput : MonoBehaviour
{
    public SafetyInjector[] safetyInjectors; 
    public bool activated;
    private Transform Temporary;
    private Transform childObject;
    private Transform TargetObject;
    private int RNG;
    private float count;
    private int numoftargets;
    //public GameObject CeilingBoards;
    //public GameObject Lights;
    // Start is called before the first frame update
    public void SafeInjectStart()
    {
        if (activated == false)
        {
            childObject = null;
            Temporary = null;
            TargetObject = null;
            activated = true;

            for (int safetynum = 0; safetynum < safetyInjectors.Length; safetynum++)
            {
                safetyInjectors[safetynum].selectedTargets.Clear(); //clear the gameobject list
                Temporary = safetyInjectors[safetynum].SafetyInjectParentGameObject.transform;

                /*
                if (safetyInjectors[safetynum].AllPreviousChildObjectNameInPath.Length > 0)
                {
                    for (int x = 0; x < safetyInjectors[safetynum].AllPreviousChildObjectNameInPath.Length; x++)
                    {
                        childObject = Temporary.Find(safetyInjectors[safetynum].AllPreviousChildObjectNameInPath[x]);
                        Temporary = childObject;
                    }
                }
                */
                // pathfinder to find the parant object of the list of child objects to randomize

                count = Temporary.childCount; // to count how many child objects there are to randomize
                childObject = null;

                //from here
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
                //to here
                //is just in case some idiot put an invalid range

                for (int i = 0; i < numoftargets; i++)
                {
                    RNG = Random.Range(0, Mathf.FloorToInt(count * 3 / 2));
                    string RNGName = string.Format("{0} ({1})", safetyInjectors[safetynum].targetRandomObjectBaseName, RNG);
                    childObject = Temporary.Find(RNGName);

                    while (childObject == null || childObject.gameObject.activeSelf == safetyInjectors[safetynum].pathActivationsStatus)
                    {
                        RNG = Random.Range(0, Mathf.FloorToInt(count * 3 / 2));
                        RNGName = string.Format("{0} ({1})", safetyInjectors[safetynum].targetRandomObjectBaseName, RNG);
                        childObject = Temporary.Find(RNGName);
                    }
                    TargetObject = childObject;
                    //Randomly pick any target that has not been activated

                    if (safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length > 0)
                    {
                        for (int x = 0; x < safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length; x++)
                        {
                            childObject = TargetObject.Find(safetyInjectors[safetynum].PathUnderTargetObjectToActivate[x]);
                            TargetObject = childObject;
                        }
                    }
                    //path finder to find exact targeted child object to activate

                    TargetObject.gameObject.SetActive(safetyInjectors[safetynum].pathActivationsStatus);
                    safetyInjectors[safetynum].selectedTargets.Add(TargetObject.gameObject);
                    //activations of path and adding the object into the list for the checker script to check

                    childObject = null;
                    TargetObject = null;
                }
                Temporary = null;
                //clear all temporary gameobjects for the loop to run agn
            }
        }
    }
}

// text me if you still dont understand (Lang Wenbo / SP 2023 sem 2 / DARE/FT/3B/02)

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class safetyInjectToCheck
{
    public string safetyInjectName;
    public bool alldone;
    public int numberOfInjectsLeft;
}
public class SuccessfulSafetyInject : MonoBehaviour
{
    public safetyInjectToCheck[] safetyInjectToCheck; 
    public SafetyInjectInput safetyInjectInput;
    
    public void safetyInjectCheck()
    {
        for (int safetynum = 0; safetynum < safetyInjectToCheck.Length; safetynum++)
        {
            safetyInjectToCheck[safetynum].numberOfInjectsLeft = 0;
            for (int x = 0; x < safetyInjectInput.safetyInjectors[safetynum].selectedTargets.Count; x++)
            {
                if (safetyInjectInput.safetyInjectors[safetynum].selectedTargets[x].gameObject.activeSelf == safetyInjectInput.safetyInjectors[safetynum].pathActivationsStatus)
                {
                    safetyInjectToCheck[safetynum].numberOfInjectsLeft++;
                }
            }
            if (safetyInjectToCheck[safetynum].numberOfInjectsLeft == 0)
            {
                safetyInjectToCheck[safetynum].alldone = true; 
            }
        }
    }
}

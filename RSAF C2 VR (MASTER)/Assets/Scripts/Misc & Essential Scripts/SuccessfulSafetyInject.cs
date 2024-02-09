using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SafetyInjectToCheck
{
    public string safetyInjectName;//for debugging
    public bool alldone;
    public int numberOfInjectsLeft;
}
public class SuccessfulSafetyInject : MonoBehaviour
{
    public SafetyInjectToCheck[] safetyInjectToCheck; 
    public SafetyInjectInput safetyInjectInput;
    
    public void safetyInjectCheck()
    {
        if (safetyInjectToCheck.Length != 0)
        {
            for (int safetynum = 0; safetynum < safetyInjectToCheck.Length; safetynum++)
            {
                safetyInjectToCheck[safetynum].numberOfInjectsLeft = 0;
                Transform Temporary = safetyInjectInput.safetyInjectors[safetynum].SafetyInjectParentGameObject.transform;

                for (int x = 0; x < safetyInjectInput.safetyInjectors[safetynum].targetnumber.Count; x++)
                {
                    Transform childObject = Temporary.GetChild(safetyInjectInput.safetyInjectors[safetynum].targetnumber[x]);
                    Transform TargetObject = childObject;

                    if (safetyInjectInput.safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length > 0)
                    {
                        for (int i = 0; i < safetyInjectInput.safetyInjectors[safetynum].PathUnderTargetObjectToActivate.Length; i++)
                        {
                            childObject = TargetObject.Find(safetyInjectInput.safetyInjectors[safetynum].PathUnderTargetObjectToActivate[i]);
                            TargetObject = childObject;
                        }
                    }
                    if (TargetObject.gameObject.activeSelf == safetyInjectInput.safetyInjectors[safetynum].pathActivationsStatus)
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
}
// text me if you still dont understand (Lang Wenbo / SP 2023 sem 2 / DARE/FT/3B/02)

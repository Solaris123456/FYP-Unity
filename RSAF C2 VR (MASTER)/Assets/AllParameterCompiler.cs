using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR;

public class AllParameterCompiler : MonoBehaviour
{
    public SuccessfulSafetyInject successfulSafetyInject;
    public SuccessRackCounter successfulRackCounter;
    public bool trainingMode = false;
    public bool pressedWithoutFinishing = false;
    public void Finish()
    {
        if (successfulRackCounter.AllRacksDone)
        {
            if (successfulSafetyInject.safetyInjectToCheck[0].alldone)
            {

            }
            else
            {

            }
            if (successfulSafetyInject.safetyInjectToCheck[1].alldone)
            {

            }
            else
            {

            }
            if (successfulSafetyInject.safetyInjectToCheck[2].alldone)
            {

            }
            else
            {

            }
        }
        else
        {
            if (pressedWithoutFinishing == false)
            {
                pressedWithoutFinishing = true;
            }
        }
    }
}

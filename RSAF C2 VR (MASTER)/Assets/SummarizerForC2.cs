using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SummarizerForC2 : MonoBehaviour
{
    public SuccessfulSafetyInject successfulSafetyInject;
    public SuccessRackCounter successfulRackCounter;
    public GameObject timerFlag;
    public bool trainingMode = false;
    public float FinalTime = 0;
    public bool Fm200 = false;
    public bool CeilingCheck = false;
    public bool LightCheck = false;
    public bool PressedWithoutFinishing = false;
    public void Finish()
    {
        if (successfulRackCounter.AllRacksDone)
        {
            //just copy past this if you need to add more injects
            if (successfulSafetyInject.safetyInjectToCheck[0].alldone)
            {
                if (LightCheck == false)
                {
                    LightCheck = true;
                }
            }
            else
            {

            }
            //just copy past this if you need to add more injects
            if (successfulSafetyInject.safetyInjectToCheck[1].alldone)
            {
                if (CeilingCheck == false)
                {
                    CeilingCheck = true;
                }
            }
            else
            {

            }
            //just copy past this if you need to add more injects
            if (successfulSafetyInject.safetyInjectToCheck[2].alldone)
            {
                if (Fm200 == false)
                {
                    Fm200 = true;
                }
            }
            else
            {

            }

            //training & assessment mode logic
            if (trainingMode == false)
            {
                timerFlag.SetActive(false);
            }
            else
            {
                if(LightCheck && CeilingCheck && Fm200)
                {
                    timerFlag.SetActive(false);
                }
            }
        }
        else
        {
            if (PressedWithoutFinishing == false)
            {
                PressedWithoutFinishing = true;
            }
        }
    }
}

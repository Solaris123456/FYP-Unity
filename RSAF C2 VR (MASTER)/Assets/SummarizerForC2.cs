using UnityEngine;
using UnityEngine.Events;

public class SummarizerForC2 : MonoBehaviour
{
    public SuccessfulSafetyInject successfulSafetyInject;
    public SuccessRackCounter successfulRackCounter;
    public Register register;
    public EventTimer eventTimer;
    public GameObject timerFlag;
    public GameObject GameCompleteTrigger;
    public AudioSource NotDoneAudio;
    public UnityEvent FailTrigger;

    public bool trainingMode = false;
    public float FinalTime = 0;
    public bool Fm200 = false;
    public bool CeilingCheck = false;
    public bool LightCheck = false;
    public bool PressedWithoutFinishing = false;
    public void Finish()
    {
        successfulRackCounter.SuccessRackCheck();
        successfulSafetyInject.safetyInjectCheck();
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
            if (!trainingMode)
            {
                StopTheGame();
                StoreDataForC2();
            }
            else
            {
                //must complete all safety injects
                if (LightCheck && CeilingCheck && Fm200)
                {
                    StopTheGame();
                }
                else
                {
                    NotDoneAudio.Play();
                    if (PressedWithoutFinishing == false)
                    {
                        PressedWithoutFinishing = true;
                    }
                }
            }
        }
        else
        {
            if (trainingMode)
            {
                NotDoneAudio.Play();
                if (PressedWithoutFinishing == false)
                {
                    PressedWithoutFinishing = true;
                }
            }
            else
            {
                FailTrigger.Invoke();
            }
        }
    }
    public void StopTheGame()
    {
        timerFlag.SetActive(false);
        FinalTime = eventTimer.RecordedTime;
        FinalTime = Mathf.Floor(FinalTime * 1000) / 1000;

        GameCompleteTrigger.SetActive(true);
    }
    public void StoreDataForC2()
    {
        
        
        register.lightErrorFound = LightCheck;
        register.ceilingErrorFound = CeilingCheck;
        register.Fm200CheckFail = Fm200;
        register.CompleteSimulation(FinalTime);
    }
}


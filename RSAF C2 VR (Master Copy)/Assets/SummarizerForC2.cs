using UnityEngine;

public class SummarizerForC2 : MonoBehaviour
{
    public SuccessfulSafetyInject successfulSafetyInject;
    public SuccessRackCounter successfulRackCounter;
    public StoreData storeData;
    public EventTimer eventTimer;
    public GameObject timerFlag;
    public GameObject GameCompleteTrigger;
    public AudioSource NotDoneAudio;

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
                StopTheGame();
                StoreDataForC2();
            }
            else
            {
                //must complete all safety injects
                if(LightCheck && CeilingCheck && Fm200)
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
            NotDoneAudio.Play();
            if (PressedWithoutFinishing == false)
            {
                PressedWithoutFinishing = true;
            }
        }
    }
    public void StopTheGame()
    {
        timerFlag.SetActive(false);
        FinalTime = eventTimer.RecordedTime;
        GameCompleteTrigger.SetActive(true);
    }
    public void StoreDataForC2()
    {
        storeData.timeTaken = FinalTime;
        storeData.lightErrorFound = LightCheck;
        storeData.ceilingErrorFound = CeilingCheck;
        storeData.Fm200CheckFail = Fm200;
        storeData.StoreSimulationResults();
    }
}


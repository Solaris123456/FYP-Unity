using UnityEngine;

public class SummarizerForDeploy : MonoBehaviour
{
    public SuccessfulSafetyInject successfulSafetyInject;
    public SuccessRackCounter successfulRackCounter;
    public Register register;
    public EventTimer eventTimer;
    public GameObject timerFlag;
    public GameObject GameCompleteTrigger;
    public AudioSource NotDoneAudio;

    public bool trainingMode = false;
    public float FinalTime = 0;
    public bool PressedWithoutFinishing = false;
    public bool LightCheck = false; //add more if there are more injects
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

            //training & assessment mode logic
            if (!trainingMode)
            {
                StopTheGame();
                StoreDataForC2();
            }
            else
            {
                //must complete all safety injects
                if(LightCheck)
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
        FinalTime = Mathf.Floor(FinalTime * 1000) / 1000;

        GameCompleteTrigger.SetActive(true);
    }
    public void StoreDataForC2()
    {
        register.lightErrorFound = LightCheck;
        register.CompleteSimulation(FinalTime);
    }
}


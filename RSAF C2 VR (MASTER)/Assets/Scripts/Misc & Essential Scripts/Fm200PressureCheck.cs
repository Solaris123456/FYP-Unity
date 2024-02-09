using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class Fm200PressureCheck : MonoBehaviour
{
    public bool trainingMode;
    public bool Normal;
    public UnityEvent CorrectTrigger;
    public UnityEvent DoneTrigger;
    public GameObject[] Abnormalities;
    public AudioSource wrongAudio;
    public int NumberOfFaultTargets;
    private float Number;
    private int RNG;
    private List<int> targetnumber = new List<int>();

    public void Start()
    {
        RNG = 0;
        Number = 0;
        RNG = Random.Range(0, 9);
        Number += RNG;
        if (Number % 2 == 0)
        {
            Normal = true;
        }
        else
        {
            Normal = false;
            for (int i = 0; i < NumberOfFaultTargets; i++)
            {
                do { RNG = Random.Range(0, (Abnormalities.Length - 1)); } while (targetnumber.Contains(RNG));
                Abnormalities[RNG].SetActive(true);
            }
        }
    }

    public void NormalButtonPressed()
    {
        if (Normal)
        {
            DoneTrigger.Invoke();
            CorrectTrigger.Invoke();
        }
        else
        {
            if (!trainingMode)
            {
                DoneTrigger.Invoke();
            }
            else
            {
                wrongAudio.Play();
            }
        }
    }

    public void AbnormalButtonPressed()
    {
        if (!Normal)
        {
            DoneTrigger.Invoke();
            CorrectTrigger.Invoke();
        }
        else
        {
            if (!trainingMode)
            {
                DoneTrigger.Invoke();
            }
            else
            {
                wrongAudio.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Valve.VR;

public class Flicker : MonoBehaviour
{
    public GameObject FlickerObject;
    public float speedMultiplier;
    public float timeBeforeFlicker;
    public float FlickerFor;
    private float TimingValue = 0;
    private float FlickerTime = 0;
    private int OddOrEven = 0;

    void Start()
    {
       if (speedMultiplier <= 0)
        {
            speedMultiplier = 1;
        }
        FlickerFor += timeBeforeFlicker;
    }
    // Update is called once per frame
    void Update()
    {
        TimingValue += Time.deltaTime;
        if (TimingValue > timeBeforeFlicker) 
        {
            if (TimingValue < FlickerFor)
            {
                FlickerTime += (Time.deltaTime * speedMultiplier);
                OddOrEven = Mathf.FloorToInt(FlickerTime % 2);
                if (OddOrEven == 1) // that's odd XD get it (I lame af sia lol)
                {
                    FlickerObject.SetActive(true);
                }
                if (OddOrEven == 0)
                {
                    FlickerObject.SetActive(false);
                }
            }
            else
            {
                FlickerObject.SetActive(true);
                FlickerTime = 0;
                TimingValue = 0;
            }
        }
    }
}

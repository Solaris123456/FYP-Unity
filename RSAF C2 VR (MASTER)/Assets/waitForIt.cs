using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class waitForIt : MonoBehaviour
{

    public float waitingTime;
    public bool activated = false;
    private float realtime;
    public UnityEvent excutable;
    // Update is called once per frame
    void Update()
    {
        realtime += Time.deltaTime;
        if (realtime > waitingTime && activated == false)
        {
            realtime = 0;
            activated = true;
            excutable.Invoke();
        }
    }
}

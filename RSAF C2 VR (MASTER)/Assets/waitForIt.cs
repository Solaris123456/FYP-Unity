using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class waitForIt : MonoBehaviour
{

    public float waitingTime;
    public bool activated = false;
    private float realtime;
    public UnityEvent excutable;
    public GameObject cheatObject;
    public UnityEvent cheatActivation;
    // Update is called once per frame
    public void Start()
    {
        if (!cheatObject.activeSelf)
        {
            excutable.Invoke();
            cheatActivation.Invoke();
        }
    }
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

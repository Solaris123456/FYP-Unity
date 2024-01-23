using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Valve.VR;

public class waitForIt : MonoBehaviour
{

    public float waitingTime;
    public bool activated = false;
    private float realtime = 0;
    public UnityEvent excutable;
    private bool cheated = false;
    public GameObject cheatObject;
    public UnityEvent cheatActivation;
    public GameObject[]cheatPermenantlyDisabledObject;
    // Update is called once per frame
    public void Start()
    {
        if (!cheatObject.activeSelf)
        {
            cheated = true;
            excutable.Invoke();
            cheatActivation.Invoke();
        }
        else
        {
            int x = 0;
            foreach (GameObject cheat in cheatPermenantlyDisabledObject)
            {
                if (cheatPermenantlyDisabledObject[x].activeSelf)
                {
                    cheatPermenantlyDisabledObject[x].SetActive(false);
                }
                x++;
            }
        }
    }
    void Update()
    {
        if (!cheated)
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
}

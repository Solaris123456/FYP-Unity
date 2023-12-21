using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    public GameObject TimerActivationFlag;
    public float RecordedTime;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        RecordedTime = 0;
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActivationFlag.activeSelf)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            RecordedTime = Timer;
            Timer = 0;
        }
    }
}

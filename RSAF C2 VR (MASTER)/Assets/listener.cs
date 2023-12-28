using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class listener : MonoBehaviour
{
    public float timeToWait;
    public AudioSource source;
    public UnityEvent EventToInvoke;
    private float RealTime;
    public bool done;
    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            RealTime += Time.deltaTime;
            if (RealTime > timeToWait)
            {
                RealTime = timeToWait;
                if (!source.isPlaying)
                {
                    EventToInvoke.Invoke();
                    done = true;
                    RealTime = 0;
                }
            }
        }
    }
}

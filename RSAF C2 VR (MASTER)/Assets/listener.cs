using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class listener : MonoBehaviour
{
    public AudioSource source;
    public UnityEvent EventToInvoke;
    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            EventToInvoke.Invoke();
        }
    }
}

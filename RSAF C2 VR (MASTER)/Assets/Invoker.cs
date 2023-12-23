using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invoker : MonoBehaviour
{
    public UnityEvent Invoking;
    // Start is called before the first frame update
    void Start()
    {
        Invoking.Invoke();
    }
}

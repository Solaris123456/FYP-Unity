using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invoker : MonoBehaviour
{
    public UnityEvent InvokingFunction;
    // Start is called before the first frame update
    void Start()
    {
        InvokingFunction.Invoke(); 
    }
}

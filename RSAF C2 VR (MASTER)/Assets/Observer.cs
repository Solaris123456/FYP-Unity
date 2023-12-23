using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    public GameObject observingObjects;
    public UnityEvent Invoker;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (observingObjects.activeSelf)
        {
            Invoker.Invoke();
        }
    }
}

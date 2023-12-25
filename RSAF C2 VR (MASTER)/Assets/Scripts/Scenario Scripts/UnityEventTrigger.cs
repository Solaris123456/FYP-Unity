using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour
{
    public UnityEvent TargetElement;
    public GameObject ReferenceElement;
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (!ReferenceElement.activeSelf)
            {
                TargetElement.Invoke();
                done = true;
            }
        }
    }
}

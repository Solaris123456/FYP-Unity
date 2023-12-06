using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusController : MonoBehaviour
{
    public GameObject[] TurnOnIfTriggered;
    public GameObject[] TurnOffIfTriggered;

    public UnityEvent EventTargets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerEvent()
    {
        foreach (GameObject target in TurnOnIfTriggered)
        {
            target.SetActive(true);
        }

        foreach (GameObject target in TurnOffIfTriggered)
        {
            target.SetActive(false);
        }

        EventTargets.Invoke();
    }
}

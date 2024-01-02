using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    public GameObject ObservedObject;
    public GameObject ObserverObject;
    public bool requiredActiveStatusOfObservedObject = false;
    public bool activated = false;
    public UnityEvent TriggerEvent;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (ObservedObject.activeSelf == requiredActiveStatusOfObservedObject && activated == false)
        {
            activated = true;
            TriggerEvent.Invoke();
            ObserverObject.SetActive(false);
        }
    }
}

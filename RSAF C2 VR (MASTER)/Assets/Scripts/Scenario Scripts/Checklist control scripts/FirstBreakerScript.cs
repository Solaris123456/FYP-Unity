using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBreakerScript : MonoBehaviour
{
    public GameObject BreakerChecker;
    public GameObject IdentifyBreaker;
    public GameObject BreakerConfirmAudio;
    public GameObject BreakerBeginflag;
    public GameObject BreakerTick;
    public GameObject CompleteCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!BreakerChecker.activeSelf && IdentifyBreaker.activeSelf)
        {
            BreakerConfirmAudio.SetActive(true);
            BreakerBeginflag.SetActive(true);
            BreakerTick.SetActive(true);
            CompleteCheck.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualScript : MonoBehaviour
{
    public GameObject Inspectionflag;
    public GameObject unlockflag;
    public GameObject manualflag;
    public GameObject ManualPickupAudio;
    public GameObject InspectionSubtitle;
    public GameObject InspectionChat;
    public GameObject InspectionCheckbox;
    public GameObject ProceedTick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (unlockflag.activeSelf && manualflag.activeSelf)
        {
            ManualPickupAudio.SetActive(true);
            Inspectionflag.SetActive(true);
            InspectionChat.SetActive(true);
            InspectionCheckbox.SetActive(true);
            //InspectionSubtitle.SetActive(true);
            ProceedTick.SetActive(true);
        }
    }
}

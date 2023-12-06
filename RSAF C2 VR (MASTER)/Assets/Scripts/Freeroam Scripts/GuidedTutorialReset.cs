using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedTutorialReset : MonoBehaviour
{
    public GameObject GuidedTutorialStep1;
    public GameObject GuidedTutorialStep2;
    public GameObject GuidedTutorialStep3;
    public GameObject GuidedTutorialStep4;
    public GameObject GuidedTutorialStep5;
    public GameObject GuidedTutorialStep6;
    public GameObject GuidedTutorialStep7;
    public GameObject GuidedTutorialStep8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGuidedTutorial()
    {
        GuidedTutorialStep1.SetActive(false);
        GuidedTutorialStep2.SetActive(false);
        GuidedTutorialStep3.SetActive(false);
        GuidedTutorialStep4.SetActive(false);
        GuidedTutorialStep5.SetActive(false);
        GuidedTutorialStep6.SetActive(false);
        GuidedTutorialStep7.SetActive(false);
        GuidedTutorialStep8.SetActive(false);

        Debug.Log("Guided Tutorial Steps all Hidden");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedTutorialTransitionScript : MonoBehaviour
{
    public GameObject PreviousStep;
    public GameObject CurrentStep;
    public GameObject NextStep;
    bool GuidedTutorialState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ForwardTransitionScript()
    {
            NextStep.SetActive(true);
            Debug.Log("Guided Tutorial Next Step Displayed");

            CurrentStep.SetActive(false);
            Debug.Log("Guided Tutorial Current Step Hidden");
        
    }

    public void PreviousTransitionScript()
    {
        PreviousStep.SetActive(true);
        Debug.Log("Guided Tutorial Previous Step Displayed");

        CurrentStep.SetActive(false);
        Debug.Log("Guided Tutorial Current Step Hidden");

    }

}

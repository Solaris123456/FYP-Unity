using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedTutorialStart : MonoBehaviour
{
    //bool GuidedTutorialState = false;
    public GameObject GuidedTutorialStep1;

    public GameObject Spark;
    public GameObject Smoke;
    public GameObject Fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGuidedTutorial()
    {
            //GuidedTutorialState = true;
            GuidedTutorialStep1.SetActive(true);
            Debug.Log("Guided Tutorial Initiated");
            Debug.Log("Guided Tutorial Step 1 Displayed");

            Fire.SetActive(false);
            Smoke.SetActive(true);
            Spark.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSkip : MonoBehaviour
{
    public GameObject PreviousStep;
    public GameObject CurrentStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStep.activeSelf)
        {
            PreviousStep.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class TrialLightScript : MonoBehaviour
{
    public GameObject[] TrialLight;

    public GameObject[] BurnFlags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Array.Exists(BurnFlags, obj => obj.activeSelf))
        {
            foreach (GameObject target in TrialLight)
            {
                target.SetActive(true);
            }
        }

        else
        {
            foreach (GameObject target in TrialLight)
            {
                target.SetActive(false);
            }
        }
    }
}

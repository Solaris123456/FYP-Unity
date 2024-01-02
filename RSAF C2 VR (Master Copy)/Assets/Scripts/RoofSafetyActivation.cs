using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoofSafetyActivation : MonoBehaviour
{
    public safetyStarter SafetyStarter;
    public GameObject CeilingError;
    public GameObject LightError;
    // Start is called before the first frame update
    void Start()
    {
        if (SafetyStarter.Light)
        { 
            LightError.SetActive(true);
        }
        if (SafetyStarter.Panel)
        {
            CeilingError.SetActive(true);
        }
    }
}

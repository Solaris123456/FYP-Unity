using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoofSafetyActivation : MonoBehaviour
{
    public bool safety = true;
    public bool panel = false;
    public bool lights = false;
    public bool errorFound = false;

    // Start is called before the first frame update
    public void safetydone()
    {
        safety = true;
        errorFound = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

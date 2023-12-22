using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDetector : MonoBehaviour
{
    public GameObject FireFlag;
    public GameObject FM200Warning;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (FireFlag.activeSelf)
        {
            FM200Warning.SetActive(true);
        }
    }
}

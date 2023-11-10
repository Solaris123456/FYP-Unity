using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerDirectionRef : MonoBehaviour
{
    public GameObject inputSource;
    public GameObject inputSource2;
    public GameObject forwardDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = inputSource.transform.forward.normalized;
        Vector3 direction2 = inputSource2.transform.forward.normalized;
        Vector3 combined = (direction + direction2).normalized;
        forwardDirection.transform.forward = combined;
    }
}

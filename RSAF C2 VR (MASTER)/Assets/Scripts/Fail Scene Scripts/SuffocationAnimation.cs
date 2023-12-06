using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuffocationAnimation : MonoBehaviour
{
    public GameObject FailTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FailedToSuffocation()
    {
        FailTest.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedScript : MonoBehaviour
{
    public GameObject proceedflag;
    public GameObject unlockflag;
    public GameObject proceedaudio;

    private bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (proceedflag.activeSelf && complete == false)
        {
            proceedaudio.SetActive(true);
            complete = true;
            unlockflag.SetActive(true);
        }
    }
}

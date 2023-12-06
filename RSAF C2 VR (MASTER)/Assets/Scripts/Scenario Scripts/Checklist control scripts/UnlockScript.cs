using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScript : MonoBehaviour
{
    public GameObject Acknowledgeproceedflag;
    public GameObject unlockflag;
    public GameObject manualflag;
    public GameObject doorinviswall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Acknowledgeproceedflag.activeSelf && unlockflag.activeSelf)
        {
            doorinviswall.SetActive(false);
            manualflag.SetActive(true);
        }
    }
}

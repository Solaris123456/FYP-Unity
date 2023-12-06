using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEnableScript : MonoBehaviour
{
    public GameObject Exitflag;
    public GameObject ExitUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Exitflag.activeSelf)
        {
            ExitUI.SetActive(true);
        }
    }
}

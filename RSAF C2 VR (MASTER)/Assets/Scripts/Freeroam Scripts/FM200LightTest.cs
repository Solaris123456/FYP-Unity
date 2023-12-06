using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FM200LightTest : MonoBehaviour
{

    bool LightValue = false;
    public GameObject FM200Light;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoSomething()
    {
        if (LightValue == false)
        {
            FM200Light.SetActive(true);
            Debug.Log("Lights On");
            LightValue = true;
        }
        else
        {
            FM200Light.SetActive(false);
            Debug.Log("Lights off");
            LightValue = false;
        }
    }
}

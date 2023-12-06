using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFire : MonoBehaviour
{
    public GameObject Spark;
    public GameObject Smoke;
    public GameObject Fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScenarioFire()
    {
        Fire.SetActive(false);
        Smoke.SetActive(true);
        Spark.SetActive(true);
    }
}

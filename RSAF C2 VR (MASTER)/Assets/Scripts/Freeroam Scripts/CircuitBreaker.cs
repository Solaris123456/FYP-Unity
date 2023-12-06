using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBreaker : MonoBehaviour
{
    public GameObject FireTick;
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

    public void KillPower()
    {
        FireTick.SetActive(false);
        Fire.SetActive(false);
        Smoke.SetActive(false);
        Spark.SetActive(false);
    }
}
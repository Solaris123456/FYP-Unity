using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircuitBreakerClearFire : MonoBehaviour
{
    public GameObject FireTick;
    public GameObject Spark;
    public GameObject Smoke;
    public GameObject BurnFlag;
    public string Failure;
    public GameObject BreakerChecker;
    public GameObject SuccessfulCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearFire()
    {
        if (BurnFlag.activeSelf)
        {
            BurnFlag.SetActive(false);
            Spark.gameObject.SetActive(false);
            Smoke.gameObject.SetActive(false);
            FireTick.SetActive(false);
            Debug.Log("Power to breaker removed");
            BreakerChecker.SetActive(false);
            SuccessfulCount.SetActive(false);
        }
        else
        {
            Debug.Log("Game over due to incorrect breaker input, Returning to menu");
            SceneManager.LoadScene(Failure);
        }

    }
}

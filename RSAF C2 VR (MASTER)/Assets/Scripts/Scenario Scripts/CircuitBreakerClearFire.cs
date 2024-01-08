using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CircuitBreakerClearFire : MonoBehaviour
{
    public GameObject FireTick;
    public GameObject Spark;
    public GameObject Smoke;
    public GameObject BurnFlag;
    public string Failure;
    public GameObject BreakerChecker;
    public UnityEvent ForAssessmentOnly;
    public bool pressed = false;

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
        if (!pressed)
        { 
            pressed = true;
            if (BurnFlag.activeSelf)
            {
                BurnFlag.SetActive(false);
                Spark.gameObject.SetActive(false);
                Smoke.gameObject.SetActive(false);
                FireTick.SetActive(false);
                Debug.Log("Power to breaker removed");
                BreakerChecker.SetActive(false);
            }
            else
            {
                ForAssessmentOnly.Invoke();
                Debug.Log("Game over due to incorrect breaker input, Returning to menu");
                SceneManager.LoadScene(Failure);
            }
        }
    }
}

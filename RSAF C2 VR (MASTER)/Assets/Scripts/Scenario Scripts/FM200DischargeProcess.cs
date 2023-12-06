using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FM200DischargeProcess : MonoBehaviour
{
    public GameObject[] FM200Targets;
    public GameObject[] burnTarget;
    public GameObject FM200Area;
    public GameObject FM200Checker;
    public GameObject FM200Warning;
    public GameObject[] FireParticle;
    public GameObject[] DeathPlane;
    public GameObject[] Audio;

    private bool hasStartedAutodischarge = false;

    // Update is called once per frame
    void Update()
    {
        if (FM200Warning.activeSelf && FM200Checker.activeSelf)
        {
            Debug.Log("FM200 Sequence initiated by automatic trigger");
            FM200Checker.SetActive(false);

            // Start the Autodischarge coroutine only once
            if (!hasStartedAutodischarge)
            {
                StartCoroutine(AutodischargeV2());
                hasStartedAutodischarge = true;
            }
        }
    }

    public IEnumerator AutodischargeV2()
    {
        Debug.Log("FM200 Begin Countdown Timer");
        yield return new WaitForSeconds(10);
        Debug.Log("FM200 Discharged");
        StartCoroutine(FM200Discharge());
    }

    /* 
    public IEnumerator AutoDischarge()
    {
        if (Array.Exists(burnTarget, obj => obj.activeSelf) && FM200Checker.gameObject.activeSelf)
        {
            Debug.Log("FM200 Begin Countdown Timer");
            yield return new WaitForSeconds(20);
            StartCoroutine(FM200Test());
            Debug.Log("FM200 Discharged through Automatic Conditions");
            FM200Checker.gameObject.SetActive(false);
        }
    }
    */

    public void ForceDischarge()
    {
        StartCoroutine(FM200Discharge()); // Start the coroutine
        Debug.Log("Command for manually discharge sent");
    }

    public IEnumerator FM200Discharge()
    {
        foreach (GameObject target in FM200Targets)
        {
            PlayParticleEffect(target);
            Debug.Log("FM200 Nozzle Discharged");
        }

        yield return new WaitForSeconds(0);

        PlayParticleEffect(FM200Area);
        Debug.Log("FM200 Area Discharged");

        foreach (GameObject target in DeathPlane)
        {
            target.SetActive(true);
        }
        Debug.Log("DeathPlane Spawned");

        foreach (GameObject target in burnTarget)
        {
            target.SetActive(false);
        }

        FM200Warning.SetActive(false);

        foreach (GameObject target in FireParticle)
        {
            target.SetActive(false);
        }

        foreach (GameObject target in Audio)
        {
            target.SetActive(true);
        }

        Debug.Log("Fires Extinguished");
    }

    private void PlayParticleEffect(GameObject target)
    {
        ParticleSystem particleSystem = target.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play(); // Play the particle system
            Debug.Log("FM200 Deployed");
            Debug.Log("Begin evacuation");
        }
        else
        {
            Debug.LogWarning("No particle system component found on the target: " + target.name);
        }
    }
}

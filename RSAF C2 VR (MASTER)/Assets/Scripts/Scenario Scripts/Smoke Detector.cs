using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetector : MonoBehaviour
{
    public GameObject[] FM200Targets;

    public GameObject FM200Area;

    public GameObject FireTick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDischarge()
    {
        if (FireTick.activeSelf)
        {
            StartCoroutine(DischargeProcess());
            Debug.Log("Discharge Process engaged");
        }
    }

    public IEnumerator DischargeProcess()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(FM200Discharge()); // Start the coroutine
        Debug.Log("command sent");
        FireTick.SetActive(false);
        yield break;
    }

    public IEnumerator FM200Discharge()
    {
        foreach (GameObject target in FM200Targets)
        {
            PlayParticleEffect(target);
        }

        yield return new WaitForSeconds(0);

        PlayParticleEffect(FM200Area);
    }

    private void PlayParticleEffect(GameObject target)
    {
        ParticleSystem particleSystem = target.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play(); // Play the particle system
            Debug.Log("FM200 Deployed");
        }
        else
        {
            Debug.LogWarning("No particle system component found on the target: " + target.name);
            Debug.Log("Begin evacuation");
        }
    }
}
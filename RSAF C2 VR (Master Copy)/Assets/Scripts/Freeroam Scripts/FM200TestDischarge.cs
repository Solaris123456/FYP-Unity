using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FM200TestDischarge : MonoBehaviour
{
    public GameObject[] FM200Targets;

    public GameObject FM200Area;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginDischarge()
    {
        StartCoroutine(FM200Test()); // Start the coroutine
        Debug.Log("command sent");
    }

    public IEnumerator FM200Test()
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

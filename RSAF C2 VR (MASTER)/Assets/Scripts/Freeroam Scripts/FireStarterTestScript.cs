using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FireStarterTestScript : MonoBehaviour
{
    public GameObject[] FlammableTargets;

    private GameObject selectedOption;

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

    public void FlameSelector()
    {
        List<GameObject> options = new List<GameObject>(FlammableTargets);

        Random random = new Random();
        int selectedIndex = random.Next(options.Count);
        selectedOption = options[selectedIndex];

        StartCoroutine(ActivateObjectsAfterDelay(selectedOption, 2.0f));
    }

    private IEnumerator ActivateObjectsAfterDelay(GameObject parentObject, float delay)
    {
        FireTick.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);

        Debug.Log("Sparks Started");

        parentObject.SetActive(true);

        if (parentObject.transform.childCount > 0)
        {
            yield return new WaitForSeconds(15);

            Debug.Log("Smoke Started");

            Transform child = parentObject.transform.GetChild(0);
            child.gameObject.SetActive(true);

            if (child.childCount > 0)
            {
                yield return new WaitForSeconds(30);

                Debug.Log("Fire Started");

                Transform grandchild = child.GetChild(0);
                grandchild.gameObject.SetActive(true);

                yield return new WaitForSeconds(2.0f);

                if (FireTick.gameObject.activeSelf)
                {
                    yield return new WaitForSeconds(2.0f);
                    yield return StartCoroutine(FM200Sequence());
                    FireTick.gameObject.SetActive(false);
                }
                else
                {
                    yield break;
                }
            }
        }
    }

    private IEnumerator FM200Sequence()
    {
        foreach (GameObject target in FM200Targets)
        {
            PlayParticleEffect(target);
        }

        yield return new WaitForSeconds(5);

        PlayParticleEffect(FM200Area);
    }

    private void PlayParticleEffect(GameObject target)
    {
        ParticleSystem particleSystem = target.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play(); // Play the particle system
        }
        else
        {
            Debug.LogWarning("No particle system component found on the target: " + target.name);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScenarioFire : MonoBehaviour
{
    public GameObject[] FlammableTargets;
    private List<GameObject> selectedOptions = new List<GameObject>();
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
        selectedOptions.Clear();

        int numTargets = Random.Range(1, 4);

        List<GameObject> options = new List<GameObject>(FlammableTargets);

        for (int i = 0; i < numTargets; i++)
        {
            int selectedIndex = Random.Range(0, options.Count);
            GameObject selectedOption = options[selectedIndex];
            selectedOptions.Add(selectedOption);
            options.RemoveAt(selectedIndex);
        }

        Debug.Log("Process Started");
        StartCoroutine(ActivateTargets());
    }

    private IEnumerator ActivateTargets()
    {
        Debug.Log("Coroutine started");
        FireTick.SetActive(true);
        Debug.Log("Fire Tick Triggered");
        Debug.Log("Coroutine resumed after waiting");

        foreach (GameObject selectedOption in selectedOptions)
        {
            PlayParticleEffect(selectedOption);
            Debug.Log("Sparks Started");


            if (selectedOption.transform.childCount > 0)
            {
                yield return new WaitForSeconds(15);

                Debug.Log("Smoke Started");

                Transform child = selectedOption.transform.GetChild(0);
                PlayParticleEffect(child.gameObject); // Play particle effect on the child

                if (child.childCount > 0)
                {
                    yield return new WaitForSeconds(30);

                    Debug.Log("Fire Started");

                    Transform grandchild = child.GetChild(0);
                    PlayParticleEffect(grandchild.gameObject); // Play particle effect on the grandchild

                    // Check if grandchild has Fire_Flag child object
                    Transform fireFlag = grandchild.Find("Fire_Flag");
                    if (fireFlag != null)
                    {
                        fireFlag.gameObject.SetActive(true);
                        Debug.Log("Flag Raised");
                    }

                    yield return new WaitForSeconds(2.0f);
                }
            }
        }
    }

    private void PlayParticleEffect(GameObject target)
    {
        ParticleSystem particleSystem = target.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play(); // Play the particle system

            // Check if the target is the root object
            if (target.transform.parent == null)
            {
                return; // Skip playing the particle effect on children
            }
        }
        else
        {
            Debug.LogWarning("No particle system component found on the target: " + target.name);
        }
    }
}
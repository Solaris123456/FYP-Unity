using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RackFireStarter : MonoBehaviour
{
    public GameObject FlammableTarget1;
    public GameObject FlammableTarget2;
    public GameObject FlammableTarget3;
    public GameObject FlammableTarget4;
    public GameObject FlammableTarget5;
    public GameObject FlammableTarget6;

    private GameObject selectedOption;

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
        List<GameObject> options = new List<GameObject>
        {
            FlammableTarget1,
            FlammableTarget2,
            FlammableTarget3,
            FlammableTarget4,
            FlammableTarget5,
            FlammableTarget6
        };

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
            }
        }
    }
}

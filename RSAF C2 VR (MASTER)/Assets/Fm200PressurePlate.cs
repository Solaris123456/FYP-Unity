using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fm200PressurePlate : MonoBehaviour
{
    public GameObject FM200UI;
    // Start is called before the first frame update
    private void Start()
    {
        if (FM200UI.activeSelf)
        {
            FM200UI.SetActive(false);
        }
    }
    void OnTriggerEnter (Collider other)
    {
      FM200UI.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
      FM200UI.SetActive(false);
    }
}

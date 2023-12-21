using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safetyStarter : MonoBehaviour
{
    public bool Light;
    public bool Panel;
    public GameObject Lighting;
    public GameObject Ceiling;
    // Start is called before the first frame update
    void Start()
    {
        if (Light)
        {
            Lighting.SetActive(true);
        }
        if (Panel)
        {
            Ceiling.SetActive(true);
        }
    }
}

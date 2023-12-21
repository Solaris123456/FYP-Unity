using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingBroken : MonoBehaviour
{
    public GameObject TargetToDisable;
    // Start is called before the first frame update
    void Start()
    {
        TargetToDisable.SetActive(false);
    }
}

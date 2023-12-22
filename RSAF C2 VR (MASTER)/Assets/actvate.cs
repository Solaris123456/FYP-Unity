using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actvate : MonoBehaviour
{
    public GameObject activator;
    // Start is called before the first frame update
    void Start()
    {
       activator.SetActive(true);   
    }
}

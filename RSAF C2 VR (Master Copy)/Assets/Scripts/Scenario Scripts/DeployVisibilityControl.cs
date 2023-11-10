using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployVisibilityControl : MonoBehaviour
{
    public GameObject[] TargetElement;
    public GameObject ReferenceElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ReferenceElement.activeSelf)
        {
            foreach (GameObject target in TargetElement)
            {
                target.SetActive(false);
            }
        }
    }
}

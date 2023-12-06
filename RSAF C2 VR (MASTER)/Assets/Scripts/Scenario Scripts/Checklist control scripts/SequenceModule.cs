using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceModule : MonoBehaviour
{
    public GameObject SequenceTarget;
    public GameObject PreviousFlag;
    public GameObject NextFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SequenceStep()
    {
        if (PreviousFlag.activeSelf)
        {
            yield return new WaitForSeconds(3);
            SequenceTarget.SetActive(true);
            NextFlag.SetActive(true);
        }
    }
}

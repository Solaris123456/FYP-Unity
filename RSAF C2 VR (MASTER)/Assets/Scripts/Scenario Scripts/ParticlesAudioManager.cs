using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAudioManager : MonoBehaviour
{
    public GameObject Target;
    public GameObject Reference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Reference.activeSelf)
        {
            Target.SetActive(true);
        }

        if (!Reference.activeSelf)
        {
            Target.SetActive(false);
        }
    }
}

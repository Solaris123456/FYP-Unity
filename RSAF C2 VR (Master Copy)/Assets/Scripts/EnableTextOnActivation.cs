using UnityEngine;
using UnityEngine.UI;

public class EnableTextOnActivation : MonoBehaviour
{
    public GameObject referenceGameObject;
    public GameObject textObject;
    public bool triggered = false;
    private void Start()
    {

    }

    private void Update()
    {
        // Check if the reference game object is active
        if (referenceGameObject.activeSelf && triggered == false)
        {
            // Enable the text object
            textObject.SetActive(true);
            triggered = true;
        }
    }
}
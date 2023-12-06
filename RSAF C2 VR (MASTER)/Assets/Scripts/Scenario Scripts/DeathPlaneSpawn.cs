using UnityEngine;

public class DeathPlaneSpawn : MonoBehaviour
{
    //public Collider PlayerHitboxTrigger;         // The object that triggers the activation
    public GameObject AirTag;      // The object to activate when collision occurs

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathPlane Trigger"))
        {
            // Activate the object
            AirTag.SetActive(true);
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DeathPlane Trigger"))
        {
            // Deactivate the object
            AirTag.SetActive(false);
            Debug.Log("left");
        }
    }
}
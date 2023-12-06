using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class ExitTeleport : MonoBehaviour
{
    public GameObject FM200Checker;
    public GameObject DeathPlane;


    public GameObject player; // Reference to the player object

    private bool isZoneInside = false;
    private float collisionTime = 0f;
    private float requiredDuration = 10f;
    private bool hasMetCondition = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == DeathPlane)
        {
            isZoneInside = true;
            collisionTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == DeathPlane)
        {
            isZoneInside = false;
            if (!hasMetCondition)
            {
                collisionTime = 0f; // Reset the collision time if the zone exits before meeting the condition
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isZoneInside && !hasMetCondition)
        {
            Debug.Log("player entered FM200 radius");
            float currentTime = Time.time;
            float elapsedTime = currentTime - collisionTime;

            if (elapsedTime >= requiredDuration)
            {
                DeathPlane.SetActive(true);
                Debug.Log("Player has stayed in the FM200 for 10 seconds, suffocated");
                hasMetCondition = true;
            }
        }
    }
}

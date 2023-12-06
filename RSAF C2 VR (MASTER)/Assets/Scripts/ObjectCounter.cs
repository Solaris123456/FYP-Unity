using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    private int objectCount;

    void Start()
    {
        CountObjects();
        Debug.Log("Number of Objects in the Scene: " + objectCount);
    }

    void CountObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        objectCount = allObjects.Length;
    }
}
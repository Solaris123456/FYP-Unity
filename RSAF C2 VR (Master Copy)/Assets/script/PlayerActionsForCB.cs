using TMPro;
using UnityEngine;

public class PlayerActionsForCB : MonoBehaviour
{
    public GameObject CB;
    public void OnUse()
    {
        TryGetComponent<Door>(out Door door);

        if (door.IsOpen)
        {
            door.Close();
            CB.SetActive(false);

        }
        else
        {
            door.Open(transform.position);
            CB.SetActive(true);
        }
    }
}
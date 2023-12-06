using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public void OnUse()
    {
        TryGetComponent<Door>(out Door door);

        if (door.IsOpen)
        {
            door.Close();
        }
        else
        {
            door.Open(transform.position);
        } 
    }
}
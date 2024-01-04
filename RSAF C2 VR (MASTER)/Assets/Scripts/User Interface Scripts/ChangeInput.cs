using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;


public class ChangeInput : MonoBehaviour
{
    public EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    public Button registerButton;
    public int Selected_UI;
    public List<GameObject> UI_list;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selected_UI--;

            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
            Debug.Log(system.currentSelectedGameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selected_UI++;
            Selectable next;
            if (system.currentSelectedGameObject)
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            else
                next = system.firstSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
            Debug.Log(system.currentSelectedGameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //user 0 pwd 1 enter 2 register 3 quit 4
        {
            /*
            if (Selected_UI == 2) // submit
            {
                Debug.Log("#dis shit btr happen once if noot");
                submitButton.onClick.Invoke(); }
            else if (Selected_UI == 3) // register
            {
                registerButton.onClick.Invoke();
            }
            */
        }
    }
}

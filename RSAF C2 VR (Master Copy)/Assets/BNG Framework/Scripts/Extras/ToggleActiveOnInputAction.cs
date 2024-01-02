using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG 
{

    /// <summary>
    /// This script will toggle a GameObject whenever the provided InputAction is executed
    /// </summary>
    public class ToggleActiveOnInputAction : MonoBehaviour {

        public InputActionReference InputAction = default;
        //public InputActionProperty menubutton;
        public GameObject ToggleObject;

        /*void Update()
        {
            if (menubutton.action.WasPressedThisFrame() || menubutton.action.WasPerformedThisFrame())
            {
                if (!ToggleObject)
                {
                    ToggleObject.SetActive(ToggleObject.activeSelf);
                }
            }
            if (menubutton.action.WasPerformedThisFrame())
            {
                if (ToggleObject)
                {
                    ToggleObject.SetActive(!ToggleObject.activeSelf);
                }
            }
        }*/

        private void OnEnable()
        {
            InputAction.action.performed += ToggleActive;
        }

        private void OnDisable()
        {
            InputAction.action.performed -= ToggleActive;
        }

        public void ToggleActive(InputAction.CallbackContext context) {
            if (ToggleObject) {
                ToggleObject.SetActive(!ToggleObject.activeSelf);
            }
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitVerifyScript : MonoBehaviour
{
    public GameObject FM200Checker;
    public GameObject[] burnFlags;
    public GameObject[] fireFlags;
    public GameObject breakerBegin;
    public GameObject fireTick;
    public GameObject Exitflag;

    private void Start()
    {

    }

    public void VerifyExit()
    {
        StartCoroutine(CheckConditions());
    }

    public IEnumerator CheckConditions()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            bool allConditionsMet = FM200Checker.activeSelf
                && AreFlagsInactive(burnFlags)
                && AreFlagsInactive(fireFlags)
                && breakerBegin.activeSelf
                && !fireTick.activeSelf;

            if (allConditionsMet)
            {
                // Perform your desired actions or code here
                Debug.Log("All conditions met, exit initialising");
                Exitflag.SetActive(true);
                // Break out of the loop to stop checking conditions
                break;
            }
        }
    }

    private bool AreFlagsInactive(GameObject[] flags)
    {
        foreach (GameObject flag in flags)
        {
            if (flag.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}

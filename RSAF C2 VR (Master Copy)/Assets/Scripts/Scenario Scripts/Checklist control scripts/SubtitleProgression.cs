using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubtitleProgression : MonoBehaviour
{
    [SerializeField] GameObject referenceObject;
    [SerializeField] GameObject previousTarget;
    [SerializeField] GameObject targetObject;
    [SerializeField] float delayBeforeActivation = 0f;
    [SerializeField] float delayBeforeDeactivation = 0f;

    private bool targetActivated = false;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => referenceObject.activeSelf);
            if (!targetActivated)
            {
                yield return new WaitForSeconds(delayBeforeActivation);
                if (previousTarget != null)
                {
                    previousTarget.SetActive(false);
                }
                targetObject.SetActive(true);
                targetActivated = true;
                StartCoroutine(DeactivateTargetAfterDelay());
            }
        }
    }

    private IEnumerator DeactivateTargetAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDeactivation);
        targetObject.SetActive(false);
    }

}
using System.Collections;
using UnityEngine;

public class StartBurn : MonoBehaviour
{
    public GameObject spark;
    public GameObject smoke;
    public GameObject fire;
    public GameObject BurnFlag;
    public GameObject FireFlag;

    private bool hasStartedBurn = false;

    public int sparktime = 5;
    public int smoketime = 20;
    public int firetime = 40;

    // Update is called once per frame
    void Update()
    {
        if (BurnFlag.activeSelf && !hasStartedBurn)
        {
            hasStartedBurn = true;
            StartCoroutine(BurnProcedure());
        }
        else
        {
            return;
        }
    }

    public IEnumerator BurnProcedure()
    {
        yield return new WaitForSeconds(sparktime);
        spark.SetActive(true);
        Debug.Log("Sparks Started");
        yield return new WaitForSeconds(smoketime);
        smoke.SetActive(true);
        Debug.Log("Smoke Started");
        yield return new WaitForSeconds(firetime);
        if (BurnFlag.activeSelf)
        {
            fire.SetActive(true);
            FireFlag.SetActive(true);
            Debug.Log("Fire Started");
        }
    }
}

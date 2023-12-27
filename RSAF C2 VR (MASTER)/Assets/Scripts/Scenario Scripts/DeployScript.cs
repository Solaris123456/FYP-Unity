using UnityEngine;

public class DeployScript : MonoBehaviour
{
    public GameObject FM200Checker;

    public GameObject VoiceRecog;
    public GameObject ExitUI;

    public GameObject DeployExitUI;
    public GameObject DeployChecklist;
    public GameObject DeployChatlog;
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (!FM200Checker.activeSelf)
            {
                VoiceRecog.SetActive(false);
                ExitUI.SetActive(false);

                DeployExitUI.SetActive(true);
                DeployChecklist.SetActive(true);
                DeployChatlog.SetActive(true);
                done = true;
            }
        }
    }
}

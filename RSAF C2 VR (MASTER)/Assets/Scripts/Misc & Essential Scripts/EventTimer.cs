using TMPro;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    public GameObject TimerActivationFlag;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI MillisecondsText;
    public TextMeshProUGUI MillisecondsText2;
    public float RecordedTime;
    public float Timer;

    // Start is called before the first frame update
    void Start()
    {
        RecordedTime = 0;
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActivationFlag.activeSelf)
        {
            Timer += Time.deltaTime;
            DisplayTime(Timer);
            RecordedTime = Timer;
        }
        else
        {
            DisplayTime(RecordedTime);

            Debug.Log(RecordedTime);
            Timer = 0;
        }
    }

    public void DisplayTime(float Timing)
    {
        var minutes = Mathf.FloorToInt(Timing / 60);
        var seconds = Mathf.FloorToInt(Timing - minutes * 60);
        string gameTimeClockDisplay = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.text = gameTimeClockDisplay;

        var miliseconds = Mathf.FloorToInt((Timing - minutes * 60 - seconds) * 1000);
        string milisecondsDisplay = string.Format("{0:000}", miliseconds);
        MillisecondsText.text = milisecondsDisplay;
        MillisecondsText2.text = MillisecondsText.text;
    }
}

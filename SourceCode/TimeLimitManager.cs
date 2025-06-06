using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLimitManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private float timeLimit = 60;

    private const float oneMinute = 60;
    private float minutes;
    private float seconds;
    private float displayTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        seconds = Mathf.Floor(Mathf.Repeat(timeLimit, oneMinute));
        minutes = Mathf.Floor((timeLimit) / oneMinute);
        timerTMP.SetText("Time" + minutes.ToString("f0") + ":" + seconds.ToString("00"));
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLimit > 0 && GameState.Instance.NowState == GameState.State.play)
        {
            timeLimit -= Time.deltaTime;
            seconds = Mathf.Floor(Mathf.Repeat((timeLimit + displayTime), oneMinute));
            minutes = Mathf.Floor((timeLimit + displayTime) / oneMinute);
            timerTMP.SetText("Time" + minutes.ToString("f0") + ":" + seconds.ToString("00"));
        }
        else if(timeLimit <=0)
        {
            ResultManager.Instance.GameOver();
        }
    }
}

using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float time = 0.00f;
    private bool timerEnabled;

    private void OnEnable()
    {
        PlayerCollision.WinEvent += EndTimer;
        PlayerCollision.PlayerLostEvent += ResetTimer;
        PlayerCollision.RestartEvent += ResetTimer;
        StartLevelScript.StartEvent += StartTimer;
    }

    private void OnDisable()
    {
        PlayerCollision.WinEvent -= EndTimer;
        PlayerCollision.PlayerLostEvent -= ResetTimer;
        PlayerCollision.RestartEvent -= ResetTimer;
        StartLevelScript.StartEvent -= StartTimer;
    }

    private void StartTimer()
    {
        timerEnabled = true;
    }

    private void ResetTimer()
    {
        time = 0.00f;
        timerText.text = "Time: " + time + "s";
        EndTimer();
    }

    private void EndTimer()
    {
        timerEnabled = false;
    }

    private void Update()
    {
        if (timerEnabled) {
            time += Time.deltaTime;
            timerText.text = "Time: " + time.ToString("#.00") + "s";
        }
    }
}

using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float time = 0.00f;
    private bool timerEnabled;

    private void Start()
    {
        PlayerCollision.WinEvent += EndTimer;
        PlayerCollision.PlayerLostEvent += ResetTimer;
        PlayerCollision.RestartEvent += ResetTimer;
        StartLevelScript.StartEvent += StartTimer;
    }

    private void StartTimer()
    {
        timerEnabled = true;
        StartCoroutine(TimerCoroutine());
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

    private IEnumerator TimerCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        while (timerEnabled) {
            time += 0.01f;
            timerText.text = "Time: " + time.ToString("#.00") + "s";
            yield return wait;
        }
    }
}

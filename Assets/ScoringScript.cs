using System;
using System.Globalization;
using TMPro;
using UnityEngine;
public class ScoringScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private TextMeshProUGUI textObject = default;
    
    private bool plrLost = false;
    
    private void Update() {
        if (!plrLost) { textObject.text = Math.Round(playerTransform.position.z, 0).ToString(CultureInfo.CurrentCulture); }
    }
    
    private void OnEnable() {
        PlayerCollision.PlayerLostEvent += PlayerLostEvent;
        RespawnScript.RespawnEvent += PlayerLostEvent;
        PlayerCollision.RestartEvent += PlayerRestartEvent;
        PlayerCollision.WinEvent += PlayerLostEvent;
    }

    private void OnDisable() {
        PlayerCollision.PlayerLostEvent -= PlayerLostEvent;
        PlayerCollision.RestartEvent -= PlayerRestartEvent;
        PlayerCollision.WinEvent -= PlayerLostEvent;
        RespawnScript.RespawnEvent -= PlayerLostEvent;
    }

    private void PlayerRestartEvent() {
        plrLost = false;
    }
    
    private void PlayerLostEvent() {
        plrLost = true;
    }
    
}

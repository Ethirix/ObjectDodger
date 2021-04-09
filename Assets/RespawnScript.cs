using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text restartText;

    public delegate void RsEvent();
    public static event RsEvent RespawnEvent;
    private void OnEnable() {
        btn.onClick.AddListener(FireRsEvent);
        PlayerCollision.RestartEvent += ButtonEnable;
    }

    private void OnDisable() {
        btn.onClick.RemoveListener(FireRsEvent);
        PlayerCollision.RestartEvent -= ButtonEnable;
    }

    private void ButtonEnable() {
        btn.enabled = true;
    }
    
    private void FireRsEvent() {
        if (!restartText.IsActive()) {
            btn.enabled = false;
            RespawnEvent?.Invoke();
        }
    }
}

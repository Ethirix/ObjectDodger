using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StartLevelScript : MonoBehaviour
{

    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private TMP_Text startText;
    [SerializeField] private float startDelay;

    public delegate void StartEv();
    public static event StartEv StartEvent;
    
    private bool enableStart = true;
    
    private void OnEnable()
    {
        PlayerCollision.RestartEvent += SetEnableStart;
    }
    private void OnDisable()
    {
        PlayerCollision.RestartEvent -= SetEnableStart;
    }

    private void SetEnableStart()
    {
        enableStart = true;
        playerMovementController.enabled = false;
        startText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && enableStart) {
            startText.gameObject.SetActive(false);
            enableStart = false;
            StartEvent?.Invoke();
            StartCoroutine(RunStartWait());
        }
    }

    private IEnumerator RunStartWait()
    {
        WaitForSeconds wait = new WaitForSeconds(startDelay);
        yield return wait;
        playerMovementController.enabled = true;
    }
}

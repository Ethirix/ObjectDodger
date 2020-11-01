using System;
using TMPro;
using UnityEngine;

public class ScoringScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMovementController movementScript;
    [SerializeField] private TextMeshProUGUI textObject;
    
    void Update()
    {
        if (movementScript.enabled) { textObject.text = Math.Round(playerTransform.position.z, 0).ToString(); }
    }
}

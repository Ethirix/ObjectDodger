using System;
using TMPro;
using UnityEngine;
public class ScoringScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private PlayerMovementController movementScript = default;
    [SerializeField] private TextMeshProUGUI textObject = default;
    
    void Update()
    {
        if (movementScript.enabled) { textObject.text = Math.Round(playerTransform.position.z, 0).ToString(); }
    }
}

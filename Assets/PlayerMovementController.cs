using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Base Values")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardForce;
    [SerializeField] private float directionalForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject floor;
    
    [Header("Multipliers")]
    [SerializeField] private float jumpSpeedMultiplier;
    [SerializeField] private float moveSideSpeedMultiplier;
    [SerializeField] private float forwardSpeedMultiplier;
    [SerializeField] private float backwardSpeedMultiplier;
    [SerializeField] private float jumpSideMoveSpeedMultiplier;
    
    [Header("Options")]
    [SerializeField] private bool allowJumpMovement;

    private bool isOnGround;
    private delegate void JumpEvent();
    private JumpEvent jumpEvent;
    private bool jumpTimeout;
    
    private void OnEnable() {
        jumpEvent += JumpEventFired;
    }
    private void OnDisable() {
        jumpEvent -= JumpEventFired;
    }

    private void FixedUpdate() {
        rb.AddForce(Movement(), ForceMode.Force);
    }

    private Vector3 Movement() {
        Vector3 force = new Vector3();

        if (isOnGround) { force.z = forwardForce;
        } else { force.z = forwardForce * jumpSpeedMultiplier; }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (isOnGround) { force.z *= forwardSpeedMultiplier;
            } else if (!isOnGround && allowJumpMovement) { force.z *= forwardSpeedMultiplier * jumpSpeedMultiplier; }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (isOnGround) { force.z *= backwardSpeedMultiplier;
            } else if (!isOnGround && allowJumpMovement) { force.z *= backwardSpeedMultiplier * jumpSpeedMultiplier; }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            if (isOnGround) { 
                force.x = -directionalForce;
                force.z *= moveSideSpeedMultiplier;
            } else if (!isOnGround && allowJumpMovement) {
                force.x = -directionalForce * jumpSpeedMultiplier;
                force.z *= jumpSideMoveSpeedMultiplier;
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            if (isOnGround) {
                force.x = directionalForce;
                force.z *= moveSideSpeedMultiplier;
            } else if (!isOnGround && allowJumpMovement) {
                force.x = directionalForce * jumpSpeedMultiplier;
                force.z *= jumpSideMoveSpeedMultiplier;
            }
        }
        
        if (Input.GetKey(KeyCode.Space) && isOnGround && !jumpTimeout) {
            force.y = -Physics.gravity.y * rb.mass * jumpForce;
            StartCoroutine(InvokeJumpEvent());
        }
        return force;
    }

    IEnumerator InvokeJumpEvent() {
        jumpEvent.Invoke();
        yield return null;
    }
    
    private void JumpEventFired() {
        jumpTimeout = true;
        Invoke(nameof(ReenableJump), 0.1f);
    }

    private void ReenableJump() {
        jumpTimeout = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == floor) { isOnGround = true; }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == floor) { isOnGround = false; }
    }
}

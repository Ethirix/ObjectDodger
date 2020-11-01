using UnityEngine;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = default;
    [SerializeField] private float forwardForce = default;
    [SerializeField] private float directionalForce = default;
    [SerializeField] private float jumpForce = default;
    [SerializeField] private GameObject floor = default;

    private bool _isOnGround;
    private void FixedUpdate()
    {
        if (_isOnGround) { rb.AddForce(0f, 0f, forwardForce); } else { rb.AddForce(0f, 0f, forwardForce * 0.5f); }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && _isOnGround) { rb.AddForce(-directionalForce, 0f, 0f, ForceMode.VelocityChange); }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && _isOnGround) { rb.AddForce(directionalForce, 0f, 0f, ForceMode.VelocityChange); }
        
        //if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && _isOnGround) { rb.AddForce(0f, 0f, forwardForce); }
        //if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && _isOnGround) { rb.AddForce(0f, 0f, -forwardForce); }
        
        if (Input.GetKey(KeyCode.Space) && _isOnGround) { rb.AddForce(0f, 9.81f * rb.mass * jumpForce, 0f, ForceMode.VelocityChange); }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == floor) { _isOnGround = true; }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == floor) { _isOnGround = false; }
    }
}

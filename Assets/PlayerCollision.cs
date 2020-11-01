using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementScript;
    private bool _restartInProgress = false;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            movementScript.enabled = false;
            if (!_restartInProgress)
            {
                _restartInProgress = true; 
                Invoke(nameof(RestartTimer), 3.0f); 
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            if (!_restartInProgress)
            {
                _restartInProgress = true;
                Invoke(nameof(RestartTimer), 3.0f);
            }
        }
    }

    private void RestartLogic()
    {
        if (!movementScript.enabled) { movementScript.enabled = true;}
        
        Transform playerTransform = transform;
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        playerTransform.position = new Vector3(0f, 1f, 0f);
        playerTransform.rotation = Quaternion.Euler(0,0,0);

        rb.velocity = new Vector3(0f,0f,0f);
        rb.angularVelocity = new Vector3(0f,0f,0f);

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject gO in obstacles)
        {
            PositionResetter pR = gO.GetComponent<PositionResetter>();
            pR.ResetPosition();
        }
        _restartInProgress = false;
    }
    
    private void RestartTimer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


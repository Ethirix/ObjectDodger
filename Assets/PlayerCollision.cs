using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void PlayerLost();
    public static event PlayerLost PlayerLostEvent;
    [SerializeField] private PlayerMovementController movementScript = default;
    private bool _restartInProgress = false;
    private bool _playerWon = false;

    public delegate void Restart();
    public static event Restart RestartEvent;

    public delegate void WinEv();
    public static event WinEv WinEvent;
    private void OnEnable() {
        RespawnScript.RespawnEvent += RespawnEventFired;
    }
    private void OnDisable() {
        RespawnScript.RespawnEvent -= RespawnEventFired;
    }

    private void RespawnEventFired() {
        movementScript.enabled = false;
        if (!_restartInProgress) {
            _restartInProgress = true;
            Invoke(nameof(RestartTimer), 3.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            movementScript.enabled = false;
            if (!_restartInProgress && !_playerWon) {
                PlayerLostEvent?.Invoke();
                _restartInProgress = true;
                Invoke(nameof(RestartTimer), 3.0f); 
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn")) {
            if (!_restartInProgress && !_playerWon) {
                PlayerLostEvent?.Invoke();
                _restartInProgress = true;
                Invoke(nameof(RestartTimer), 3.0f);
            }
        } else if (other.gameObject.CompareTag("Finish")) {
            if (!_restartInProgress) {
                WinEvent?.Invoke();
                _playerWon = true;
                movementScript.enabled = false;
            }
        }
    }
    
    private void RestartLogic()
    {
        movementScript.enabled = true;
        _playerWon = false;
     
        Transform playerTransform = transform;
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        playerTransform.position = new Vector3(0f, 1f, 0f);
        playerTransform.rotation = Quaternion.Euler(0,0,0);

        rb.velocity = new Vector3(0f,0f,0f);
        rb.angularVelocity = new Vector3(0f,0f,0f);

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject gO in obstacles) {
            PositionResetter pR = gO.GetComponent<PositionResetter>();
            pR.ResetPosition();
        }
        RestartEvent?.Invoke();
        _restartInProgress = false;
    }
    
    private void RestartTimer()
    {
        RestartLogic();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


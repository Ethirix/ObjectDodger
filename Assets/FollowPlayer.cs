using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotOffset;

    void Update()
    {
        transform.position = playerTransform.position + posOffset;
        transform.rotation = Quaternion.Euler(rotOffset);
    }
}

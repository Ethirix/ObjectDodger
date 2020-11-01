using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private Vector3 posOffset = default;
    [SerializeField] private Vector3 rotOffset = default;

    void Update()
    {
        transform.position = playerTransform.position + posOffset;
        transform.rotation = Quaternion.Euler(rotOffset);
    }
}

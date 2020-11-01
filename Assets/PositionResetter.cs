using UnityEngine;

public class PositionResetter : MonoBehaviour
{
    private Vector3 _defaultPos;
    private Vector3 _defaultRot;
    private Transform _objTransform;
    void Start()
    {
        _objTransform = transform;
        _defaultPos = _objTransform.position;
        _defaultRot = _objTransform.rotation.eulerAngles;
    }
    public void ResetPosition()
    {
        if (_objTransform.position != _defaultPos) { _objTransform.position = _defaultPos; }
        if (_objTransform.rotation != Quaternion.Euler(_defaultRot)) { _objTransform.rotation = Quaternion.Euler(_defaultRot); }
    }
}

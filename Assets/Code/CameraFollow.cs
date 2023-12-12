using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _offsetZ;
    private Vector3 targetDir;

    void LateUpdate()
    {
        targetDir = new Vector3(0, transform.position.y, _followTarget.position.z + _offsetZ);

        transform.position = targetDir;
    }
}
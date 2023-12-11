using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    private float _offsetZ;
    private Vector3 targetDir;

    /// <summary>
    /// Kameranýn baslangictaki Z'si offseti oluyor.
    /// </summary>
    private void Start()
    {
        _offsetZ = transform.position.z;
    }

    void LateUpdate()
    {
        targetDir = new Vector3(0, transform.position.y, _followTarget.position.z + _offsetZ);

        transform.position = targetDir;
    }
}
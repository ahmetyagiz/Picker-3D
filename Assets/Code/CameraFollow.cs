using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _offsetZ;
    public Vector3 targetDir;
    public bool follow = true;

    public bool turnDefault = true;
    public bool turnLeft;
    public bool turnRight;

    void LateUpdate()
    {
        if (follow)
        {
            if (turnDefault)
            {
                targetDir = new Vector3(0, transform.position.y, _followTarget.position.z + _offsetZ);
            }
            else if (turnLeft)
            {
                targetDir = new Vector3(_followTarget.position.x - _offsetZ, transform.position.y, 95);
            }
            else if (turnRight)
            {
                targetDir = new Vector3(-114, transform.position.y, _followTarget.position.z + _offsetZ);
            }

            transform.position = targetDir;
        }

    }

    public void TurnLeft()
    {
        follow = false;
        transform.parent = _followTarget;
    }

    public void TurnRight()
    {
        follow = false;
        transform.parent = _followTarget;
    }
}
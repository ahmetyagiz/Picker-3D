using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 18.18f, -16);
    public bool follow;

    Vector3 defaultDir;
    Vector3 rightDir;
    Vector3 leftDir;

    bool isRight;
    bool isLeft;
    Transform rotatePiv;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null && follow)
        {
            if (isRight)
            {
                rightDir = new Vector3(target.position.x -15, defaultDir.y, rotatePiv.transform.position.z);
                transform.position = rightDir;
            }
            else if (isLeft)
            {
                leftDir = new Vector3(rotatePiv.transform.position.x, defaultDir.y, target.position.z - 15);
                transform.position = leftDir;
            }
            else
            {
                defaultDir = target.position + offset;
                defaultDir.x = 0;
                transform.position = defaultDir;
            }
        }
    }

    public void TurnLeft(Transform rotatePivot)
    {
        follow = false;
        isRight = false;
        isLeft = true;
        transform.DOMove(new Vector3(rotatePivot.position.x, defaultDir.y, rotatePivot.position.z - 5), 1.5f);

        transform.parent = rotatePivot;

        rotatePivot.transform.DORotate(new Vector3(0, -90, 0), 1.5f, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            transform.parent = null;
            follow = true;
        });

        rotatePiv = rotatePivot;
    }

    public void TurnRight(Transform rotatePivot)
    {
        follow = false;
        isLeft = false;
        isRight = true;
        transform.DOMove(new Vector3(rotatePivot.position.x - 5, defaultDir.y, rotatePivot.position.z), 1.5f);

        transform.parent = rotatePivot;

        rotatePivot.transform.DORotate(new Vector3(0, 90, 0), 1.5f, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            transform.parent = null;
            follow = true;
        });

        rotatePiv = rotatePivot;
    }
}
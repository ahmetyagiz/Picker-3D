using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private GameObject dropObject;
    [SerializeField] private GameObject dropperModel;
    [SerializeField] private SplineFollower splineFollower;
    public bool isDropping;

    public IEnumerator DropObjectRoutine()
    {
        splineFollower.follow = true;
        isDropping = true;

        while (isDropping)
        {
            Vector3 dropPoint = new Vector3(dropperModel.transform.position.x, 10 /* spline offseti alabilirim*/, dropperModel.transform.position.z);
            Instantiate(dropObject, dropPoint, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StopDropObject()
    {
        isDropping = false;
        transform.DOMoveY(transform.position.y + 30, 1f);
    }
}
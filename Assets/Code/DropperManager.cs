using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private GameObject dropObject;
    [SerializeField] private GameObject dropperModel;
    [SerializeField] private SplineFollower splineFollower;
    
    int sayi;

    public IEnumerator DropObjectRoutine()
    {
        splineFollower.follow = true;

        while (sayi < 10)
        {
            Vector3 dropPoint = new Vector3(dropperModel.transform.position.x, 10 /* spline offseti alabilirim*/, dropperModel.transform.position.z);
            Instantiate(dropObject, dropPoint, Quaternion.identity);
            sayi++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using UnityEngine;

public enum DropObject
{
    Cube,
    Sphere,
    Cone
}

public class DropperManager : MonoBehaviour
{
    [Header("Object Prefabs")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject conePrefab;

    [Header("Select Drop Object")]
    public DropObject dropObject;

    [Header("Pathing")]
    [SerializeField] private GameObject dropperModel;
    [SerializeField] private SplineFollower splineFollower;
    public bool isDropping;
    private GameObject selectedDropObject;

    private void Start()
    {
        switch (dropObject)
        {
            case DropObject.Cube:
                selectedDropObject = cubePrefab;
                break;
            case DropObject.Sphere:
                selectedDropObject = spherePrefab;
                break;
            case DropObject.Cone:
                selectedDropObject = conePrefab;
                break;
        }
    }

    public IEnumerator DropObjectRoutine()
    {
        splineFollower.follow = true;
        isDropping = true;

        while (isDropping)
        {
            Vector3 dropPoint = new Vector3(dropperModel.transform.position.x, 10 /* spline offseti alabilirim*/, dropperModel.transform.position.z);
            Instantiate(selectedDropObject, dropPoint, Quaternion.identity);
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void StopDropObject()
    {
        //Debug.Log("Yol bitti");
        splineFollower.follow = false;
        isDropping = false;
        //dropperModel.transform.position = new Vector3(dropperModel.transform.position.x, 10, dropperModel.transform.position.z) ;
        ////splineFollower.follow = false;
        transform.DOMoveY(transform.position.y + 30, 1f);
    }
}
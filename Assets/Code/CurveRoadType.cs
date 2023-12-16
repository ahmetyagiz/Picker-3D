using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveRoadType : MonoBehaviour
{
    public enum TurnType
    {
        Left,
        Right
    }

    public TurnType turnType;

    void sayi()
    {
        if(TurnType.Left == turnType)
        {

        }
    }
}

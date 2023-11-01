using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUtility
{
    public const int MAX_SIZE_ID = 11;

    public static bool IsOverSize(int size)
    {
        return (MAX_SIZE_ID <= size);
    }

    public static Vector3 CalculateMidPosition(Vector3 _p1, Vector3 _p2)
    {
        return (_p1 + _p2) / 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{

    private static InGameManager instance;
    public static InGameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnClickEvent_ResetBalls()
    {
        var balls = BallManager.Instance.BallInstances;

        foreach (var b in balls)
        {
            Destroy(b);
        }
    }
}

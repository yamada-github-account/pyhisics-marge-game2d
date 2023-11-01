using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotate : MonoBehaviour
{
    public float minAngle = -5f;  // 最小回転角度（起動時の角度からの偏差）
    public float maxAngle = 5f;   // 最大回転角度（起動時の角度からの偏差）
    public bool isRotationLocked = false;  // 回転をロックするかどうか

    private float initialRotationZ; // 起動時のZ軸回転角度

    void Start()
    {
        // 起動時のZ軸回転角度を保存
        initialRotationZ = transform.eulerAngles.z;
    }

    void Update()
    {
        if (isRotationLocked)
        {
            // 回転を完全にロック
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, initialRotationZ);
        }
        else
        {
            // 現在のZ軸回転角度を取得
            float currentRotationZ = transform.eulerAngles.z;

            // 360度以上の角度を考慮する
            float deltaRotationZ = Mathf.DeltaAngle(initialRotationZ, currentRotationZ);

            // MinMax範囲を超えた場合に修正
            if (deltaRotationZ < minAngle)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, initialRotationZ + minAngle);
            }
            else if (deltaRotationZ > maxAngle)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, initialRotationZ + maxAngle);
            }
        }
    }
}

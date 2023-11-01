using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    [SerializeField]
    private GameObject rootBone = null;
    public Vector3 RootBonePosition => rootBone.transform.position;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    public int uId = 0;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    public int sizeId = 0;

    private bool isDestroyed = false;
    public bool IsDestroyed => isDestroyed;

    private void Start()
    {
        // 自身の固有番号を生成（重複なし）
        uId = BallManager.Instance.GetUniqueID();
    }

    public void DestroyInCollision()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"OnDestroy by id={uId}({gameObject.name})");
    }
}

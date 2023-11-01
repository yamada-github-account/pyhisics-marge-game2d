using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public List<float> possibleSizes; // インスペクターから設定する可能なサイズ（float型）
    public List<GameObject> ballPrefabs;

    [SerializeField]
    private bool useScroll;
    private int debugSizeId;
    public int DebugSizeId => debugSizeId;
    private int debugMinSizeId = 0; // 下限
    private int debugMaxSizeId = 1; // 上限

    private static BallManager instance;
    public static BallManager Instance => instance;

    private int currentID = 0;

    private List<GameObject> ballInstances = new List<GameObject>();
    public List<GameObject> BallInstances => ballInstances;

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

        debugMaxSizeId = possibleSizes.Count - 1;
    }
    void Update()
    {
        if (useScroll)
        {
            DebugUpdateScroll();
        }

        // 左クリックされたときの処理
        if (Input.GetMouseButtonDown(0))
        {
            // カメラからマウスのポイントへのレイを投影する
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Z軸の位置（任意）
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // Z軸を0に設定（2Dゲームのため）
            worldPosition.z = 0;

            // ランダムなサイズを選択
            int sizeIndex = 0;
            if (possibleSizes.Count > 0)
            {
                //sizeIndex = Random.Range(0, possibleSizes.Count);
                sizeIndex = Random.Range(0, 5);
            }

            if (useScroll)
            {
                sizeIndex = debugSizeId;
            }

            // Prefabをインスタンス化（生成）
            CreateBall(worldPosition, sizeIndex, true);
        }
    }

    public int GetUniqueID()
    {
        return currentID++;
    }

    public GameObject CreateBall(Vector3 spawnPosition, int sizeId, bool isRotateRandom = false)
    {
        Quaternion randomRotation = Quaternion.identity;
        if (isRotateRandom)
        {
            // ランダムな角度を生成
            float randomAngle = Random.Range(0f, 360f);
            randomRotation = Quaternion.Euler(0f, 0f, randomAngle);
        }

        GameObject obj = Instantiate(ballPrefabs[sizeId], spawnPosition, randomRotation);

        if (obj.TryGetComponent<BallObject>(out var ballObj))
        {
            float size = possibleSizes[sizeId];
            obj.transform.localScale = new Vector3(size, size, 1);

            ballObj.sizeId = sizeId;
        }

        // 管理に追加
        ballInstances.Add(obj);

        return obj;
    }

    private void DebugUpdateScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // マウスホイールで上にスクロールした場合、値を増やす
        if (scroll > 0f)
        {
            if (debugSizeId < debugMaxSizeId) // 上限を超えないようにする
            {
                debugSizeId++;
            }

            Debug.Log("Current value: " + debugSizeId);
        }
        // マウスホイールで下にスクロールした場合、値を減らす
        else if (scroll < 0f)
        {
            if (debugSizeId > debugMinSizeId) // 下限を超えないようにする
            {
                debugSizeId--;
            }

            Debug.Log("Current value: " + debugSizeId);
        }
    }
}

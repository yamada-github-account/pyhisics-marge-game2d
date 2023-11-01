using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnClick : MonoBehaviour
{
    public GameObject prefabToInstantiate; // インスペクターから設定するPrefab
    public List<float> possibleSizes; // インスペクターから設定する可能なサイズ（float型）

    void Update()
    {
        // 左クリックされたときの処理
        if (Input.GetMouseButtonDown(0))
        {
            // カメラからマウスのポイントへのレイを投影する
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Z軸の位置（任意）
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // Z軸を0に設定（2Dゲームのため）
            worldPosition.z = 0;

            // ランダムな角度を生成
            float randomAngle = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

            // Prefabをインスタンス化（生成）
            GameObject newPrefab = Instantiate(prefabToInstantiate, worldPosition, randomRotation);

            // ランダムなサイズを選択して設定
            if (possibleSizes.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleSizes.Count);
                float randomSize = possibleSizes[randomIndex];
                newPrefab.transform.localScale = new Vector3(randomSize, randomSize, 1);
            }
        }
    }
}

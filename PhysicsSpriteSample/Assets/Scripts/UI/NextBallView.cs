using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextBallView : MonoBehaviour
{
    [SerializeField, Header("Next表示")]
    private TextMeshProUGUI debugNextBallIdText;
    private int beforeFrameId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beforeFrameId != BallManager.Instance.DebugSizeId)
        {
            debugNextBallIdText.text = $"{BallManager.Instance.DebugSizeId}";

            beforeFrameId = BallManager.Instance.DebugSizeId;

        }
    }
}

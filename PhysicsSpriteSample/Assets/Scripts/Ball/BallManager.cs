using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public List<float> possibleSizes; // �C���X�y�N�^�[����ݒ肷��\�ȃT�C�Y�ifloat�^�j
    public List<GameObject> ballPrefabs;

    [SerializeField]
    private bool useScroll;
    private int debugSizeId;
    public int DebugSizeId => debugSizeId;
    private int debugMinSizeId = 0; // ����
    private int debugMaxSizeId = 1; // ���

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

        // ���N���b�N���ꂽ�Ƃ��̏���
        if (Input.GetMouseButtonDown(0))
        {
            // �J��������}�E�X�̃|�C���g�ւ̃��C�𓊉e����
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Z���̈ʒu�i�C�Ӂj
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // Z����0�ɐݒ�i2D�Q�[���̂��߁j
            worldPosition.z = 0;

            // �����_���ȃT�C�Y��I��
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

            // Prefab���C���X�^���X���i�����j
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
            // �����_���Ȋp�x�𐶐�
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

        // �Ǘ��ɒǉ�
        ballInstances.Add(obj);

        return obj;
    }

    private void DebugUpdateScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // �}�E�X�z�C�[���ŏ�ɃX�N���[�������ꍇ�A�l�𑝂₷
        if (scroll > 0f)
        {
            if (debugSizeId < debugMaxSizeId) // ����𒴂��Ȃ��悤�ɂ���
            {
                debugSizeId++;
            }

            Debug.Log("Current value: " + debugSizeId);
        }
        // �}�E�X�z�C�[���ŉ��ɃX�N���[�������ꍇ�A�l�����炷
        else if (scroll < 0f)
        {
            if (debugSizeId > debugMinSizeId) // �����𒴂��Ȃ��悤�ɂ���
            {
                debugSizeId--;
            }

            Debug.Log("Current value: " + debugSizeId);
        }
    }
}

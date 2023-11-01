using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnClick : MonoBehaviour
{
    public GameObject prefabToInstantiate; // �C���X�y�N�^�[����ݒ肷��Prefab
    public List<float> possibleSizes; // �C���X�y�N�^�[����ݒ肷��\�ȃT�C�Y�ifloat�^�j

    void Update()
    {
        // ���N���b�N���ꂽ�Ƃ��̏���
        if (Input.GetMouseButtonDown(0))
        {
            // �J��������}�E�X�̃|�C���g�ւ̃��C�𓊉e����
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Z���̈ʒu�i�C�Ӂj
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // Z����0�ɐݒ�i2D�Q�[���̂��߁j
            worldPosition.z = 0;

            // �����_���Ȋp�x�𐶐�
            float randomAngle = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

            // Prefab���C���X�^���X���i�����j
            GameObject newPrefab = Instantiate(prefabToInstantiate, worldPosition, randomRotation);

            // �����_���ȃT�C�Y��I�����Đݒ�
            if (possibleSizes.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleSizes.Count);
                float randomSize = possibleSizes[randomIndex];
                newPrefab.transform.localScale = new Vector3(randomSize, randomSize, 1);
            }
        }
    }
}

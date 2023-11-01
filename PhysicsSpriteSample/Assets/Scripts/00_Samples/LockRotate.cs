using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotate : MonoBehaviour
{
    public float minAngle = -5f;  // �ŏ���]�p�x�i�N�����̊p�x����̕΍��j
    public float maxAngle = 5f;   // �ő��]�p�x�i�N�����̊p�x����̕΍��j
    public bool isRotationLocked = false;  // ��]�����b�N���邩�ǂ���

    private float initialRotationZ; // �N������Z����]�p�x

    void Start()
    {
        // �N������Z����]�p�x��ۑ�
        initialRotationZ = transform.eulerAngles.z;
    }

    void Update()
    {
        if (isRotationLocked)
        {
            // ��]�����S�Ƀ��b�N
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, initialRotationZ);
        }
        else
        {
            // ���݂�Z����]�p�x���擾
            float currentRotationZ = transform.eulerAngles.z;

            // 360�x�ȏ�̊p�x���l������
            float deltaRotationZ = Mathf.DeltaAngle(initialRotationZ, currentRotationZ);

            // MinMax�͈͂𒴂����ꍇ�ɏC��
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionReceiver : MonoBehaviour
{
    public BallObject parent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (parent.IsDestroyed)
            {
                return;
            }

            // BallObject同士の接触時判定
            if (collision.gameObject.TryGetComponent<BallCollisionReceiver>(out BallCollisionReceiver otherReceiver))
            {
                // 所属が同じレシーバーとは取りたくない
                if (otherReceiver.parent.uId != parent.uId)
                {
                    CollisionWithBallObject(otherReceiver.parent);
                }
            }
        }

        //Debug.Log($"OnCollisionEnter by {collision.gameObject.name}");
    }

    private void CollisionWithBallObject(BallObject otherBall)
    {
        if (otherBall.sizeId == parent.sizeId && otherBall.IsDestroyed == false)
        {
            int nextSizeId = parent.sizeId + 1;

            if (BallUtility.IsOverSize(nextSizeId) == false)
            {
                Vector3 spawnPosition = BallUtility.CalculateMidPosition(parent.RootBonePosition, otherBall.RootBonePosition);

                BallManager.Instance.CreateBall(spawnPosition, nextSizeId, true);
            }

            otherBall.DestroyInCollision();

            parent.DestroyInCollision();
        }
    }
}

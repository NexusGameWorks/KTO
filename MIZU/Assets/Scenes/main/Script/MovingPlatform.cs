using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // 移動開始地点
    public Transform pointB;  // 移動終了地点
    public float speed = 2.0f;  // 移動速度

    private bool movingToB = true;  // 移動方向のフラグ

    void Update()
    {
        // 移動処理
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToB = true;
            }
        }
    }

    // プレイヤーが床に触れたときに子オブジェクトに設定
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // プレイヤーが床から離れたときに親オブジェクトを解除
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}

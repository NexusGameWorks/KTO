using UnityEngine;

public class RespawnOnTrigger : MonoBehaviour
{
    public Vector3 respawnPoint; // リスポーンする座標

    private void Start()
    {
        // 初期位置をリスポーンポイントとして設定
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーにのみ反応
        {
            Respawn(other.gameObject);
        }
    }

    private void Respawn(GameObject player)
    {
        // プレイヤーをリスポーンポイントに戻す
        player.transform.position = respawnPoint;

        // 必要ならば速度もリセット（Rigidbodyを使用している場合）
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

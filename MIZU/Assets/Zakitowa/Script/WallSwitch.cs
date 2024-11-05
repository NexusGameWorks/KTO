using UnityEngine;

public class WallSwitch : MonoBehaviour
{
    public GameObject wall;  // 消したい壁のオブジェクト

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // プレイヤーがスイッチに触れたとき
        {
            wall.SetActive(false);  // 壁を非表示にする
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButton : MonoBehaviour, IButtonAction
{
    protected bool isPressed = false;  //  ボタンが押されたかどうかのフラグ

    //  ボタンが押された時に実行されるアクションを定義している抽象メソッド
    public abstract void Execute();

    public GameObject buttonTop;  // 押される部分のオブジェクト
    public float pressOffset = 0.01f;   // ボタンが下がる距離

    private Vector3 initialbuttonPosition;  // ボタン上部の初期位置

    void Start()
    {
        initialbuttonPosition = buttonTop.transform.localPosition;
    }

    //  プレイヤーがオブジェクトに接触した時に呼び出される
    private void OnCollisionEnter(Collision collision)
    {
        //  プレイヤー以外の衝突の場合は何もしない
        if (!collision.gameObject.CompareTag("Player")) return;

        if (isPressed) return;  //  既に押されていた場合は何もしない

        //  プレイヤーの状態を取得する
        var playerPhaseState = collision.gameObject.GetComponent<MM_PlayerPhaseState>();

        //  プレイヤーがSolidの状態ではない場合に返す
        if (playerPhaseState == null || playerPhaseState.GetState() != MM_PlayerPhaseState.State.Solid)
        {
            Debug.Log($"{gameObject.name}: プレイヤーはSolidの状態ではない");
            return;
        }

        // ボタン上部を下げる
        buttonTop.transform.localPosition = new Vector3(
            initialbuttonPosition.x,
            initialbuttonPosition.y - pressOffset,
            initialbuttonPosition.z
        );

        //  ボタンが押された状態にする
        isPressed = true;
        Execute();
    }
}

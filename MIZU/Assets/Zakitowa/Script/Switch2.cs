using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour
{
    public GameObject platform; // 対象のプラットフォームオブジェクトをここにアサインします
    private Vector3 originalScale;
    public Vector3 enlargedScale = new Vector3(2, 2, 2); // 拡大後のサイズを設定します
    public float shrinkDelay = 2.0f; // 元のサイズに戻るまでの遅延時間

    private Coroutine shrinkCoroutine;

    void Start()
    {
        if (platform != null)
        {
            originalScale = platform.transform.localScale;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (shrinkCoroutine != null)
            {
                StopCoroutine(shrinkCoroutine);
            }
            platform.transform.localScale = enlargedScale;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shrinkCoroutine = StartCoroutine(ShrinkPlatformAfterDelay());
        }
    }

    IEnumerator ShrinkPlatformAfterDelay()
    {
        yield return new WaitForSeconds(shrinkDelay);
        platform.transform.localScale = originalScale;
    }
}

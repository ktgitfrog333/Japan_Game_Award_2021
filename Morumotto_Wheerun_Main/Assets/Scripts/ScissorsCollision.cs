using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// はさみ衝突判定オブジェクトを扱うスクリプトクラス
/// </summary>
public class ScissorsCollision : MonoBehaviour
{
    /// <summary>追跡対象のオブジェクト名</summary>
    public string _name { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(_name))
        {
            StartCoroutine(Disable());
        }
    }

    /// <summary>
    /// 時間差でオブジェクトを無効化
    /// </summary>
    /// <returns></returns>
    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.parent.gameObject.SetActive(false);
        StopCoroutine(Disable());
    }
}

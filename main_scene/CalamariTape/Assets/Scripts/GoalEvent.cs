using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール判定スクリプトクラス
/// </summary>
public class GoalEvent : MonoBehaviour
{
    /// <summary>プレイヤー判定</summary>
    [SerializeField] private PlayerManager _mode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(_mode._calamari.name)
            || other.gameObject.name.Equals(_mode._nenchak.name)
            || other.gameObject.name.Equals(_mode._tsurutsuru.name))
        {
            Debug.Log("ゴール処理を起動");
        }
    }
}

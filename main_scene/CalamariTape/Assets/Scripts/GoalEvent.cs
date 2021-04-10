using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール判定スクリプトクラス
/// </summary>
public class GoalEvent : MonoBehaviour
{
    /// <summary>ゴール床オブジェクト接着判定</summary>
    [SerializeField] private bool _goalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTrigger == true && other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("ゴール処理を起動");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("GoalFloor"))
        {
            _goalTrigger = true;
        }
    }
}

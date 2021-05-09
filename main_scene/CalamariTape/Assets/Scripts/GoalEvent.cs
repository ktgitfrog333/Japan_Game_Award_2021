using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール判定スクリプトクラス
/// </summary>
public class GoalEvent : MonoBehaviour
{
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;

    /// <summary>クリア画面のUI</summary>
    [SerializeField] private GameObject _clearUI;

    /// <summary>ゴール床オブジェクト接着判定</summary>
    private bool _goalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTrigger == true && other.gameObject.tag.Equals("Player"))
        {
            _playerManager.GetComponent<CalamariMoveController>().enabled = false;
            _playerManager.GetComponent<NenchakMoveController>().enabled = false;
            _playerManager.GetComponent<TsuruTsuruMoveController>().enabled = false;
            _clearUI.SetActive(true);
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

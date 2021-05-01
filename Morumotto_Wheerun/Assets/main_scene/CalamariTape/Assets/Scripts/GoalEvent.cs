using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール判定スクリプトクラス
/// </summary>
public class GoalEvent : MonoBehaviour
{
    /// <summary>カラマリモードの操作スクリプト</summary>
    [SerializeField] private CalamariMoveController _calamariController;
    /// <summary>ネンチャクモードの操作スクリプト</summary>
    [SerializeField] private NenchakMoveController _nenchakController;
    /// <summary>ツルツルモードの操作スクリプト</summary>
    [SerializeField] private TsuruTsuruMoveController _tsurutsuruController;

    /// <summary>クリア画面のUI</summary>
    [SerializeField] private GameObject _clearUI;

    /// <summary>ゴール床オブジェクト接着判定</summary>
    private bool _goalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTrigger == true && other.gameObject.tag.Equals("Player"))
        {
            _calamariController.enabled = false;
            _nenchakController.enabled = false;
            _tsurutsuruController.enabled = false;
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

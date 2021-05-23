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
    /// <summary>セーブ実行スクリプト</summary>
    [SerializeField] private SaveControllerScene _saveController;
    /// <summary>花火パーティクル</summary>
    [SerializeField] private GameObject[] _fireworks;

    /// <summary>ゴール床オブジェクト接着判定</summary>
    private bool _goalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTrigger == true && other.gameObject.tag.Equals("Player"))
        {
            StopPlayer();
            _clearUI.SetActive(true);
            _saveController.SaveDataWrite();
            StartCoroutine(BloomFire());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("GoalFloor"))
        {
            _goalTrigger = true;
        }
    }

    /// <summary>
    /// プレイヤーの各モード操作と移動を止める
    /// </summary>
    private void StopPlayer()
    {
        _playerManager._calamariController._characterStop = true;
        _playerManager._calamariController.enabled = false;
        _playerManager._nenchakController.enabled = false;
        _playerManager._tsurutsuruController._characterStop = true;
        _playerManager._tsurutsuruController.enabled = false;
    }

    /// <summary>
    /// パーティクルで花火を生成する
    /// </summary>
    /// <returns></returns>
    private IEnumerator BloomFire()
    {
        for (int i = 0; i < _fireworks.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            _fireworks[i].SetActive(true);
        }
        StopCoroutine(BloomFire());
    }
}

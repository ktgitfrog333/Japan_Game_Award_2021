using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール判定スクリプトクラス
/// </summary>
public class GoalEvent : MonoBehaviour, DebugDemo
{
    /// <summary>モード変更の制御</summary>
    [SerializeField] private ModeChanger _modeChanger;
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>メニュー制御</summary>
    [SerializeField] private PauseWindowManager _pauseWindowManager;
    /// <summary>クリア画面のUI</summary>
    [SerializeField] private GameObject _clearUI;
    /// <summary>セーブ実行スクリプト</summary>
    [SerializeField] private SaveControllerScene _saveController;
    /// <summary>花火パーティクル</summary>
    [SerializeField] private GameObject[] _fireworks;

    /// <summary>SE・ME管理オブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>ゴール床オブジェクト接着判定</summary>
    private bool _goalTrigger;

    /// <summary>デバッグ</summary>
    [SerializeField] private VisualizeDebugMode _debug;

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTrigger == true && other.gameObject.tag.Equals("Player"))
        {
            StopPlayer();
            _clearUI.SetActive(true);
            _sfx.PlaySFX("me_game_clear");
            StartCoroutine(Save());
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
        _modeChanger.enabled = false;
        _playerManager._calamariController._characterStop = true;
        _playerManager._calamariAnimation.PauseAnimation("Scotch_tape_outside");
        _playerManager._calamariController.enabled = false;
        _playerManager._nenchakController.enabled = false;
        _playerManager._tsurutsuruController._characterStop = true;
        _playerManager._tsuruTsuruAnimation.PauseAnimation("Scotch_tape_outside");
        _playerManager._tsurutsuruController.enabled = false;
        _pauseWindowManager.enabled = false;
    }

    private IEnumerator Save()
    {
        _saveController.SaveDataWrite();
        yield return null;
        StopCoroutine(Save());
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

    public void DebugDemo1(string message)
    {
        if (_debug.Debug == true && _debug.DebugUI == true)
        {
            _debug.Log(message);
        }
    }
}

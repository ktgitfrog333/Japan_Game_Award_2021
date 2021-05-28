using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが接触した際にメッセージを表示するスクリプトクラス
/// </summary>
public class MessageManager : MonoBehaviour
{
    /// <summary>メッセージ表示用のオブジェクト</summary>
    [SerializeField] private GameObject _message;
    /// <summary>モード変更の制御</summary>
    [SerializeField] private ModeChanger _modeChanger;
    /// <summary>プレイヤーの各モードごとの制御</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>メニュー制御</summary>
    [SerializeField] private PauseWindowManager _pauseWindowManager;

    /// <summary>メッセージ表示中も操作を止めない（デバッグ用）</summary>
    [SerializeField] private bool _controllerDisabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (_controllerDisabled == true)
            {
                if (_message.activeSelf == false)
                {
                    _modeChanger.enabled = false;
                    _playerManager._calamariController.enabled = false;
                    _playerManager._nenchakController.enabled = false;
                    _playerManager._tsurutsuruController.enabled = false;
                    _pauseWindowManager.enabled = false;
                }
            }

            _message.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _message.SetActive(false);
        }
    }

    /// <summary>
    /// プレイヤーの操作停止を解除
    /// </summary>
    public void PlayerControllerEnable()
    {
        _modeChanger.enabled = true;
        _playerManager._calamariController.enabled = true;
        _playerManager._nenchakController.enabled = true;
        _playerManager._tsurutsuruController.enabled = true;
        _pauseWindowManager.enabled = true;
    }
}

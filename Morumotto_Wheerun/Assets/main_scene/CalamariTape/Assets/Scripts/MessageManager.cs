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
    /// <summary>プレイヤーの各モード制御</summary>
    [SerializeField] private PlayerManager _playerManager;

    /// <summary>メッセージ表示中も操作を止めない（デバッグ用）</summary>
    [SerializeField] private bool _controllerDisabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (_controllerDisabled == true)
            {
                _playerManager._calamariController.enabled = false;
                _playerManager._nenchakController.enabled = false;
                _playerManager._tsurutsuruController.enabled = false;
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
        _playerManager._calamariController.enabled = true;
        _playerManager._nenchakController.enabled = true;
        _playerManager._tsurutsuruController.enabled = true;
    }
}

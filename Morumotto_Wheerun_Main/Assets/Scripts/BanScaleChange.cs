using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Name;

/// <summary>
/// 大きさ変更を禁止
/// </summary>
public class BanScaleChange : MonoBehaviour
{
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;

    private void OnTriggerEnter(Collider other)
    {
        SetActivePlayerScaler(other.gameObject, false);
    }

    private void OnTriggerExit(Collider other)
    {
        SetActivePlayerScaler(other.gameObject, true);
    }

    /// <summary>
    /// プレイヤーの大きさ変更操作を有効・無効
    /// </summary>
    /// <param name="gameObject">プレイヤー</param>
    private void SetActivePlayerScaler(GameObject gameObject, bool active)
    {
        if (gameObject.name.Equals(_playerManager._calamari.name) || gameObject.name.Equals(_playerManager._nenchak.name) || gameObject.name.Equals(_playerManager._tsurutsuru.name))
        {
            if (_playerManager._calamariScaler.enabled != active)
            {
                _playerManager._calamariScaler.enabled = active;
            }
            if (_playerManager._nenchakScaler.enabled != active)
            {
                _playerManager._nenchakScaler.enabled = active;
            }
            if (_playerManager._tsuruTsuruScaler.enabled != active)
            {
                _playerManager._tsuruTsuruScaler.enabled = active;
            }
        }
    }
}

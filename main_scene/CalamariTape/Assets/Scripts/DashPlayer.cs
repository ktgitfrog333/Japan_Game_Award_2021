using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Tag;
using DeadException;
using Controller.AllmodeState;
using Beans.Field;
using Const.Name;

/// <summary>
/// プレイヤーをダッシュさせる
/// </summary>
public class DashPlayer : MonoBehaviour
{
    /// <summary>プレイヤーのモード管理スクリプトクラス</summary>
    private PlayerManager _playerManager;
    /// <summary>ダッシュパネル</summary>
    public DashPanel _dashPanel { set; get; }
    /// <summary>プレイヤーエフェクトスクリプトクラス</summary>
    private PlayerEffectController _playerEffectController;

    private void OnEnable()
    {
        // プレイヤー情報を検索して、PlayerManagerを取得する
        var g = GameObject.FindGameObjectWithTag(TagManager.PLAYER);
        if (DeadNullReference.CheckReferencedComponent(g, new PlayerManager().GetType().ToString()) == true)
        {
            _playerManager = g.GetComponent<PlayerManager>();
        }
        if (DeadNullReference.CheckReferencedComponent(g, new PlayerEffectController().GetType().ToString()) == true)
        {
            _playerEffectController = g.GetComponent<PlayerEffectController>();
        }
    }

    private void OnDisable()
    {
        Destroy(GetComponent<DashPlayer>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerManager != null)
        {
            // ゲームオーバーエリアに落下後にダッシュ状態が保持される不具合の対応
            if (other.gameObject.name.Equals(NameManager.GAME_OVER_LINE))
            {
                _dashPanel.StopAndDestroyDashPlayer(this);
            }
        }
    }

    void Update()
    {
        if (_playerManager != null && _playerEffectController != null)
        {
            // プレイヤーのダッシュ速度を停止して、本コンポーネントを削除する
            if (name.Equals(_playerManager._calamari.name))
            {
                PlayEffect(AllmodeStateConf.IsFieldWalled(_playerManager._calamari.transform, _playerManager._calamariWallMove._registMaxDistance));
            }
            else if (name.Equals(_playerManager._nenchak.name))
            {
                PlayEffect(AllmodeStateConf.IsFieldWalled(_playerManager._nenchak.transform, _playerManager._nenchakWallMove._registMaxDistance));
            }
            else if (name.Equals(_playerManager._tsurutsuru.name))
            {
                PlayEffect(AllmodeStateConf.IsFieldWalled(_playerManager._tsurutsuru.transform, _playerManager._tsuruTsuruGroundMove._registMaxDistance));
            }
        }
    }

    /// <summary>
    /// エフェクトを発生
    /// </summary>
    /// <param name="fieldWalled">衝突オブジェクト情報</param>
    private void PlayEffect(FieldWalled fieldWalled)
    {
        if (fieldWalled._collisionResult == true)
        {
            _playerEffectController.AppearEffectCollision(fieldWalled._hitCollider);
            _dashPanel.StopAndDestroyDashPlayer(this);
        }
        else
        {
            _playerEffectController._effectActive = false;
        }
    }
}

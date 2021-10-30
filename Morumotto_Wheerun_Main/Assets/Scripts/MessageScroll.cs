using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// UIのImageにてメッセージをスクロール制御するスクリプトクラス
/// </summary>
public class MessageScroll : MonoBehaviour
{
    /// <summary>アニメーションコントローラー</summary>
    [SerializeField] private Animator _anim;
    /// <summary>遷移先のステート名</summary>
    [SerializeField] private string _name = "Message001";
    /// <summary>アニメーションスピード</summary>
    [SerializeField] private float _speed = 0.5f;

    /// <summary>メッセージ処理管理スクリプト</summary>
    [SerializeField] private MessageManager _messageManager;

    /// <summary>SE・ME管理オブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>メッセージスキップを動的に制御するフラグ</summary>
    private bool _messageSkip;
    /// <summary>メッセージハッシュ（遷移先）</summary>
    private int _messageHash;
    /// <summary>メッセージハッシュ（終了後）</summary>
    private int _messageCompleteHash;

    void Start()
    {
        _messageHash = Animator.StringToHash(_name);
        _anim.speed = _speed;
        _anim.Play(_messageHash);
    }

    private void OnEnable()
    {
        _messageSkip = false;
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Submit") && _messageSkip == false)
        {
            // アニメーションを終了
            _anim.Play(_messageHash, 0, 1);
            _sfx.PlaySFX("se_decided");
            _messageSkip = true;
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName(_name) == false || _messageSkip == true)
        {
            _messageManager.PlayerControllerEnable();
        }
    }
}

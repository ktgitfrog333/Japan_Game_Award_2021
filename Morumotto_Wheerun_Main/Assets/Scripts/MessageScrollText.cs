using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

/// <summary>
/// UIのTextにてメッセージをスクロール制御するスクリプトクラス
/// </summary>
public class MessageScrollText : MonoBehaviour
{
    /// <summary>メッセージ処理管理スクリプト</summary>
    [SerializeField] private MessageManager _messageManager;

    /// <summary>SE・ME管理オブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>テキストデータ</summary>
    [SerializeField] private Text _uiText;
    /// <summary>１文字ごとのテキスト送り時間</summary>
    private float _waitSeconds;

    /// <summary>メッセージスキップを動的に制御するフラグ</summary>
    private bool _messageSkip;
    /// <summary>SEを一度だけ再生制御するフラグ</summary>
    private bool _skipPlaySFX;

    /// <summary>テキスト何文字目</summary>
    private int _idx;
    /// <summary>テキスト配列</summary>
    private char[] _charList;
    /// <summary>テキスト送り停止フラグ</summary>
    private bool _messageStop;
    /// <summary>プレイヤー操作解除済みフラグ</summary>
    private bool _playerControllerEnable;

    private void OnEnable()
    {
        _messageSkip = false;
        _skipPlaySFX = false;
        _waitSeconds = 0.075f;
        _idx = 0;
        _charList = _uiText.text.ToCharArray();
        _uiText.text = "";
        StartCoroutine(ScrollSentence());
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Submit") && _messageSkip == false && _messageStop == false)
        {
            if (_skipPlaySFX == false)
            {
                _sfx.PlaySFX("se_decided");
                _skipPlaySFX = true;
            }
            _messageSkip = true;
            _waitSeconds = 0f;
        }

        if ((_messageSkip == true || _uiText.text.Length == _charList.Length) && _playerControllerEnable == false)
        {
            _messageSkip = false;
            _playerControllerEnable = true;
            _messageManager.PlayerControllerEnable();
        }
    }

    /// <summary>
    /// テキスト送り
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScrollSentence()
    {
        yield return new WaitForSeconds(_waitSeconds);
        if (_messageStop == false)
        {
            if (_idx < _charList.Length && _messageSkip == false)
            {
                _uiText.text += _charList[_idx];
                _idx++;
            }

            // メッセージを最後まで送った場合はスキップを有効（テキスト終了）にする
            if (_uiText.text.Length == _charList.Length)
            {
                _messageSkip = true;
                _messageStop = true;
            }
        }
        StartCoroutine(ScrollSentence());
    }

    /// <summary>
    /// スクロールを止める
    /// </summary>
    public void StopSentence()
    {
        _messageStop = true;
    }

    /// <summary>
    /// スクロールを再生
    /// </summary>
    public void StartSentence()
    {
        _messageStop = false;
    }

    /// <summary>
    /// テキストメッセージ送りの長さを調べる
    /// </summary>
    /// <returns>確認結果（0：テキスト送り途中/1：テキスト送り完了）</returns>
    public int CheckMessageLength()
    {
        var r = 0;
        if (_uiText.text.Length == _charList.Length)
        {
            r = 1;
        }
        return r;
    }
}

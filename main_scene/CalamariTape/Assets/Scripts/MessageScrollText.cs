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

    private void OnEnable()
    {
        _messageSkip = false;
        _waitSeconds = 0.075f;
        StartCoroutine(ScrollSentence());
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Submit") && _messageSkip == false)
        {
            _sfx.PlaySFX("se_decided");
            _messageSkip = true;
            _waitSeconds = 0f;
        }

        if (_messageSkip == true)
        {
            _messageSkip = false;
            _messageManager.PlayerControllerEnable();
        }
    }

    /// <summary>
    /// テキスト送り
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScrollSentence()
    {
        var charList = _uiText.text.ToCharArray();

        _uiText.text = "";
        for (int i = 0; i < charList.Length && _messageSkip == false; i++)
        {
            yield return new WaitForSeconds(_waitSeconds);
            _uiText.text += charList[i];
        }

        // メッセージを最後まで送った場合はスキップを有効（テキスト終了）にする
        if (_uiText.text.Length == charList.Length)
        {
            _messageSkip = true;
        }
        StopCoroutine(ScrollSentence());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Const.Tag;

/// <summary>
/// ポーズ画面表示制御スクリプトクラス
/// </summary>
public class PauseWindowManager : MonoBehaviour
{
    /// <summary>ポーズ画面UIオブジェクト</summary>
    [SerializeField] private GameObject _menu;
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>メニューを閉じる際に一度のみ実行するよう制御するフラグ</summary>
    private bool _menuClose;
    /// <summary>チュートリアルメッセージ一覧UI</summary>
    [SerializeField] private GameObject _adviceMessage;
    /// <summary>チュートリアルメッセージ処理</summary>
    private List<MessageScrollText> _messageText;
    /// <summary>レベルデザインギミック制御</summary>
    [SerializeField] private StopGimmick _stopGimmick;

    private void Start()
    {
        CashMessageText();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Menu") == true)
        {
            _playerManager._calamariAnimation.PauseAnimation("Scotch_tape_outside");
            _playerManager._calamariController.enabled = false;
            _playerManager._nenchakAnimation.PauseAnimation("Scotch_tape_outside");
            _playerManager._nenchakController.enabled = false;
            _playerManager._tsuruTsuruAnimation.PauseAnimation("Scotch_tape_outside");
            _playerManager._tsurutsuruController.enabled = false;
            _menu.SetActive(true);
            StopAdviceMessages();
            _stopGimmick.StopAllGimmik();
        }
    }

    private void OnEnable()
    {
        _menuClose = false;
    }

    /// <summary>
    /// チュートリアルメッセージをキャッシュ
    /// </summary>
    private void CashMessageText()
    {
        var cnt = _adviceMessage.transform.childCount;
        if (0 < cnt)
        {
            _messageText = new List<MessageScrollText>();
            for (var i = 0; i < cnt; i++)
            {
                var g = _adviceMessage.transform.GetChild(i);
                if (g.tag.Equals(TagManager.MESSAGE))
                {
                    _messageText.Add(g.GetComponent<MessageScrollText>());
                }
            }
        }

    }

    /// <summary>
    /// チュートリアルメッセージを一時停止
    /// </summary>
    private void StopAdviceMessages()
    {
        foreach (var m in _messageText)
        {
            if (m.isActiveAndEnabled == true)
            {
                m.StopSentence();
            }
        }
    }

    /// <summary>
    /// メニューを閉じる
    /// </summary>
    public void MenuClose()
    {
        if (_menuClose == false)
        {
            StartCoroutine(MenuCloseHalfSec());
            _menuClose = true;
        }
    }

    /// <summary>
    /// 0.5秒後にメニューを閉じる
    /// </summary>
    /// <returns></returns>
    private IEnumerator MenuCloseHalfSec()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckActiveAdviceMessages() != 0)
        {
            _playerManager._calamariController.enabled = true;
            _playerManager._nenchakController.enabled = true;
            _playerManager._tsurutsuruController.enabled = true;
            _stopGimmick.StartAllGimmik();
        }
        _menuClose = false;
        _menu.SetActive(false);
        StartAdviceMessages();

        StopCoroutine(MenuCloseHalfSec());
    }

    /// <summary>
    /// チュートリアルメッセージの有効チェック
    /// </summary>
    /// <returns></returns>
    private int CheckActiveAdviceMessages()
    {
        var result = -1;
        foreach (var m in _messageText)
        {
            if (m.isActiveAndEnabled == true)
            {
                result = m.CheckMessageLength();
            }
        }
        return result;
    }

    /// <summary>
    /// チュートリアルメッセージを再生
    /// </summary>
    private void StartAdviceMessages()
    {
        foreach (var m in _messageText)
        {
            if (m.isActiveAndEnabled == true)
            {
                m.StartSentence();
            }
        }
    }
}

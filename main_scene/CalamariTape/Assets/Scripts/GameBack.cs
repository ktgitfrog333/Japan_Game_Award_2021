using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームに戻る実行スクリプトクラス
/// </summary>
public class GameBack : MonoBehaviour
{
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _window;
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;
    /// <summary>メニューを閉じる際に一度のみ実行するよう制御するフラグ</summary>
    private bool _menuClose;

    private void OnEnable()
    {
        _menuClose = false;
    }

    /// <summary>
    /// ゲームに戻るイベントを実行
    /// </summary>
    public void EventGameBack()
    {
        if (_menuClose == false)
        {
            _sfx.PlaySFX("se_close");
            _window.MenuClose();

            _menuClose = true;
        }
    }
}

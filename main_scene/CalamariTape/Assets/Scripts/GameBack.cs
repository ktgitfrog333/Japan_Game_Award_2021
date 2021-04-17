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

    /// <summary>
    /// ゲームに戻るイベントを実行
    /// </summary>
    public void EventGameBack()
    {
        _sfx.PlaySFX("se_decided");
        _window.MenuClose();
    }
}

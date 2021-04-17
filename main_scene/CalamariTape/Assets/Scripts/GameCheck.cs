using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊び方の確認実行スクリプトクラス
/// </summary>
public class GameCheck : MonoBehaviour
{
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _window;
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>
    /// 遊び方の確認イベントを実行
    /// </summary>
    public void EventGameCheck()
    {
        _sfx.PlaySFX("se_decided");
        Debug.Log("遊び方の確認");
    }
}

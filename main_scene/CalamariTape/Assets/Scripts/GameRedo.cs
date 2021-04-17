using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージをやりなおす実行スクリプトクラス
/// </summary>
public class GameRedo : MonoBehaviour
{
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _window;
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>
    /// ステージをやりなおすイベントを実行
    /// </summary>
    public void EventGameRedo()
    {
        _sfx.PlaySFX("se_decided");
        Debug.Log("ステージをやりなおす");
    }
}

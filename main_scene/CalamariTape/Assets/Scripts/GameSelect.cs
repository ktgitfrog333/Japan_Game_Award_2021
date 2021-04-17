using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 他のステージを選ぶ実行スクリプトクラス
/// </summary>
public class GameSelect : MonoBehaviour
{
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _window;
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;

    /// <summary>
    /// 他のステージを選ぶイベントを実行
    /// </summary>
    public void EventGameSelect()
    {
        _sfx.PlaySFX("se_decided");
        Debug.Log("他のステージを選ぶ");
    }
}

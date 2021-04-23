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
    /// <summary>操作方法UI</summary>
    [SerializeField] private GameObject _manual;
    /// <summary>画面表示フラグ</summary>
    private bool _openerFlag;

    /// <summary>
    /// 遊び方の確認イベントを実行
    /// </summary>
    public void EventGameCheck()
    {
        if (_openerFlag == false)
        {
            _sfx.PlaySFX("se_decided");
            _manual.SetActive(true);
            _openerFlag = true;
        }
        else
        {
            _sfx.PlaySFX("se_close");
            _manual.SetActive(false);
            _openerFlag = false;
        }
    }
}

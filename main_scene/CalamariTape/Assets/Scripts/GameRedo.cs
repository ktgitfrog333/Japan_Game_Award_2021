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
    /// <summary>黒い背景</summary>
    [SerializeField] private GameObject _blackBackGround;
    /// <summary>メニューを連続を実行フラグ</summary>
    private bool _flag;

    /// <summary>
    /// ステージをやりなおすイベントを実行
    /// </summary>
    public void EventGameRedo()
    {
        if (_flag == false)
        {
            _flag = true;
            _sfx.PlaySFX("se_decided");
            _blackBackGround.SetActive(true);
            Debug.Log("ステージをやりなおす");
        }
    }
}

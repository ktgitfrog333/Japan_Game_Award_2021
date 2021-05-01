using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///UI操作スクリプトクラス
/// </summary>
public class UIController : MonoBehaviour
{
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;
    /// <summary>選択状態フレーム</summary>
    [SerializeField] private GameObject _frame;

    /// <summary>
    /// 選択時に呼び出すメソッド
    /// </summary>
    public void Selected()
    {
        _frame.SetActive(true);
        // TODO：メニュー画面のセレクト効果音を確認
        _sfx.PlaySFX("se_menu");
    }

    /// <summary>
    /// 選択解除時に呼び出すメソッド
    /// </summary>
    public void Diselected()
    {
        _frame.SetActive(false);
    }
}

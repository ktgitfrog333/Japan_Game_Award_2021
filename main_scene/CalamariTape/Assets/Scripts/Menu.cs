using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メニュー画面制御スクリプトクラス
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;
    /// <summary>選択項目のUIオブジェクト</summary>
    [SerializeField] private UIController _firstElement;

    private void OnEnable()
    {
        _firstElement.Selected();
        _sfx.PlaySFX("se_menu");
    }
}

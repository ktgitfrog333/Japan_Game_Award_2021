using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// メニュー画面制御スクリプトクラス
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;
    /// <summary>選択項目のUIスクリプト</summary>
    [SerializeField] private UIController _firstElement;
    /// <summary>選択項目のUIオブジェクト</summary>
    [SerializeField] private GameObject _firstObject;
    /// <summary>イベントシステム</summary>
    [SerializeField] private EventSystem _event;

    private void OnEnable()
    {
        _event.SetSelectedGameObject(_firstObject);
        _firstElement.Selected();
        _sfx.PlaySFX("se_menu");
    }
}

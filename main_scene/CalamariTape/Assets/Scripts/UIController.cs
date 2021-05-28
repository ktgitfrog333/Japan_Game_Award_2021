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
    /// <summary>位置・回転・スケール情報</summary>
    private RectTransform _transform;
    /// <summary>幅</summary>
    private float _width;
    /// <summary>高さ</summary>
    private float _height;
    /// <summary>一番最初のみ実施</summary>
    private bool _first = true;

    /// <summary>
    /// 選択時に呼び出すメソッド
    /// </summary>
    public void Selected()
    {
        if (_first == true)
        {
            _transform = transform as RectTransform;
            _width = _transform.sizeDelta.x;
            _height = _transform.sizeDelta.y;

            _first = false;
        }
        _frame.SetActive(true);
        _transform.sizeDelta = new Vector2(_width * 1.2f, _height * 1.2f);
        _sfx.PlaySFX("se_select");
    }

    /// <summary>
    /// 選択解除時に呼び出すメソッド
    /// </summary>
    public void Diselected()
    {
        _frame.SetActive(false);
        _transform.sizeDelta = new Vector2(_width, _height);
    }
}

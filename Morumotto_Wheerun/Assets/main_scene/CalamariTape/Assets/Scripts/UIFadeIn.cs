using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// UIへフェード演出を入れるスクリプトクラス
/// </summary>
public class UIFadeIn : MonoBehaviour
{
    /// <summary>UIの画像</summary>
    [SerializeField] private Image _image;

    /// <summary>赤色のレベル</summary>
    private float _redColorLevel;
    /// <summary>緑色のレベル</summary>
    private float _greenColorLevel;
    /// <summary>青色のレベル</summary>
    private float _blueColorLevel;
    /// <summary>透明色のレベル</summary>
    private float _alphaColorLevel;

    /// <summary>不透明度変化の速度</summary>
    private float _fadeSpeed;

    void Start()
    {
        _redColorLevel = _image.color.r;
        _greenColorLevel = _image.color.g;
        _blueColorLevel = _image.color.b;
        _alphaColorLevel = 1f;

        _fadeSpeed = 0.75f;

        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// フェードアウトを実装
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        for (int i = 0; i < 360 || -0.1f < _alphaColorLevel; i++)
        {
            _alphaColorLevel = Mathf.MoveTowards(_alphaColorLevel, 0f, _fadeSpeed * Time.deltaTime);
            _image.color = new Color(_redColorLevel, _greenColorLevel, _blueColorLevel, _alphaColorLevel);
            yield return null;
            if (0.1 < _alphaColorLevel)
            {
                StopCoroutine(FadeIn());
            }
        }
    }
}

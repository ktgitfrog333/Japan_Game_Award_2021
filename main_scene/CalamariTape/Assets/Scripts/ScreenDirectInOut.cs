using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIへフェード演出を入れるスクリプトクラス
/// </summary>
public class ScreenDirectInOut : MonoBehaviour
{
    /// <summary>位置情報の最低値</summary>
    private static readonly float LOAD_NOW_MIN_POSITION = 0f;
    /// <summary>位置情報の最高値</summary>
    private static readonly float LOAD_NOW_MAX_POSITION = -1920.0f;

    /// <summary>UIの位置情報をキャッシュ</summary>
    private RectTransform _loadNowRect;
    /// <summary>ロード画面を画面外へ移動させる挙動のトリガー</summary>
    public bool _drawLoadNowFadeInTrigger { get; set; }
    /// <summary>ロード画面を画面内へ移動させる挙動のトリガー</summary>
    public bool _drawLoadNowFadeOutTrigger { get; set; }
    /// <summary>スタート演出</summary>
    [SerializeField] private GameObject _startPoint;

    /// <summary>次に移動するシーン情報</summary>
    [SerializeField] private SceneMove _nextScene;

    private void Start()
    {
        _loadNowRect = this.gameObject.transform as RectTransform;
        _loadNowRect.anchoredPosition = new Vector2(LOAD_NOW_MIN_POSITION, 0);
        _drawLoadNowFadeInTrigger = true;
    }

    private void Update()
    {
        if (_drawLoadNowFadeInTrigger == true)
        {
            DrawLoadNowFadeIn();
        }
        if (_drawLoadNowFadeOutTrigger == true)
        {
            DrawLoadNowFadeOut();
        }
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    private void DrawLoadNowFadeIn()
    {
        if (_loadNowRect.anchoredPosition.x >= LOAD_NOW_MAX_POSITION)
        {
            _loadNowRect.anchoredPosition += new Vector2(-1000 * Time.deltaTime, 0);
        }
        else
        {
            _loadNowRect.anchoredPosition = new Vector2(LOAD_NOW_MAX_POSITION, 0);
            _drawLoadNowFadeInTrigger = false;
            ActiveObject();
        }
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    private void DrawLoadNowFadeOut()
    {
        if (_loadNowRect.anchoredPosition.x <= LOAD_NOW_MIN_POSITION)
        {
            _loadNowRect.anchoredPosition += new Vector2(1000 * Time.deltaTime, 0);
        }
        else
        {
            _loadNowRect.anchoredPosition = new Vector2(LOAD_NOW_MIN_POSITION, 0);
            _drawLoadNowFadeOutTrigger = false;
            _nextScene.NextScene();
        }
    }

    /// <summary>
    /// タイミングごとにオブジェクトを有効にする
    /// </summary>
    private void ActiveObject()
    {
        _startPoint.SetActive(true);
    }
}

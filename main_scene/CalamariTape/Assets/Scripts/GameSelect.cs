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
    /// <summary>シーン遷移の演出画像</summary>
    [SerializeField] private GameObject _loadNowSprite;
    /// <summary>シーン遷移スクリプト</summary>
    [SerializeField] private ScreenDirectInOut _direct;
    /// <summary>遷移先のシーン名</summary>
    [SerializeField] private string _nextSceneName;
    /// <summary>遷移先のシーン管理</summary>
    [SerializeField] private SceneMove _nextScene;

    /// <summary>メニューを連続を実行フラグ</summary>
    private bool _flag;

    /// <summary>
    /// 他のステージを選ぶイベントを実行
    /// </summary>
    public void EventGameSelect()
    {
        if (_flag == false)
        {
            _sfx.PlaySFX("se_decided");
            _nextScene.sceneName = _nextSceneName;
            _loadNowSprite.SetActive(true);
            _direct._drawLoadNowFadeOutTrigger = true;
            Debug.Log("他のステージを選ぶ");

            _flag = true;
        }
    }
}

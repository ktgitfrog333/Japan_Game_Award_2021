using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージをやりなおす実行スクリプトクラス
/// </summary>
public class GameRedo : MonoBehaviour
{
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
    /// ステージをやりなおすイベントを実行
    /// </summary>
    public void EventGameRedo()
    {
        if (_flag == false)
        {
            _sfx.PlaySFX("se_decided");
            _nextScene.sceneName = _nextSceneName;
            _loadNowSprite.SetActive(true);
            _direct._drawLoadNowFadeOutTrigger = true;

            _flag = true;
        }
    }
}

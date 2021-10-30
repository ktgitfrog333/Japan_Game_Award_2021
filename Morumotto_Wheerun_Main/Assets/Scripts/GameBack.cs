using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

/// <summary>
/// ゲームに戻る実行スクリプトクラス
/// </summary>
public class GameBack : MonoBehaviour
{
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _window;
    /// <summary>効果音ゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfx;
    /// <summary>メニューを閉じる際に一度のみ実行するよう制御するフラグ</summary>
    private bool _menuClose;
    /// <summary>ボタンイベント</summary>
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _menuClose = false;
        if (_button.isActiveAndEnabled == false)
        {
            _button.enabled = true;
        }
    }

    /// <summary>
    /// ゲームに戻るイベントを実行
    /// </summary>
    public async void EventGameBack()
    {
        if (_menuClose == false)
        {
            _sfx.PlaySFX("se_close");
            _window.MenuClose();

            _menuClose = true;
            await Task.Delay(100);
            _button.enabled = false;
        }
    }
}

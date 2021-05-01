using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// ポーズ画面表示制御スクリプトクラス
/// </summary>
public class PauseWindowManager : MonoBehaviour
{
    /// <summary>ポーズ画面UIオブジェクト</summary>
    [SerializeField] private GameObject _menu;
    /// <summary>カラマリモードの操作スクリプト</summary>
    [SerializeField] private CalamariMoveController _calamariController;
    /// <summary>ネンチャクモードの操作スクリプト</summary>
    [SerializeField] private NenchakMoveController _nenchakController;
    /// <summary>ツルツルモードの操作スクリプト</summary>
    [SerializeField] private TsuruTsuruMoveController _tsurutsuruController;
    /// <summary>メニューを閉じる際に一度のみ実行するよう制御するフラグ</summary>
    private bool _menuClose;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Menu") == true)
        {
            _calamariController.enabled = false;
            _nenchakController.enabled = false;
            _tsurutsuruController.enabled = false;
            _menu.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _menuClose = false;
    }

    /// <summary>
    /// メニューを閉じる
    /// </summary>
    public void MenuClose()
    {
        if (_menuClose == false)
        {
            StartCoroutine(MenuCloseHalfSec());
            _menuClose = true;
        }
    }

    /// <summary>
    /// 0.5秒後にメニューを閉じる
    /// </summary>
    /// <returns></returns>
    private IEnumerator MenuCloseHalfSec()
    {
        yield return new WaitForSeconds(0.5f);
        _calamariController.enabled = true;
        _nenchakController.enabled = true;
        _tsurutsuruController.enabled = true;
        _menuClose = false;
        _menu.SetActive(false);

        StopCoroutine(MenuCloseHalfSec());
    }
}

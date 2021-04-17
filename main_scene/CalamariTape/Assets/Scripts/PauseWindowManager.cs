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

    /// <summary>
    /// メニューを閉じる
    /// </summary>
    public void MenuClose()
    {
        Debug.Log("メニューを閉じる");
    }
}

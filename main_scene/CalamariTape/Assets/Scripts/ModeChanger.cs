using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// プレイヤーモードチェンジのスクリプトクラス
/// <para/>カラマリモード・ネンチャクモード・ツルツルモードへ切り替え
/// </summary>
public class ModeChanger : MonoBehaviour
{
    /// <summary>カラマリモードオブジェクト</summary>
    [SerializeField] private GameObject _calamari;
    /// <summary>ネンチャクモードオブジェクト</summary>
    [SerializeField] private GameObject _nenchak;
    /// <summary>ツルツルモードオブジェクト</summary>
    [SerializeField] private GameObject _tsurutsuru;

    /// <summary>カラマリモード切り替え入力フラグ</summary>
    private bool _calamariInput;
    /// <summary>ツルツルモード切り替え入力フラグ</summary>
    private bool _tsurutsuruInput;

    /// <summary>モード状態</summary>
    private string _mode;

    void Start()
    {
        _calamari.SetActive(true);
        _mode = _calamari.name.ToString();
    }

    private void Update()
    {
        // ループ内で複数のモード切り替え入力が発生しないように制御するフラグ
        bool inputActive = false;

        // 入力チェック
        if (_tsurutsuruInput == false && inputActive == false && _mode.Equals(_calamari.name.ToString()))
        {
            _calamariInput = false;
            _tsurutsuruInput = CrossPlatformInputManager.GetButtonDown("RB");
            inputActive = true;

        }
        if (_calamariInput == false && inputActive == false && _mode.Equals(_tsurutsuru.name.ToString()))
        {
            _tsurutsuruInput = false;
            _calamariInput = CrossPlatformInputManager.GetButtonDown("RB");
            inputActive = true;
        }

        // モード切り替え
        if (_tsurutsuruInput == true)
        {
            TsurutsuruModeEnabledChange();
        }
        if (_calamariInput == true)
        {
            CalamariModeDisabledChange();
        }
    }

    /// <summary>
    /// カラマリモードへ切り替え
    /// </summary>
    private void CalamariModeDisabledChange()
    {
        _calamari.transform.position = _tsurutsuru.transform.position;
        _calamari.transform.eulerAngles = _tsurutsuru.transform.eulerAngles;
        _calamari.SetActive(true);
        _nenchak.SetActive(false);
        _tsurutsuru.SetActive(false);
        _mode = _calamari.name.ToString();
    }

    /// <summary>
    /// ネンチャクモードへ切り替え
    /// </summary>
    private void NenchakModeEnabledChange()
    {
        _calamari.SetActive(false);
        _nenchak.SetActive(true);
        _tsurutsuru.SetActive(false);
        _mode = _nenchak.name.ToString();
    }

    /// <summary>
    /// ツルツルモードへ切り替え
    /// </summary>
    private void TsurutsuruModeEnabledChange()
    {
        _tsurutsuru.transform.position = _calamari.transform.position;
        _tsurutsuru.transform.eulerAngles = _calamari.transform.eulerAngles;
        _calamari.SetActive(false);
        _nenchak.SetActive(false);
        _tsurutsuru.SetActive(true);
        _mode = _tsurutsuru.name.ToString();
    }
}

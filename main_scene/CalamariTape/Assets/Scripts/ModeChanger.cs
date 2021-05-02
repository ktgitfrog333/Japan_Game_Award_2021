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
    /// <summary>ツルツルモード切り替え入力フラグ</summary>
    private bool _nenchakInput;

    /// <summary>モード状態</summary>
    private string _mode;

    /// <summary>追従させるカメラを切り替える</summary>
    [SerializeField] CameraPointMove _cameraPoint;

    void Start()
    {
        Debug.Log("カラマリモード");
        _calamari.SetActive(true);
        _cameraPoint.PlayerCameraLook(_calamari);
        _mode = _calamari.name.ToString();
    }

    private void Update()
    {
        // ループ内で複数のモード切り替え入力が発生しないように制御するフラグ
        bool inputActive = false;

        // 入力チェック（ツルツルモードもしくはネンチャクモードからカラマリモード）
        if (_nenchakInput == false && _tsurutsuruInput == false && inputActive == false && _mode.Equals(_calamari.name.ToString()))
        {
            _calamariInput = false;
            if (CrossPlatformInputManager.GetButtonDown("RB") == true)
            {
                _tsurutsuruInput = CrossPlatformInputManager.GetButtonDown("RB");
                inputActive = true;
            }
            else if (CrossPlatformInputManager.GetButtonDown("LB") == true)
            {
                _nenchakInput = CrossPlatformInputManager.GetButtonDown("LB");
                inputActive = true;
            }
        }

        // 入力チェック（ツルツルモード）
        if (_calamariInput == false && inputActive == false && _mode.Equals(_tsurutsuru.name.ToString()))
        {
            _tsurutsuruInput = false;
            _calamariInput = CrossPlatformInputManager.GetButtonDown("RB");
            inputActive = true;
        }

        // 入力チェック（ネンチャクモード）
        if (_calamariInput == false && inputActive == false && _mode.Equals(_nenchak.name.ToString()))
        {
            _nenchakInput = false;
            _calamariInput = CrossPlatformInputManager.GetButtonDown("LB");
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
        if (_nenchakInput == true)
        {
            NenchakModeEnabledChange();
        }
    }

    /// <summary>
    /// カラマリモードへ切り替え
    /// </summary>
    private void CalamariModeDisabledChange()
    {
        Debug.Log("カラマリモード");
        var p = Vector3.zero;
        var r = Vector3.zero;
        if (_mode.Equals(_tsurutsuru.name.ToString()))
        {
            p = _tsurutsuru.transform.position;
            r = _tsurutsuru.transform.eulerAngles;
        }
        else if (_mode.Equals(_nenchak.name.ToString()))
        {
            p = _nenchak.transform.position;
            r = _nenchak.transform.eulerAngles;
        }
        _calamari.transform.position = p;
        _calamari.transform.eulerAngles = r;
        _calamari.SetActive(true);
        _cameraPoint.PlayerCameraLook(_calamari);
        _nenchak.SetActive(false);
        _tsurutsuru.SetActive(false);
        _mode = _calamari.name.ToString();
    }

    /// <summary>
    /// ネンチャクモードへ切り替え
    /// </summary>
    private void NenchakModeEnabledChange()
    {
        Debug.Log("ネンチャクモード");
        _nenchak.transform.position = _calamari.transform.position;
        _nenchak.transform.eulerAngles = _calamari.transform.eulerAngles;
        _calamari.SetActive(false);
        _nenchak.SetActive(true);
        _cameraPoint.PlayerCameraLook(_nenchak);
        _tsurutsuru.SetActive(false);
        _mode = _nenchak.name.ToString();
    }

    /// <summary>
    /// ツルツルモードへ切り替え
    /// </summary>
    private void TsurutsuruModeEnabledChange()
    {
        Debug.Log("ツルツルモード");
        _tsurutsuru.transform.position = _calamari.transform.position;
        _tsurutsuru.transform.eulerAngles = _calamari.transform.eulerAngles;
        _calamari.SetActive(false);
        _nenchak.SetActive(false);
        _tsurutsuru.SetActive(true);
        _cameraPoint.PlayerCameraLook(_tsurutsuru);
        _mode = _tsurutsuru.name.ToString();
    }
}

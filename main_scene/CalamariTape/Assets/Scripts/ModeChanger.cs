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
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;

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

    /// <summary>モード切り替えエフェクト</summary>
    [SerializeField] private GameObject _scissorsEffect;
    /// <summary>パーティクルシステム</summary>
    [SerializeField] private ParticleSystem[] _particleSystem;

    void Start()
    {
        Debug.Log("カラマリモード");
        _playerManager._calamari.SetActive(true);
        _cameraPoint.PlayerCameraLook(_playerManager._calamari);
        _mode = _playerManager._calamari.name.ToString();
    }

    private void Update()
    {
        // ループ内で複数のモード切り替え入力が発生しないように制御するフラグ
        bool inputActive = false;

        // 入力チェック（ツルツルモードもしくはネンチャクモードからカラマリモード）
        if (_nenchakInput == false && _tsurutsuruInput == false && inputActive == false && _mode.Equals(_playerManager._calamari.name.ToString()))
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
        if (_calamariInput == false && inputActive == false && _mode.Equals(_playerManager._tsurutsuru.name.ToString()))
        {
            _tsurutsuruInput = false;
            _calamariInput = CrossPlatformInputManager.GetButtonDown("RB");
            inputActive = true;
        }

        // 入力チェック（ネンチャクモード）
        if (_calamariInput == false && inputActive == false && _mode.Equals(_playerManager._nenchak.name.ToString()))
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
        var position = Vector3.zero;
        var rotation = Vector3.zero;
        var scale = new Vector3(1, 1, 1);
        if (_mode.Equals(_playerManager._tsurutsuru.name.ToString()))
        {
            position = _playerManager._tsurutsuru.transform.position;
            rotation = _playerManager._tsurutsuru.transform.eulerAngles;
            scale = _playerManager._tsurutsuru.transform.localScale;
        }
        else if (_mode.Equals(_playerManager._nenchak.name.ToString()))
        {
            position = _playerManager._nenchak.transform.position;
            rotation = _playerManager._nenchak.transform.eulerAngles;
            scale = _playerManager._nenchak.transform.localScale;
        }
        // エフェクト発生
        PlayScissorsEffect(position);

        _playerManager._calamari.transform.position = position;
        _playerManager._calamari.transform.eulerAngles = rotation;
        _playerManager._calamari.SetActive(true);
        _playerManager._calamariController.OnChange();
        _cameraPoint.PlayerCameraLook(_playerManager._calamari);
        _playerManager._nenchak.SetActive(false);
        _playerManager._tsurutsuru.SetActive(false);
        _mode = _playerManager._calamari.name.ToString();
    }

    /// <summary>
    /// ネンチャクモードへ切り替え
    /// </summary>
    private void NenchakModeEnabledChange()
    {
        Debug.Log("ネンチャクモード");
        // エフェクト発生
        PlayScissorsEffect(_playerManager._calamari.transform.position);

        _playerManager._nenchak.transform.position = _playerManager._calamari.transform.position;
        _playerManager._nenchak.transform.eulerAngles = _playerManager._calamari.transform.eulerAngles;
        _playerManager._calamari.SetActive(false);
        _playerManager._nenchak.SetActive(true);
        _playerManager._nenchakController.OnChange();
        _cameraPoint.PlayerCameraLook(_playerManager._nenchak);
        _playerManager._tsurutsuru.SetActive(false);
        _mode = _playerManager._nenchak.name.ToString();
    }

    /// <summary>
    /// ツルツルモードへ切り替え
    /// </summary>
    private void TsurutsuruModeEnabledChange()
    {
        Debug.Log("ツルツルモード");
        // エフェクト発生
        PlayScissorsEffect(_playerManager._calamari.transform.position);

        _playerManager._tsurutsuru.transform.position = _playerManager._calamari.transform.position;
        _playerManager._tsurutsuru.transform.eulerAngles = _playerManager._calamari.transform.eulerAngles;
        _playerManager._calamari.SetActive(false);
        _playerManager._nenchak.SetActive(false);
        _playerManager._tsurutsuru.SetActive(true);
        _cameraPoint.PlayerCameraLook(_playerManager._tsurutsuru);
        _mode = _playerManager._tsurutsuru.name.ToString();
    }

    /// <summary>
    /// モード切り替えエフェクト
    /// </summary>
    /// <param name="position">対象座標</param>
    private void PlayScissorsEffect(Vector3 position)
    {
        _scissorsEffect.SetActive(true);
        _scissorsEffect.transform.position = position;
        if (0 < _particleSystem.Length)
        {
            for (int i = 0; i < _particleSystem.Length; i++)
            {
                _particleSystem[i].Play();
            }
        }
    }
}

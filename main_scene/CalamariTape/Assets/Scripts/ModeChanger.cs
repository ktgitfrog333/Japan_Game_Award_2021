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

    /// <summary>はさみのオブジェクト</summary>
    [SerializeField] private GameObject _scissorsObject;
    /// <summary>はさみのスクリプト</summary>
    [SerializeField] private Scissors _scissors;
    /// <summary>はさみコライダーのスクリプト</summary>
    [SerializeField] private ScissorsCollision _scissorsCollision;

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

        if (_scissorsObject.activeInHierarchy == false)
        {
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
                // ツルツルモードのみ移動中はモード切り替え不可とする
                if (_tsurutsuru.GetComponent<TsuruTsuruMoveController>()._modeChangeEnable == true)
                {
                    _tsurutsuruInput = false;
                    _calamariInput = CrossPlatformInputManager.GetButtonDown("RB");
                    inputActive = true;
                }
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
        if (_mode.Equals(_tsurutsuru.name.ToString()))
        {
            position = _tsurutsuru.transform.position;
            rotation = _tsurutsuru.transform.eulerAngles;
            scale = _tsurutsuru.transform.localScale;
        }
        else if (_mode.Equals(_nenchak.name.ToString()))
        {
            position = _nenchak.transform.position;
            rotation = _nenchak.transform.eulerAngles;
            scale = _nenchak.transform.localScale;
        }
        PlayScissorsAnimation(_calamari.transform, _calamari.name, position, scale);

        _calamari.transform.position = position;
        _calamari.transform.eulerAngles = rotation;
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
        PlayScissorsAnimation(_nenchak.transform, _nenchak.name, _calamari.transform.position, _calamari.transform.localScale);

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
        PlayScissorsAnimation(_tsurutsuru.transform, _tsurutsuru.name, _calamari.transform.position, _calamari.transform.localScale);

        _tsurutsuru.transform.position = _calamari.transform.position;
        _tsurutsuru.transform.eulerAngles = _calamari.transform.eulerAngles;
        _calamari.SetActive(false);
        _nenchak.SetActive(false);
        _tsurutsuru.SetActive(true);
        _cameraPoint.PlayerCameraLook(_tsurutsuru);
        _mode = _tsurutsuru.name.ToString();
    }

    /// <summary>
    /// はさみを有効にしてオブジェクトへ向かうアニメーション
    /// </summary>
    /// <param name="transform">ターゲット位置</param>
    /// <param name="name">衝突対象オブジェクト名</param>
    /// <param name="vector3Position">生成位置の座標</param>
    private void PlayScissorsAnimation(Transform transform, string name, Vector3 vector3Position, Vector3 vector3Scale)
    {
        _scissorsObject.SetActive(true);
        _scissors._target = transform;
        _scissors._tracking = true;
        _scissorsCollision._name = name;

        var position = new Vector3(-2.84f, -0.15f, -1.06f);
        var rotation = new Vector3(-9.88f, -2.13f, -3.58f);
        _scissorsObject.transform.position = MultiplyVector3(vector3Position, vector3Scale) + new Vector3(position.x, position.y, position.z);
        _scissorsObject.transform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);
    }

    /// <summary>
    /// ベクター3の乗算<param/>
    /// スケールをそのまま掛けると離れすぎる為、計算内に補正あり
    /// </summary>
    /// <param name="standard">基準となるベクター位置情報</param>
    /// <param name="multiply">掛ける値（ベクターのスケール指定）</param>
    /// <returns>計算後のベクター位置情報</returns>
    private Vector3 MultiplyVector3(Vector3 standard, Vector3 multiply)
    {
        float valueX = 1.0f + (multiply.x - 1.0f) / 2;
        float valueY = 1.0f + (multiply.y - 1.0f) / 2;
        float valueZ = 1.0f + (multiply.z - 1.0f) / 2;
        return new Vector3(standard.x * valueX, standard.y * valueY, standard.z * valueZ);
    }
}

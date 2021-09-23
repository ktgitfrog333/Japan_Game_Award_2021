using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Gimmicks;
using DeadException;
using Const.Component;

/// <summary>
/// 滑る床
/// </summary>
public class IcePlane : MonoBehaviour
{
    /// <summary>摩擦率</summary>
    [SerializeField, Range(0, 3)] private int _frictionLevel = 1;
    /// <summary>プレイヤーが振り返って移動した際の速度率</summary>
    [SerializeField, Range(0, 3)] private int _changeOverLevel = 1;
    /// <summary>プレイヤーが振り返って移動した際の速度率の最大値</summary>
    private static readonly int CHANGE_OVER_LEVEL_MAX = 3;
    /// <summary>滑る床判定用のフラグ</summary>
    public readonly bool _icePlane = true;
    /// <summary>プレイヤー管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>衝突オブジェクト</summary>
    private GameObject _colliderObj;
    /// <summary>滑る移動方向</summary>
    private Vector3 _iceRunDirection;
    /// <summary>滑る床の移動速度</summary>
    private float _iceGroundSetMoveSpeed;
    /// <summary>滑る床の移動速度（キャッシュ）</summary>
    private float _iceGroundSetMoveSpeedCash;
    /// <summary>プレイヤー側の移動速度の初期値</summary>
    private float _groundSetMoveSpeedCash;
    /// <summary>コルーチン実行中</summary>
    public bool _isRunning { get; private set; }
    /// <summary>移動速度の係数</summary>
    private float _percent = 1f;
    /// <summary>ネンチャクモードのみ向く方向を制御</summary>
    private Vector3 _nenchakLkAt;

    private void Start()
    {
        _colliderObj = null;

        CheckConfig();
    }

    /// <summary>
    /// プレイヤーから実行される　プレイヤーが滑る床にヒットした場合
    /// </summary>
    /// <param name="gameObject">プレイヤー</param>
    public void OnRayHitEnter(GameObject gameObject)
    {
        // プレイヤーのみ
        if (gameObject.name.Equals(_playerManager._calamari.name) || gameObject.name.Equals(_playerManager._nenchak.name) || gameObject.name.Equals(_playerManager._tsurutsuru.name))
        {
            _colliderObj = gameObject;
            if (_colliderObj != null)
            {
                if (0 < _frictionLevel && 0 < _changeOverLevel)
                {
                    ChangeOver(_colliderObj);
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーから実行される　プレイヤーが滑る床にヒットして離れた場合
    /// </summary>
    public void OnRayHitExit()
    {
        ResetChangeOver(_colliderObj);
        _colliderObj = null;
    }

    private void Update()
    {
        if (_colliderObj != null)
        {
            if (0 < _frictionLevel && 0 < _changeOverLevel)
            {
                MoveCollidObject(_colliderObj);
                
            }
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_frictionLevel == 0)
        {
            Debug.Log("摩擦率（_frictionLevel）の未設定");
        }
        if (_changeOverLevel == 0)
        {
            Debug.Log("プレイヤーが振り返って移動した際の速度率（_changeoverLevel）の未設定");
        }
    }

    /// <summary>
    /// 滑る床上での移動入力による速度を制御
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    private void ChangeOver(GameObject obj)
    {
        if (obj.name.Equals(_playerManager._calamari.name))
        {
            // カラマリモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CALAMARI_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<CalamariMoveController>();
                // 滑る床の上の移動速度
                _iceGroundSetMoveSpeed = ctrl._groundSetMoveSpeed;
                _iceGroundSetMoveSpeedCash = _iceGroundSetMoveSpeed;
                // プレイヤー自身の移動速度
                _groundSetMoveSpeedCash = ctrl._groundSetMoveSpeed;
                if (_groundSetMoveSpeedCash == ctrl._groundSetMoveSpeed)
                {
                    ctrl._groundSetMoveSpeed = ctrl._groundSetMoveSpeed * ((float)_changeOverLevel / CHANGE_OVER_LEVEL_MAX);
                }
            }
        }
        else if (obj.name.Equals(_playerManager._nenchak.name))
        {
            // ネンチャクモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.NENCHAK_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<NenchakMoveController>();
                // 滑る床の上の移動速度
                _iceGroundSetMoveSpeed = ctrl._groundSetMoveSpeed;
                _iceGroundSetMoveSpeedCash = _iceGroundSetMoveSpeed;
                // プレイヤー自身の移動速度
                _groundSetMoveSpeedCash = ctrl._groundSetMoveSpeed;
                if (_groundSetMoveSpeedCash == ctrl._groundSetMoveSpeed)
                {
                    ctrl._groundSetMoveSpeed = ctrl._groundSetMoveSpeed * ((float)_changeOverLevel / CHANGE_OVER_LEVEL_MAX);
                    _nenchakLkAt = ctrl._transform.position + new Vector3(ctrl._horizontal, 0f, ctrl._vertical);
                }
            }
        }
        else if (obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            // ツルツルモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.TSURUTSURU_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<TsuruTsuruMoveController>();
                // 滑る床の上の移動速度
                _iceGroundSetMoveSpeed = ctrl._groundSetMoveSpeed;
                _iceGroundSetMoveSpeedCash = _iceGroundSetMoveSpeed;
                // プレイヤー自身の移動速度
                _groundSetMoveSpeedCash = ctrl._groundSetMoveSpeed;
                if (_groundSetMoveSpeedCash == ctrl._groundSetMoveSpeed)
                {
                    ctrl._groundSetMoveSpeed = ctrl._groundSetMoveSpeed * ((float)_changeOverLevel / CHANGE_OVER_LEVEL_MAX);
                }
            }
        }
    }

    /// <summary>
    /// 滑る床上での移動入力による速度を元に戻す
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    private void ResetChangeOver(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.name.Equals(_playerManager._calamari.name))
            {
                // カラマリモード
                if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CALAMARI_MOVE_CONTROLLER) == true)
                {
                    var ctrl = obj.GetComponent<CalamariMoveController>();
                    ctrl._groundSetMoveSpeed = _groundSetMoveSpeedCash;
                }
            }
            else if (obj.name.Equals(_playerManager._nenchak.name))
            {
                // ネンチャクモード
                if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.NENCHAK_MOVE_CONTROLLER) == true)
                {
                    var ctrl = obj.GetComponent<NenchakMoveController>();
                    ctrl._groundSetMoveSpeed = _groundSetMoveSpeedCash;
                }
            }
            else if (obj.name.Equals(_playerManager._tsurutsuru.name))
            {
                // ツルツルモード
                if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.TSURUTSURU_MOVE_CONTROLLER) == true)
                {
                    var ctrl = obj.GetComponent<TsuruTsuruMoveController>();
                    ctrl._groundSetMoveSpeed = _groundSetMoveSpeedCash;
                }
            }
        }
    }

    /// <summary>
    /// 触れたプレイヤーを動かす
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    private void MoveCollidObject(GameObject obj)
    {
        if (obj.name.Equals(_playerManager._calamari.name))
        {
            // カラマリモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CALAMARI_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<CalamariMoveController>();
                if (_isRunning == false && (0.1f < Mathf.Abs(ctrl._horizontal) || 0.1f < Mathf.Abs(ctrl._vertical)))
                {
                    _iceRunDirection = new Vector3(ctrl._horizontal, 0f, ctrl._vertical);
                }
                if (0.1f < Mathf.Abs(_iceRunDirection.magnitude))
                {
                    if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CHARACTER_CONTROLLER) == true)
                    {
                        MoveObject(obj.GetComponent<CharacterController>());
                    }
                }
            }
        }
        else if (obj.name.Equals(_playerManager._nenchak.name))
        {
            // ネンチャクモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.NENCHAK_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<NenchakMoveController>();
                if (_isRunning == false && (0.1f < Mathf.Abs(ctrl._horizontal) || 0.1f < Mathf.Abs(ctrl._vertical)))
                {
                    _iceRunDirection = new Vector3(ctrl._horizontal, 0f, ctrl._vertical);
                    ctrl._transform.LookAt(_nenchakLkAt);
                }
                if (0.1f < Mathf.Abs(_iceRunDirection.magnitude))
                {
                    if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CHARACTER_CONTROLLER) == true)
                    {
                        MoveObject(obj.GetComponent<CharacterController>());
                    }
                }
            }
        }
        else if (obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            // ツルツルモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.TSURUTSURU_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<TsuruTsuruMoveController>();
                if (_isRunning == false && (0.1f < Mathf.Abs(ctrl._horizontal) || 0.1f < Mathf.Abs(ctrl._vertical)))
                {
                    _iceRunDirection = new Vector3(ctrl._horizontal, 0f, ctrl._vertical);
                }
                if (0.1f < Mathf.Abs(_iceRunDirection.magnitude))
                {
                    if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CHARACTER_CONTROLLER) == true)
                    {
                        MoveObject(obj.GetComponent<CharacterController>());
                    }
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーを移動させる
    /// </summary>
    /// <param name="ctrl">プレイヤー</param>
    private void MoveObject(CharacterController ctrl)
    {
        if (_iceGroundSetMoveSpeed == _iceGroundSetMoveSpeedCash)
        {
            if (_isRunning == false)
            {
                StartCoroutine(DownSpeed());
            }
            else
            {
                ctrl.Move(_iceRunDirection * _iceGroundSetMoveSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 速度を少しずつ下げる
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownSpeed()
    {
        if (_isRunning == true)
        {
            yield break;
        }
        _isRunning = true;

        while (0f < _percent)
        {
            _iceGroundSetMoveSpeed = _iceGroundSetMoveSpeedCash * _percent;
            _percent -= 0.1f * _frictionLevel;
            yield return new WaitForSeconds(0.5f);
        }
        _iceGroundSetMoveSpeed = _iceGroundSetMoveSpeedCash;
        _iceRunDirection = Vector3.zero;
        _percent = 1f;
        _isRunning = false;
    }
}

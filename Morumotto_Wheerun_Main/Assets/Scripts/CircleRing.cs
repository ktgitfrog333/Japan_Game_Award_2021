using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Gimmicks;

/// <summary>
/// 回転リング
/// </summary>
public class CircleRing : MonoBehaviour
{
    /// <summary>回転量</summary>
    [SerializeField, Range(0, 3)] public int _spinLevel = 1;
    private float _speed = 30f;
    /// <summary>回転方向</summary>
    [SerializeField, Range(-1, 1)] private int _directionRotat = 0;
    /// <summary>位置・角度・スケール</summary>
    private Transform _transform;

    /// <summary>ギミック実行範囲内検知</summary>
    private bool _playerIn;
    /// <summary>モード名を格納</summary>
    private string _playerModeName;
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>回転リングにリンクした足場</summary>
    [SerializeField] private CircleScaffold _circleScaffold;

    private void Start()
    {
        _transform = transform;
        CheckConfig();
    }

    private void Update()
    {
        InputAxis();
        if (_directionRotat != 0)
        {
            _transform.localEulerAngles += Vector3.back * (_speed * _spinLevel) *  Time.deltaTime * _directionRotat;
            _circleScaffold._transform.localEulerAngles += Vector3.back * (_speed * _spinLevel) * Time.deltaTime * _directionRotat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーがリング内に入った際にフラグを切り替える
        var r = GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, GetType().ToString());
        if (r == 1)
        {
            _playerIn = true;
            _playerModeName = other.gameObject.name;
        }
        else if (r == 0 && _playerIn == true)
        {
            _playerIn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーがリング内に入った際にフラグを切り替える
        var r = GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, GetType().ToString());
        if (r == 1)
        {
            _playerIn = false;
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_spinLevel == 0)
        {
            Debug.Log("回転量（_spinLevel）の未設定");
        }
    }

    /// <summary>
    /// 操作入力の検知
    /// </summary>
    private void InputAxis()
    {
        if (_playerIn == true)
        {
            var h = 0f;
            if (_playerModeName.Equals(_playerManager._calamari.name))
            {
                h = _playerManager._calamariController.MoveVelocityAngl.x;
            }
            else if (_playerModeName.Equals(_playerManager._tsurutsuru.name))
            {
                h = _playerManager._tsurutsuruController._horizontal;
            }
            if (0.1f <= h)
            {
                _directionRotat = 1;
            }
            else if (h <= -0.1f)
            {
                _directionRotat = -1;
            }
            else
            {
                _directionRotat = 0;
            }
        }
        else
        {
            _directionRotat = 0;
        }
    }
}

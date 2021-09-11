using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Conveyor;
using Const.Conveyor;
using DeadException;
using Const.Component;

[RequireComponent(typeof(LinearConveyor))]
/// <summary>
/// ベルトコンベア上でプレイヤーを動かす
/// </summary>
public class ConveyorMoveCharacter : MonoBehaviour
{
    /// <summary>移動する角度</summary>
    [SerializeField, Range(0, 5)] private int _direction;
    //[SerializeField] private Vector3 _direction;
    /// <summary>移動する速度</summary>
    [SerializeField] private float _speed;
    /// <summary>移動する速度</summary>
    public float Speed { get { return _speed; } }
    /// <summary>移動速度の最高値</summary>
    [SerializeField] private float _maxSpeed;
    /// <summary>移動速度の最低値</summary>
    [SerializeField] private float _minSpeed;
    /// <summary>プレイヤー管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>位置・角度・スケール</summary>
    private Transform _transform;
    /// <summary>衝突オブジェクト</summary>
    private Collider _collider;
    /// <summary>有効無効の制御フラグ</summary>
    private bool _active;
    /// <summary>プレイヤー以外のベルトコンベアの挙動</summary>
    private LinearConveyor _linearConveyor;

    private void Start()
    {
        _active = true;
        _collider = null;
        _transform = transform;
        _linearConveyor = GetComponent<LinearConveyor>();
        CheckConfig();
    }

    private void Update()
    {
        if (_active == true && _collider != null)
        {
            MoveCollidObject(_collider.gameObject, _speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if (obj.name.Equals(_playerManager._calamari.name) || obj.name.Equals(_playerManager._nenchak.name) || obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            _collider = other;
            SetSpeed(obj);
        }
        else
        {
            _collider = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj.name.Equals(_playerManager._calamari.name) || obj.name.Equals(_playerManager._nenchak.name) || obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            _collider = null;
        }
    }


    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_speed == 0f)
        {
            Debug.Log("移動速度（_speed）の未設定");
        }
    }

    /// <summary>
    /// プレイヤーのモード情報を参照して、最高速度と最低速度を設定
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    private void SetSpeed(GameObject obj)
    {
        if (obj.name.Equals(_playerManager._calamari.name))
        {
            _maxSpeed = _speed * ConveyorManager.SPEED_MIDDLE_MAGNI;
            _minSpeed = _speed / ConveyorManager.SPEED_HIGH_MAGNI;
        }
        else if (obj.name.Equals(_playerManager._nenchak.name))
        {
            _maxSpeed = _speed;
            _minSpeed = _speed;
        }
        else if (obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            _maxSpeed = _speed * ConveyorManager.SPEED_HIGH_MAGNI;
            _minSpeed = _speed / ConveyorManager.SPEED_MIDDLE_MAGNI;
        }
    }

    /// <summary>
    /// 触れたプレイヤーを動かす
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    /// <param name="speed">移動速度</param>
    private void MoveCollidObject(GameObject obj, float speed)
    {
        if (obj.name.Equals(_playerManager._calamari.name))
        {
            // カラマリモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CALAMARI_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<CalamariMoveController>();
                speed = CalcSpeedInStream(ctrl.MoveVelocityAngl, speed);
            }
        }
        else if (obj.name.Equals(_playerManager._nenchak.name))
        {
            // ネンチャクモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.NENCHAK_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<NenchakMoveController>();
                speed = CalcSpeedInStream(ctrl.MoveVelocityAngl, speed);
            }
        }
        else if (obj.name.Equals(_playerManager._tsurutsuru.name))
        {
            // ツルツルモード
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.TSURUTSURU_MOVE_CONTROLLER) == true)
            {
                var ctrl = obj.GetComponent<TsuruTsuruMoveController>();
                speed = CalcSpeedInStream(ctrl.MoveVelocityAngl, speed);
            }
        }
        MoveObject(obj, speed);
    }

    /// <summary>
    /// 流れに乗せて速度を計算
    /// </summary>
    /// <param name="angle">プレイヤーが向いている角度</param>
    /// <param name="speed">移動する速度</param>
    /// <returns>移動する速度</returns>
    private float CalcSpeedInStream(Vector3 angle, float speed)
    {
        if (_direction == (int)ConveyorMoveMode.RIGHT)
        {
            if (0f < angle.x)
            {
                speed = _maxSpeed;
            }
            else if (angle.x < 0f)
            {
                speed = _minSpeed;
            }
        }
        else if (_direction == (int)ConveyorMoveMode.LEFT)
        {
            if (angle.x < 0f)
            {
                speed = _maxSpeed;
            }
            else if (0f < angle.x)
            {
                speed = _minSpeed;
            }
        }
        else if (_direction == (int)ConveyorMoveMode.UP)
        {
            if (0f < angle.y)
            {
                speed = _maxSpeed;
            }
            else if (angle.y < 0f)
            {
                speed = _minSpeed;
            }
        }
        else if (_direction == (int)ConveyorMoveMode.DOWN)
        {
            if (angle.y < 0f)
            {
                speed = _maxSpeed;
            }
            else if (0f < angle.y)
            {
                speed = _minSpeed;
            }
        }
        else if (_direction == (int)ConveyorMoveMode.FORWARD)
        {
            if (0f < angle.z)
            {
                speed = _maxSpeed;
            }
            else if (angle.z < 0f)
            {
                speed = _minSpeed;
            }
        }
        else if (_direction == (int)ConveyorMoveMode.BACK)
        {
            if (angle.z < 0f)
            {
                speed = _maxSpeed;
            }
            else if (0f < angle.z)
            {
                speed = _minSpeed;
            }
        }

        return speed;
    }

    /// <summary>
    /// プレイヤーを移動させる
    /// </summary>
    /// <param name="obj">プレイヤー</param>
    /// <param name="speed">移動速度</param>
    private void MoveObject(GameObject obj, float speed)
    {
        if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CHARACTER_CONTROLLER) == true)
        {
            var ctrl = obj.GetComponent<CharacterController>();
            Vector3 v = Vector3.zero;
            if (_direction == (int)ConveyorMoveMode.RIGHT)
            {
                v = Vector3.right;
            }
            else if (_direction == (int)ConveyorMoveMode.LEFT)
            {
                v = Vector3.left;
            }
            else if (_direction == (int)ConveyorMoveMode.UP)
            {
                v = Vector3.up;
            }
            else if (_direction == (int)ConveyorMoveMode.DOWN)
            {
                v = Vector3.down;
            }
            else if (_direction == (int)ConveyorMoveMode.BACK)
            {
                v = Vector3.back;
            }
            else if (_direction == (int)ConveyorMoveMode.FORWARD)
            {
                v = Vector3.forward;
            }
            ctrl.Move(v * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// オブジェクトの有効・無効制御
    /// </summary>
    /// <param name="active">制御フラグ（有効・無効）</param>
    public void SetActive(bool active)
    {
        _active = active;
        if (active == false)
        {
            _linearConveyor.ChangeSpeed(0f);
        }
        else
        {
            _linearConveyor.ChangeSpeed(_speed);
        }
        _linearConveyor.enabled = active;
    }
}

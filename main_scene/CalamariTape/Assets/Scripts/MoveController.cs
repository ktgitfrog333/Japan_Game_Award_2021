using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class MoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 100f;
    /// <summary>移動速度の初期値</summary>
    private float _offSetMoveSpeed;
    /// <summary>重力</summary>
    [SerializeField] private float _gravity = 20f;
    /// <summary>拡大率</summary>
    [SerializeField,Range(1, 4)] private float _scale = 1;
    /// <summary>拡大率の一時保存</summary>
    private float _registedScale;

    /// <summary>プレイヤー移動のコントローラー</summary>
    [SerializeField] private CharacterController _characterController;

    /// <summary>移動前の位置・回転を保存</summary>
    private Transform _transform;
    /// <summary>移動時の位置・回転を保存</summary>
    private Vector3 _moveVelocity;

    /// <summary>カメラのトランスフォーム</summary>
    private Transform _mainCameraTransform;
    /// <summary>カメラの正面補正</summary>
    private Vector3 _mainCameraForward;

    /// <summary>スティック入力（横）の保存</summary>
    private float _registedHorizontal;
    /// <summary>スティック入力（縦）の保存</summary>
    private float _registedVertical;

    /// <summary>ジャンプ力の設定値</summary>
    [SerializeField,Range(130,230)] private float _jumpPower = 130f;
    /// <summary>ジャンプ制御値</summary>
    [SerializeField] private float _jumpScale;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _offSetMoveSpeed = _moveSpeed;

        _registedHorizontal = 0f;
        _registedVertical = 0f;
        _jumpScale = _jumpPower;
        if (Camera.main != null)
        {
            _mainCameraTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        CharacterMovement();
    }

    private void Update()
    {
        if (_characterController.isGrounded && _jumpAction != true)
        {
            _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        ScaleChangeForController();
        ScaleChangeForMouse();

        if (_registedScale != _scale)
        {
            _registedScale = _scale;
            _transform.localScale = new Vector3(1, 1, 1) * _scale;
            _moveSpeed = _offSetMoveSpeed * _scale;
        }

        if (1 < _scale)
        {
            _jumpScale = _jumpPower + (25 * _scale);
        }
        else
        {
            _jumpScale = _jumpPower;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Wall"))
        {
            _registedHorizontal = 0f;
            _registedVertical = 0f;
        }
    }

    /// <summary>
    /// コントーローラーによる拡大・縮小
    /// </summary>
    private void ScaleChangeForController()
    {
        // 拡大
        if (CrossPlatformInputManager.GetButton("ScaleUp") == true && CrossPlatformInputManager.GetButton("ScaleDown") == false)
        {
            if (_scale < 4.01f)
            {
                _scale += 0.01f;
            }
            else
            {
                _scale = 4.0f;
            }
        }
        // 縮小
        else if (CrossPlatformInputManager.GetButton("ScaleDown") == true && CrossPlatformInputManager.GetButton("ScaleUp") == false)
        {
            if (0.99f < _scale)
            {
                _scale -= 0.01f;
            }
            else
            {
                _scale = 1.0f;
            }
        }
    }

    /// <summary>
    /// マウスホイールによる拡大・縮小
    /// </summary>
    private void ScaleChangeForMouse()
    {
        var m_scroll = CrossPlatformInputManager.GetAxis("Mouse ScrollWheel");
        // 拡大
        if (0.0f < m_scroll)
        {
            if (_scale + m_scroll < 4.01f)
            {
                _scale += m_scroll;
            }
            else
            {
                _scale = 4.0f;
            }
        }
        // 縮小
        else if (m_scroll < 0.0f)
        {
            if (0.99f < _scale + m_scroll)
            {
                _scale += m_scroll;
            }
            else
            {
                _scale = 1.0f;
            }
        }
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");

        // 入力されたスティック入力をゼロにしない
        if (Mathf.Abs(_registedHorizontal) < Mathf.Abs(h))
        {
            _registedHorizontal = h;
        }
        if (Mathf.Abs(_registedVertical) < Mathf.Abs(v))
        {
            _registedVertical = v;
        }

        _moveVelocity.x = _registedHorizontal * _moveSpeed;
        _moveVelocity.z = _registedVertical * _moveSpeed;

        if (_mainCameraTransform != null)
        {
            _mainCameraForward = Vector3.Scale(_mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            _moveVelocity = _moveVelocity.z * _mainCameraForward + _moveVelocity.x * _mainCameraTransform.right;
        }
        else
        {
            _moveVelocity = _moveVelocity.z * Vector3.forward + _moveVelocity.x * Vector3.right;
        }

        if (_characterController.isGrounded && _jumpAction == true)
        {
            // ジャンプ処理
            _moveVelocity.y = _jumpScale; // ジャンプの際は上方向に移動させる
            _jumpAction = false;
        }
        else
        {
            // 重力による加速
            _moveVelocity.y -= _gravity * Time.deltaTime;
        }

        MoveAndAnimation();
    }

    /// <summary>
    /// キャラクターを動かす
    /// </summary>
    private void MoveAndAnimation()
    {
        // 移動方向に向く
        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        // オブジェクトを動かす
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // 移動スピードをanimatorに反映
        //_movedSpeedToAnimator = new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude;
        //_animator.SetFloat("MoveSpeed", _movedSpeedToAnimator);
    }
}

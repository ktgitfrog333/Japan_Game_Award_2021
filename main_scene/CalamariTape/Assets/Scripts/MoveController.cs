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
    [SerializeField,Range(1, 4)] private int _scale = 1;
    /// <summary>拡大率の一時保存</summary>
    private int _registedScale;

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

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _offSetMoveSpeed = _moveSpeed;

        _registedHorizontal = 0f;
        _registedVertical = 0f;
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
        if (_registedScale != _scale)
        {
            _transform.localScale = new Vector3(1, 1, 1) * _scale;
            _moveSpeed = _offSetMoveSpeed * _scale;
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

        if (_characterController.isGrounded)
        {
            //if (Input.GetButtonDown("Jump"))
            //{
            //    // ジャンプ処理
            //    _moveVelocity.y = _jumpPower; // ジャンプの際は上方向に移動させる
            //}
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

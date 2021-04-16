using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class TsuruTsuruMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 3f;
    /// <summary>移動速度の初期値</summary>
    private float _groundSetMoveSpeed;
    /// <summary>移動速度の初期値</summary>
    private float _airSetMoveSpeed;

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
    [SerializeField] private float _jumpPower = 5f;
    /// <summary>ジャンプ制御値</summary>
    private float _jumpVelocity;
    /// <summary>ジャンプ制御の最大値</summary>
    [SerializeField] private float _jumpMax = 35f;
    /// <summary>ジャンプ制御の最大値の一時保存</summary>
    private float _registedJumpMax;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;

    /// <summary>重力値の加速度</summary>
    private float _gravityAcceleration;

    /// <summary>位置フラグを一時保存</summary>
    [SerializeField] private bool _positionCashDebugOff;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>SE再生中フラグ</summary>
    private bool _sfxPlayed;

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _groundSetMoveSpeed = _moveSpeed;

        _registedHorizontal = 0f;
        _registedVertical = 0f;
        _gravityAcceleration = 0f;
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

        _registedScale = _scale;
        _transform.localScale = new Vector3(1, 1, 1) * _scale;
        // 大きさに合わせて速度を計算
        var x = _scale - 1f;
        x = _moveSpeed + (1f * (x / 3));
        _groundSetMoveSpeed = x;

        // 大きさに合わせてジャンプを計算
        var y = _scale - 1f;
        y = _jumpMax + (10f * (y / 3));
        _registedJumpMax = y;

        // 空中の移動速度補正
        if (_characterController.isGrounded == false)
        {
            _airSetMoveSpeed = 2f;
        }

        // デバッグ：移動計測のコルーチンを起動する
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PositionCash());
        }
    }

    /// <summary>
    /// 移動距離を測定する
    /// </summary>
    /// <returns></returns>
    private IEnumerator PositionCash()
    {
        Debug.Log("計測開始");
        var pos1 = new Vector2(_transform.position.x, _transform.position.z);

        yield return new WaitForSeconds(1f/* * Time.deltaTime*/);

        // 距離を計測
        var pos2 = new Vector2(_transform.position.x, _transform.position.z);
        var distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);
        Debug.Log("計測終了");

        StopCoroutine(PositionCash());
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

        var speed = 0f;
        if (_characterController.isGrounded == true)
        {
            speed = _groundSetMoveSpeed;
        }
        else
        {
            speed = _airSetMoveSpeed;
        }

        _moveVelocity.x = _registedHorizontal * speed;
        _moveVelocity.z = _registedVertical * speed;

        if (_mainCameraTransform != null)
        {
            _mainCameraForward = Vector3.Scale(_mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            _moveVelocity = _moveVelocity.z * _mainCameraForward + _moveVelocity.x * _mainCameraTransform.right;
        }
        else
        {
            _moveVelocity = _moveVelocity.z * Vector3.forward + _moveVelocity.x * Vector3.right;
        }

        if (_characterController.isGrounded == true && _jumpAction == true)
        {
            // ジャンプ処理
            _jumpVelocity += _jumpPower;
            _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
            _gravityAcceleration = 0f;

            // 効果音を再生する
            PlaySoundEffect();
        }
        else if (_characterController.isGrounded == false && _jumpAction == true && _jumpVelocity < _registedJumpMax)
        {
            // ジャンプ処理
            _jumpVelocity += _jumpPower;
            _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
            _gravityAcceleration = 0f;

            // 効果音を再生する
            PlaySoundEffect();
        }
        else
        {
            _jumpAction = false;
            _sfxPlayed = false;
            _jumpVelocity = 0f;
            // 重力による加速
            _gravityAcceleration += Time.deltaTime;
            _moveVelocity.y = Physics.gravity.y * _gravityAcceleration;
        }

        MoveAndAnimation();
    }

    /// <summary>
    /// 効果音を再生する
    /// </summary>
    private void PlaySoundEffect()
    {
        if (_sfxPlayed == false)
        {
            _sfxPlayed = true;
            if (_registedJumpMax < 40)
            {
                _sfxPlay.PlaySFX("jump_1");
            }
            else
            {
                _sfxPlay.PlaySFX("jump_2");
            }
        }
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

        // デバッグ：移動計測のコルーチンを起動する
        if (_positionCashDebugOff == false && (0 < _moveVelocity.x || 0 < _moveVelocity.z))
        {
            _positionCashDebugOff = true;
            StartCoroutine(PositionCash());
        }

        // 移動スピードをanimatorに反映
        //_movedSpeedToAnimator = new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude;
        //_animator.SetFloat("MoveSpeed", _movedSpeedToAnimator);
    }
}

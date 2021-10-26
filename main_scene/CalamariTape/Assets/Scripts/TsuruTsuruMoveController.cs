using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Const.Layer;
using Controller.AllmodeState;
using DeadException;
using Const.Component;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class TsuruTsuruMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 7f;
    /// <summary>移動速度の初期値</summary>
    public float _groundSetMoveSpeed { get; set; }
    /// <summary>移動速度（最大）</summary>
    [SerializeField] private float _maxMoveSpeed = 8f;

    /// <summary>プレイヤー移動のコントローラー</summary>
    [SerializeField] private CharacterController _characterController;

    /// <summary>移動前の位置・回転を保存</summary>
    private Transform _transform;
    /// <summary>移動時の位置・回転を保存</summary>
    private Vector3 _moveVelocity;
    /// <summary>移動時の位置・回転を保存</summary>
    public Vector3 MoveVelocityAngl { get { return _moveVelocity; } }

    /// <summary>カメラのトランスフォーム</summary>
    private Transform _mainCameraTransform;
    /// <summary>カメラの正面補正</summary>
    private Vector3 _mainCameraForward;

    /// <summary>スティック入力（横）の保存</summary>
    public float _horizontal { private set; get; }
    /// <summary>スティック入力（縦）の保存</summary>
    public float _vertical { private set; get; }

    /// <summary>ジャンプ力の設定値</summary>
    [SerializeField] private float _jumpPower = 5f;
    /// <summary>ジャンプ制御値</summary>
    private float _jumpVelocity;
    /// <summary>ジャンプ制御の最大値</summary>
    [SerializeField] private float _jumpMax = 35f;
    /// <summary>ジャンプ制御の最大値の一時保存</summary>
    private float _registedJumpMax;
    /// <summary>ジャンプ制御の最大値（最大）</summary>
    [SerializeField] private float _maxJumpMax = 45f;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;

    /// <summary>アニメーション</summary>
    private float _movedSpeedToAnimator;

    /// <summary>重力値の加速度</summary>
    public float _gravityAcceleration { get; set; }

    /// <summary>位置フラグを一時保存</summary>
    [SerializeField] private bool _positionCashDebugOff;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>SE再生中フラグ</summary>
    private bool _sfxPlayed;

    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] private TsuruTsuruAnimation _animation;

    /// <summary>エフェクトのスクリプト</summary>
    [SerializeField] private TsuruTsuruEffectController _effectController;
    /// <summary>プレイヤーの移動制御を停止するフラグ</summary>
    public bool _characterStop { set; get; } = false;

    /// <summary>移動SE再生中フラグ</summary>
    private bool _sfxPlayedMove;

    /// <summary>地面移動制御</summary>
    [SerializeField] private TsuruTsuruGroundMove _groundMove;
    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private TsuruTsuruScaler _scaler;
    /// <summary>滑る床</summary>
    private IcePlane _icePlane;

    void Start()
    {
        _transform = this.transform;
        _groundSetMoveSpeed = _moveSpeed;

        _horizontal = 0f;
        _vertical = 0f;
        _gravityAcceleration = 0f;
        if (Camera.main != null)
        {
            _mainCameraTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_characterStop == false)
        {
            CharacterMovement();
        }
        if (IsWallCollisitioned() == true)
        {
            _horizontal = 0f;
            _vertical = 0f;
        }
        else
        {
            _effectController._effectActive = false;
        }
    }

    /// <summary>
    /// 外部からツルツルモードの速度を止めたい時に使用する
    /// </summary>
    public void OtherActionTsurutsuruStop()
    {
        _horizontal = 0f;
        _vertical = 0f;
    }

    private void Update()
    {
        if ((AllmodeStateConf.IsGrounded(_characterController, _transform, _groundMove._registMaxDistance) == true/* || AllmodeStateConf.IsConveyor(_transform, _groundMove._registMaxDistance) == true*/) && _jumpAction != true)
        {
            _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        _scaler.ScaleChangeForController();
        _scaler.ScaleChangeForMouse();

        // 大きさに合わせて速度を計算
        _groundSetMoveSpeed = AllmodeStateConf.ParameterMatchScale(_moveSpeed, _maxMoveSpeed, _scaler.Scale);

        // 大きさに合わせてジャンプを計算
        _registedJumpMax = AllmodeStateConf.ParameterMatchScale(_jumpMax, _maxJumpMax, _scaler.Scale);

        // デバッグ：移動計測のコルーチンを起動する
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PositionCash());
        }

        // アニメーションのループ対策
        if (_animation.getAnimationLoop("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator) == true)
        {
            _animation.setAnimetionParameters("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator);
        }

        // 滑る床の上にいるか
        var iceObj = AllmodeStateConf.IsIcePlanedAndObject(_characterController, _transform, _groundMove._registMaxDistance);
        if (iceObj != null && DeadNullReference.CheckReferencedComponent(iceObj, ComponentManager.ICE_PLANE))
        {
            _icePlane = iceObj.GetComponent<IcePlane>();
            _icePlane.OnRayHitEnter(gameObject);
        }
        else if (_icePlane != null)
        {
            _icePlane.OnRayHitExit();
            _icePlane = null;
        }
    }

    private void OnEnable()
    {
        OtherActionTsurutsuruStop();
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");

        // 入力されたスティック入力をゼロにしない
        if (Mathf.Abs(_horizontal) < Mathf.Abs(h))
        {
            _horizontal = h;
        }
        if (Mathf.Abs(_vertical) < Mathf.Abs(v))
        {
            _vertical = v;
        }

        var speed = 0f;
        // 空中の移動速度を地上の移動速度と同様にする
        speed = _groundSetMoveSpeed;

        _moveVelocity.x = _horizontal * speed;
        _moveVelocity.z = _vertical * speed;

        if (_mainCameraTransform != null)
        {
            _mainCameraForward = Vector3.Scale(_mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            _moveVelocity = _moveVelocity.z * _mainCameraForward + _moveVelocity.x * _mainCameraTransform.right;
        }
        else
        {
            _moveVelocity = _moveVelocity.z * Vector3.forward + _moveVelocity.x * Vector3.right;
        }

        if (AllmodeStateConf.IsGrounded(_characterController, _transform, _groundMove._registMaxDistance) == true && _jumpAction == true)
        {
            // ジャンプ処理
            _jumpVelocity += _jumpPower;
            _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
            _gravityAcceleration = 0f;

            // 効果音を再生する
            PlaySoundEffect();
        }
        else if (AllmodeStateConf.IsGrounded(_characterController, _transform, _groundMove._registMaxDistance) == false && _jumpAction == true && _jumpVelocity < _registedJumpMax)
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
            var g = 1f;
            if (_scaler._zeroGravity == true)
            {
                g = 0f;
            }
            // 重力による加速
            _gravityAcceleration += Time.deltaTime;
            _moveVelocity.y = Physics.gravity.y * _gravityAcceleration * g;
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

        // デバッグ：移動計測のコルーチンを起動する
        if (_positionCashDebugOff == false && (0 < _moveVelocity.x || 0 < _moveVelocity.z))
        {
            _positionCashDebugOff = true;
            StartCoroutine(PositionCash());
        }

        // 移動スピードをanimatorに反映
        _movedSpeedToAnimator = new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude;
        _animation.setAnimetionParameters("Morumotto", "MoveSpeed", _movedSpeedToAnimator);
        _animation.setAnimetionParameters("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator);
        if (0 < _movedSpeedToAnimator)
        {
            PlaySoundEffectMove();
        }
        else
        {
            _sfxPlayedMove = false;
        }
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
    /// 移動効果音を再生する
    /// </summary>
    private void PlaySoundEffectMove()
    {
        if (_sfxPlayedMove == false)
        {
            _sfxPlayedMove = true;
            _sfxPlay.PlaySFX("se_move");
        }
    }

    /// <summary>
    /// 壁衝突判定
    /// </summary>
    /// <returns>壁に当たったか否か</returns>
    private bool IsWallCollisitioned()
    {
        bool result = false;
        Debug.DrawRay(_transform.position, Vector3.forward * _groundMove._registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.back * _groundMove._registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.left * _groundMove._registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.right * _groundMove._registMaxDistance, Color.green);

        if (result == false && 0 < _vertical)
        {
            var ray = new Ray(_transform.position, Vector3.forward);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _groundMove._registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                    _animation.PauseAnimation("Scotch_tape_outside");
                }
            }
        }

        if (result == false && _vertical < 0)
        {
            var ray = new Ray(_transform.position, Vector3.back);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _groundMove._registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                    _animation.PauseAnimation("Scotch_tape_outside");
                }
            }
        }

        if (result == false && _horizontal < 0)
        {
            var ray = new Ray(_transform.position, Vector3.left);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _groundMove._registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                    _animation.PauseAnimation("Scotch_tape_outside");
                }
            }
        }

        if (result == false && 0 < _horizontal)
        {
            var ray = new Ray(_transform.position, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _groundMove._registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                    _animation.PauseAnimation("Scotch_tape_outside");
                }
            }
        }

        return result;
    }

    ///// <summary>
    ///// 接地判定
    ///// </summary>
    ///// <returns>接地状態か否か</returns>
    //private bool IsGrounded()
    //{
    //    var result = _characterController.isGrounded;

    //    if (result == false)
    //    {
    //        Debug.DrawRay(_transform.position + Vector3.up * 0.1f, Vector3.down * _groundMove._registMaxDistance, Color.green);
    //        var ray = new Ray(_transform.position + Vector3.up * 0.1f, Vector3.down);
    //        foreach (RaycastHit hit in Physics.RaycastAll(ray, _groundMove._registMaxDistance))
    //        {
    //            if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
    //            {
    //                result = true;
    //            }
    //        }
    //    }

    //    return result;
    //}

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
}

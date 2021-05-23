using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Const.Layer;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class TsuruTsuruMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 7f;
    /// <summary>移動速度の初期値</summary>
    private float _groundSetMoveSpeed;
    /// <summary>移動速度の初期値</summary>
    private float _airSetMoveSpeed;
    /// <summary>移動速度（最大）</summary>
    [SerializeField] private float _maxMoveSpeed = 8f;

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
    /// <summary>ジャンプ制御の最大値（最大）</summary>
    [SerializeField] private float _maxJumpMax = 45f;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;

    /// <summary>アニメーション</summary>
    private float _movedSpeedToAnimator;

    /// <summary>重力値の加速度</summary>
    private float _gravityAcceleration;

    /// <summary>位置フラグを一時保存</summary>
    [SerializeField] private bool _positionCashDebugOff;

    /// <summary>RayCast判定の距離値</summary>
    [SerializeField] private float _maxDistance = 2.3f;
    /// <summary>RayCast判定の距離の処理内で扱う</summary>
    private float _registMaxDistance;
    /// <summary>RayCast判定の距離値（最大）</summary>
    [SerializeField] private float _maxMaxDistance = 8.5f;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>SE再生中フラグ</summary>
    private bool _sfxPlayed;

    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] private Animator _animator;

    /// <summary>テープ（外側）の位置情報</summary>
    [SerializeField] private Transform _tapeOutside;
    /// <summary>モルモットの位置情報</summary>
    [SerializeField] private Transform _morumotto;
    /// <summary>回転スピード</summary>
    [SerializeField] private float _rollSpeed = 5f;

    /// <summary>エフェクトのスクリプト</summary>
    [SerializeField] private TsuruTsuruEffectController _effectController;
    /// <summary>プレイヤーの移動制御を停止するフラグ</summary>
    public bool _characterStop { set; get; } = false;
    /// <summary>プレイヤーのモードチェンジ有効フラグ</summary>
    public bool _modeChangeEnable { set; get; } = true;

    /// <summary>移動SE再生中フラグ</summary>
    private bool _sfxPlayedMove;

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _groundSetMoveSpeed = _moveSpeed;

        _registMaxDistance = _maxDistance;

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
        if (_characterStop == false)
        {
            CharacterMovement();
        }
        if (IsWallCollisitioned() == true)
        {
            _registedHorizontal = 0f;
            _registedVertical = 0f;
            _modeChangeEnable = true;
        }
        else
        {
            _effectController._effectActive = false;
        }
    }

    private void Update()
    {
        if (IsGrounded() && _jumpAction != true)
        {
            _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        ScaleChangeForController();
        ScaleChangeForMouse();

        _registedScale = _scale;
        _transform.localScale = new Vector3(1, 1, 1) * _scale;
        // 大きさに合わせて速度を計算
        _groundSetMoveSpeed = ParameterMatchScale(_moveSpeed, _maxMoveSpeed);

        // 大きさに合わせてジャンプを計算
        _registedJumpMax = ParameterMatchScale(_jumpMax, _maxJumpMax);

        // 大きさに合わせてRayの距離を計算
        _registMaxDistance = ParameterMatchScale(_maxDistance, _maxMaxDistance);

        // 空中の移動速度補正
        if (IsGrounded() == false)
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
            _modeChangeEnable = false;
        }
        if (Mathf.Abs(_registedVertical) < Mathf.Abs(v))
        {
            _registedVertical = v;
            _modeChangeEnable = false;
        }

        var speed = 0f;
        if (IsGrounded() == true)
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

        if (IsGrounded() == true && _jumpAction == true)
        {
            // ジャンプ処理
            _jumpVelocity += _jumpPower;
            _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
            _gravityAcceleration = 0f;

            // 効果音を再生する
            PlaySoundEffect();
        }
        else if (IsGrounded() == false && _jumpAction == true && _jumpVelocity < _registedJumpMax)
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
        _animator.SetFloat("MoveSpeed", _movedSpeedToAnimator);
        if (0 < _movedSpeedToAnimator)
        {
            PlaySoundEffectMove();
        }
        else
        {
            _sfxPlayedMove = false;
        }

        if (0 < _movedSpeedToAnimator)
        {
            RollObject();
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
    /// 移動速度に応じて各オブジェクトを回転させる
    /// </summary>
    private void RollObject()
    {
        _tapeOutside.eulerAngles += new Vector3(0, 0, _rollSpeed * -1);
        _morumotto.eulerAngles += new Vector3(_rollSpeed, 0, 0);
    }

    /// <summary>
    /// 壁衝突判定
    /// </summary>
    /// <returns>壁に当たったか否か</returns>
    private bool IsWallCollisitioned()
    {
        bool result = false;
        Debug.DrawRay(_transform.position, Vector3.forward * _registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.back * _registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.left * _registMaxDistance, Color.green);
        Debug.DrawRay(_transform.position, Vector3.right * _registMaxDistance, Color.green);

        if (result == false && 0 < _registedVertical)
        {
            var ray = new Ray(_transform.position, Vector3.forward);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                }
            }
        }

        if (result == false && _registedVertical < 0)
        {
            var ray = new Ray(_transform.position, Vector3.back);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                }
            }
        }

        if (result == false && _registedHorizontal < 0)
        {
            var ray = new Ray(_transform.position, Vector3.left);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                }
            }
        }

        if (result == false && 0 < _registedHorizontal)
        {
            var ray = new Ray(_transform.position, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                    _effectController.AppearEffectCollision(hit.collider);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns>接地状態か否か</returns>
    private bool IsGrounded()
    {
        var result = _characterController.isGrounded;

        if (result == false)
        {
            Debug.DrawRay(_transform.position + Vector3.up * 0.1f, Vector3.down * _registMaxDistance, Color.green);
            var ray = new Ray(_transform.position + Vector3.up * 0.1f, Vector3.down);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                {
                    result = true;
                }
            }
        }

        return result;
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
    /// スケールの大きさに合わせて各パラメータを調整する
    /// </summary>
    /// <param name="min">基準値（初期値/最小値）</param>
    /// <param name="max">最大値</param>
    /// <returns>計算後の値</returns>
    private float ParameterMatchScale(float min, float max)
    {
        var v = _scale - 1f;
        v = min + ((max - min) * (v / 3));
        return v;
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
}

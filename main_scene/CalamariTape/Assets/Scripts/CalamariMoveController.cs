using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller.WallHorizontal;
using Controller.AllmodeState;
using Const.Layer;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class CalamariMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 5f;
    /// <summary>移動速度の初期値</summary>
    private float _groundSetMoveSpeed;
    /// <summary>移動速度の初期値</summary>
    private float _airSetMoveSpeed;
    /// <summary>移動速度（最大）</summary>
    [SerializeField] private float _maxMoveSpeed = 6f;

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

    /// <summary>移動速度を一時停止する制御フラグ</summary>
    [SerializeField] private bool _calamariStop;

    /// <summary>壁走り</summary>
    [SerializeField] private CalamariWallMove _wallMove;

    /// <summary>2点間の距離を測る際の一つ目を記録したか否か</summary>
    private bool _distanceFirstPointSaved;
    /// <summary>2点間の距離</summary>
    private Vector2 _distancePoint;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>SE再生中フラグ</summary>
    private bool _sfxPlayedJump;

    /// <summary>カラマリモードのアニメーション</summary>
    [SerializeField] private CalamariAnimation _animation;

    /// <summary>プレイヤーの移動制御を停止するフラグ</summary>
    public bool _characterStop { set; get; } = false;
    /// <summary>プレイヤーの移動入力の許可状態フラグ</summary>
    public bool _characterControlInput { set; get; } = true;
    /// <summary>慣性ありフラグ</summary>
    private bool _inertia;

    /// <summary>移動SE再生中フラグ</summary>
    private bool _sfxPlayedMove;
    /// <summary>耐久ゲージ減少SE再生中フラグ</summary>
    private bool _sfxPlayedDerable;

    /// <summary>プレイヤーの耐久値</summary>
    [SerializeField] private CalamariHealth _health;

    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private CalamariScaler _scaler;

    void Start()
    {
        _transform = this.transform;
        _groundSetMoveSpeed = _moveSpeed;

        _gravityAcceleration = 0f;
        if (Camera.main != null)
        {
            _mainCameraTransform = Camera.main.transform;
        }
        var color = _health.ReadMaterial();
        _health.ReflectMaterial(color);
        _health._defaultAlpha = _health.ReadMaterial().a;
    }

    private void FixedUpdate()
    {
        if (_characterStop == false)
        {
            CharacterMovement();
        }
    }

    private void Update()
    {
        if (_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false)
        {
            if (IsGrounded() && _jumpAction != true)
            {
                _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        _scaler.ScaleChangeForController();
        _scaler.ScaleChangeForMouse();

        // 大きさに合わせて速度を計算
        _groundSetMoveSpeed = AllmodeStateConf.ParameterMatchScale(_moveSpeed, _maxMoveSpeed, _scaler.Scale);

        // 大きさに合わせてジャンプを計算
        _registedJumpMax = AllmodeStateConf.ParameterMatchScale(_jumpMax, _maxJumpMax, _scaler.Scale);

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

        // アニメーションのループ対策
        if (_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false)
        {
            if (_animation.getAnimationLoop("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator) == true)
            {
                _animation.setAnimetionParameters("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator);
            }
        }
    }

    private void OnEnable()
    {
        _calamariStop = false;

        // 切り替えた際にSEがオフになっていない場合はオフにする
        if (_sfxPlayedDerable == true)
        {
            _sfxPlayedDerable = false;
            StopCoroutine(SleepTimeSoundEffectDerableDecrease());
        }
    }

    /// <summary>
    /// モードチェンジ時に実行されるメソッド
    /// </summary>
    public void OnChange()
    {
        _wallMove._zeroGravity = false;
        _wallMove._enableGravity = false;

        var c = _health.ReadMaterial();
        _health.ReflectMaterial(c.r, c.g, c.b, _health._defaultAlpha);
        _health._blinkingMaterialStart = false;
        _wallMove._rigidbodyVelocity = Vector3.zero;
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = 0f;
        var v = 0f;
        if (_characterControlInput == true)
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxis("Vertical");
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

        // 移動速度を断続的にする制御
        if (_calamariStop == true)
        {
            h = 0;
            v = 0;
        }

        // 前後方向で登る制御
        if (0 < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunVertical == true && _wallMove._wallRunHorizontal == false)
        {
            Debug.DrawRay(_transform.position, Vector3.down * _wallMove._registMaxDistance, Color.green);
            if (0f < v)
            {
                _moveVelocity.y = v * speed;
            }
            else if (v < 0f && Physics.Raycast(_transform.position, Vector3.down, _wallMove._registMaxDistance) == true)
            {
                _moveVelocity.z = v * speed;
            }
            else if (v < 0f)
            {
                _moveVelocity.y = v * speed;
            }
            else
            {
                _moveVelocity.y = 0f;
            }
            _moveVelocity.x = h * speed;
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        // 横方向で登る制御
        else if (0 < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunHorizontal == true)
        {
            if (_wallMove._enableGravity == false)
            {
                // 右側に壁があった際の床上移動と壁移動
                if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                {
                    Debug.DrawRay(_transform.position, Vector3.down * _wallMove._registMaxDistance, Color.green);
                    if (0f < h)
                    {
                        _moveVelocity.y = h * speed * _wallMove._wallRunHorizontalMode;
                    }
                    else if (h < 0f && Physics.Raycast(_transform.position, Vector3.down, _wallMove._registMaxDistance) == true)
                    {
                        _moveVelocity.x = h * speed;
                    }
                    else if (h < 0f)
                    {
                        _moveVelocity.y = h * speed * _wallMove._wallRunHorizontalMode;
                    }
                    else
                    {
                        _moveVelocity.y = 0f;
                    }
                }
                // 左側に壁があった際の床上移動と壁移動
                else if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                {
                    Debug.DrawRay(_transform.position, Vector3.down * _wallMove._registMaxDistance, Color.green);
                    if (h < 0f)
                    {
                        _moveVelocity.y = h * speed * _wallMove._wallRunHorizontalMode;
                    }
                    else if (0f < h && Physics.Raycast(_transform.position, Vector3.down, _wallMove._registMaxDistance) == true)
                    {
                        _moveVelocity.x = h * speed;
                    }
                    else if (0f < h)
                    {
                        _moveVelocity.y = h * speed * _wallMove._wallRunHorizontalMode;
                    }
                    else
                    {
                        _moveVelocity.y = 0f;
                    }
                }
            }
            else
            {
                ControllGravity();
            }
            _moveVelocity.z = v * speed;
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        // 壁を登らない
        else
        {
            // 慣性あり
            if ((0 < _moveVelocity.x && h < 0) || (_moveVelocity.x < 0 && 0 < h) || _inertia == true)
            {
                _inertia = true;
                _moveVelocity.x = (_moveVelocity.x + h * 0.5f) * speed;
            }
            // 慣性なし
            else
            {
                _moveVelocity.x = h * speed;
            }

            // 慣性あり
            if ((0 < _moveVelocity.z && v < 0) || (_moveVelocity.z < 0 && 0 < v) || _inertia == true)
            {
                _inertia = true;
                _moveVelocity.z = (_moveVelocity.z + v * 0.5f) * speed;
            }
            // 慣性なし
            else
            {
                _moveVelocity.z = v * speed;
            }
        }
        // 壁を登らない
        if (_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false)
        {
            if (_mainCameraTransform != null)
            {
                _mainCameraForward = Vector3.Scale(_mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
                _moveVelocity = _moveVelocity.z * _mainCameraForward + _moveVelocity.x * _mainCameraTransform.right;
            }
            else
            {
                _moveVelocity = _moveVelocity.z * Vector3.forward + _moveVelocity.x * Vector3.right;
            }
        }
        // 壁を登らない（または耐久値ゼロ）
        if ((_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false) || _health.Parameter <= 0)
        {
            if (IsGrounded() == true && _jumpAction == true)
            {
                // ジャンプ処理
                _jumpVelocity += _jumpPower;
                _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
                _gravityAcceleration = 0f;

                // 効果音を再生する
                PlaySoundEffectJump();
            }
            else if (IsGrounded() == false && _jumpAction == true && _jumpVelocity < _registedJumpMax)
            {
                // ジャンプ処理
                _jumpVelocity += _jumpPower;
                _moveVelocity.y = _jumpVelocity; // ジャンプの際は上方向に移動させる
                _gravityAcceleration = 0f;

                // 効果音を再生する
                PlaySoundEffectJump();
            }
            else if (IsGrounded() == false)
            {
                // 重力制御
                ControllGravity();
                if (_jumpAction == true)
                {
                    _jumpAction = false;
                }
            }
            else
            {
                _jumpAction = false;
                _sfxPlayedJump = false;
                _jumpVelocity = 0f;
            }
        }

        MoveAndAnimation();

        if (IsGrounded() == false || _inertia == true)
        {
            // 値を0へ戻す
            _moveVelocity = Vector3.zero;
            _movedSpeedToAnimator = 0f;
            StartCoroutine(inertiaCancel());
        }
    }

    /// <summary>
    /// 重力制御
    /// </summary>
    private void ControllGravity()
    {
        // 重力による加速
        _gravityAcceleration += Time.deltaTime;
        var g = 1f;
        var mx = 1f;
        if (_wallMove._zeroGravity == true)
        {
            g = 0f;
            mx = 5f;
            _moveVelocity.x *= mx;
        }
        _moveVelocity.y = Physics.gravity.y * _gravityAcceleration * g;
    }

    /// <summary>
    /// 一定時間後に移動入力を許可する
    /// </summary>
    /// <returns></returns>
    private IEnumerator inertiaCancel()
    {
        yield return new WaitForSeconds(1.5f);
        _inertia = false;
        StopCoroutine(inertiaCancel());
    }

    /// <summary>
    /// キャラクターを動かす
    /// </summary>
    private void MoveAndAnimation()
    {
        // 移動方向に向く
        CharacterLookAt();

        // オブジェクトを動かす
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // デバッグ：移動計測のコルーチンを起動する
        if (_positionCashDebugOff == false && (0 < _moveVelocity.x || 0 < _moveVelocity.z))
        {
            _positionCashDebugOff = true;
            // デバッグ用のコメントアウトの為、残す
            //StartCoroutine(PositionCash());
        }

        /*
         移動スピードをanimatorに反映
         ・かつ、横向きの壁を移動中　または　縦向きの壁を移動中
         ・かつ、耐久ゲージが残っている
         ・かつ、粘着フラグが有効である
         */
        if ((_wallMove._wallRunVertical == true || _wallMove._wallRunHorizontal == true) && 0 < _health.Parameter && _health.Adhesive == true)
        {
            _movedSpeedToAnimator = new Vector3(_moveVelocity.x, _moveVelocity.y, 0).magnitude;
        }
        else
        {
            _movedSpeedToAnimator = new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude;
        }

        var hAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        var vAxis = CrossPlatformInputManager.GetAxis("Vertical");
        if (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis))
        {
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

        // 2点間の距離を測って一時的に停止する処理を呼び出す
        if (0 < _movedSpeedToAnimator && (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis)))
        {
            if (_distanceFirstPointSaved == false)
            {
                _distanceFirstPointSaved = true;
                // 壁を登らない
                if (_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false)
                {
                    _distancePoint = new Vector2(_transform.position.x, _transform.position.z);
                }
                // 壁を登る
                else
                {
                    _distancePoint = new Vector2(_transform.position.x, _transform.position.y);
                }
            }
            else
            {
                var point = new Vector2();
                // 壁を登らない
                if (_wallMove._wallRunVertical == false && _wallMove._wallRunHorizontal == false)
                {
                    point = new Vector2(_transform.position.x, _transform.position.z);
                }
                // 壁を登る
                else
                {
                    point = new Vector2(_transform.position.x, _transform.position.y);
                }
                var distance = Vector2.Distance(_distancePoint, point);
                if (4 < Mathf.Abs(distance) && _distanceFirstPointSaved == true)
                {
                    _distanceFirstPointSaved = false;
                    StartCoroutine(CalamariStop());
                }
            }
        }

        /*
         テープの耐久ゲージを減らす
         ・回転アニメーションが再生されている
         ・かつ、耐久ゲージが残っている
         ・かつ、粘着フラグが有効である
         ・かつ、横向きの壁を移動中　または　縦向きの壁を移動中
         ・かつ、動く壁が止まっている
         ・かつ、縦方向の入力中　または　横方向の入力中
         */
        if (0 < _movedSpeedToAnimator && 0 < _health.Parameter && _health.Adhesive == true && (_wallMove._wallRunVertical == true || _wallMove._wallRunHorizontal == true) && (Mathf.Abs(_wallMove._rigidbodyVelocity.magnitude) <= 0f) && (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis)))
        {
            PlaySoundEffectDerableDecrease();

            _health.Parameter -= Time.deltaTime;
            var m = _health.ReadMaterial();
            var a = _health.CalcAlpha(_health.Parameter);
            _health.ReflectMaterial(m.r, m.g, m.b, a);
            Debug.Log("耐久値：" + _health.Parameter);
            if (0f < _health.Parameter && _health.Parameter < 2f)
            {
                if (_health._blinkingMaterialStart == false)
                {
                    _health._blinkingMaterialStart = true;
                    StartCoroutine(_health.BlinkingMaterial());
                }
            }
            else if (_health.Parameter <= 0)
            {
                _health.Parameter = 0f;
                _health.Adhesive = false;
                Debug.Log("耐久値無し");
            }
        }
    }

    /// <summary>
    /// 耐久ゲージ減少SEを再生する
    /// </summary>
    private void PlaySoundEffectDerableDecrease()
    {
        if (_sfxPlayedDerable == false)
        {
            _sfxPlayedDerable = true;
            StartCoroutine(SleepTimeSoundEffectDerableDecrease());
            _sfxPlay.PlaySFX("se_derable_decrease");
        }
    }

    /// <summary>
    /// 耐久ゲージ減少SEの連続再生を防ぐ処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator SleepTimeSoundEffectDerableDecrease()
    {
        yield return new WaitForSeconds(0.75f);
        _sfxPlayedDerable = false;
        StopCoroutine(SleepTimeSoundEffectDerableDecrease());
    }

    /// <summary>
    /// キャラクターを動かす際の向きを調整する
    /// ※各モードによって角度が異なるため注意
    /// </summary>
    private void CharacterLookAt()
    {
        var hAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        var vAxis = CrossPlatformInputManager.GetAxis("Vertical");
        if (_wallMove._wallRunVertical == true)
        {
            if (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis))
            {
                if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.x))
                {
                    if (Mathf.Abs(_moveVelocity.x) < Mathf.Abs(_moveVelocity.y))
                    {
                        // 上方向なら縦向き
                        if (0f < _moveVelocity.y && 0.1f <= vAxis)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 0f);
                        }
                        // 下向きなら縦向き
                        else if (_moveVelocity.y < 0f && vAxis <= -0.1f)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, 0f);
                        }
                    }
                    else
                    {
                        // 左向きなら横向き
                        if (0f < _moveVelocity.x && 0.1f <= hAxis)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, -90f);
                        }
                        // 右向きなら横向き
                        else if (_moveVelocity.x < 0f && hAxis <= -0.1f)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 90f);
                        }
                    }
                }
            }
        }
        else if (_wallMove._wallRunHorizontal == true)
        {
            if (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis))
            {
                if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.z))
                {
                    if (Mathf.Abs(_moveVelocity.y) < Mathf.Abs(_moveVelocity.z))
                    {
                        // 右側に壁があった際の床上移動と壁移動
                        if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                        {
                            // 正面なら縦向き
                            if (0f < _moveVelocity.z && 0.1f <= vAxis)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 90f);
                            }
                            else if (_moveVelocity.z < 0f && vAxis <= -0.1f)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, -90f);
                            }
                        }
                        // 左側に壁があった際の床上移動と壁移動
                        else if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                        {
                            // 正面なら縦向き
                            if (0f < _moveVelocity.z && 0.1f <= vAxis)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, -90f);
                            }
                            else if (_moveVelocity.z < 0f && vAxis <= -0.1f)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, 90f);
                            }
                        }
                    }
                    else
                    {
                        // 右側に壁があった際の床上移動と壁移動
                        if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                        {
                            // 左向きなら横向き
                            if (0f < _moveVelocity.y && 0.1f <= hAxis)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 90f, 0f);
                            }
                            // 右向きなら横向き
                            else if (_moveVelocity.y < 0f && hAxis <= -0.1f)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, -90f, 0f);
                            }
                        }
                        // 左側に壁があった際の床上移動と壁移動
                        else if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                        {
                            // 左向きなら横向き
                            if (0f < _moveVelocity.y && hAxis <= -0.1f)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, -90f, 0f);
                            }
                            // 右向きなら横向き
                            else if (_moveVelocity.y < 0f && 0.1f <= hAxis)
                            {
                                _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 90f, 0f);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
    }

    /// <summary>
    /// 移動を断続的に停止
    /// </summary>
    /// <returns></returns>
    private IEnumerator CalamariStop()
    {
        if (_calamariStop == false)
        {
            _calamariStop = true;
        }

        _animation.PauseAnimation("Scotch_tape_outside");

        yield return new WaitForSeconds(1f);

        if (_calamariStop == true)
        {
            _calamariStop = false;
        }

        _animation.PlayAnimation("Scotch_tape_outside");

        StopCoroutine(CalamariStop());
    }

    /// <summary>
    /// ジャンプ効果音を再生する
    /// </summary>
    private void PlaySoundEffectJump()
    {
        if (_sfxPlayedJump == false)
        {
            _sfxPlayedJump = true;
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
    /// 接地判定
    /// </summary>
    /// <returns>接地状態か否か</returns>
    private bool IsGrounded()
    {
        var result = _characterController.isGrounded;

        if (result == false)
        {
            Debug.DrawRay(_transform.position + Vector3.up * 0.1f, Vector3.down * _wallMove._registMaxDistance, Color.green);
            var ray = new Ray(_transform.position + Vector3.up * 0.1f, Vector3.down);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _wallMove._registMaxDistance))
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
    /// 移動距離を測定する
    /// </summary>
    /// <returns></returns>
    private IEnumerator PositionCash()
    {
        Debug.Log("計測開始");
        var pos1 = new Vector2(_transform.position.x, _transform.position.z);

        yield return new WaitForSeconds(1f);

        // 距離を計測
        var pos2 = new Vector2(_transform.position.x, _transform.position.z);
        var distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);
        Debug.Log("計測終了");

        StopCoroutine(PositionCash());
    }
}

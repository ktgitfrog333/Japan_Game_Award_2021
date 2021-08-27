using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller.WallHorizontal;
using Controller.AllmodeState;
using Const.Tag;
using Const.Layer;
using Controller.WallVertical;

/// <summary>
/// プレイヤー操作スクリプトクラス
/// </summary>
public class NenchakMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float _moveSpeed = 3f;
    /// <summary>移動速度の初期値</summary>
    private float _groundSetMoveSpeed;
    /// <summary>移動速度（最大）</summary>
    [SerializeField] private float _maxMoveSpeed = 4f;

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

    /// <summary>位置フラグを一時保存</summary>
    [SerializeField] private bool _positionCashDebugOff;

    /// <summary>アニメーション</summary>
    private float _movedSpeedToAnimator;

    /// <summary>重力値の加速度</summary>
    private float _gravityAcceleration;
    /// <summary>壁走り</summary>
    [SerializeField] private NenchakWallMove _wallMove;

    /// <summary>ネンチャクモードのアニメーション</summary>
    [SerializeField] private NenchakAnimation _animation;

    /// <summary>テープ（外側）の位置情報</summary>
    [SerializeField] private Transform _tapeOutside;
    /// <summary>モルモットの位置情報</summary>
    [SerializeField] private Transform _morumotto;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>移動SE再生中フラグ</summary>
    private bool _sfxPlayedMove;
    /// <summary>耐久ゲージ減少SE再生中フラグ</summary>
    private bool _sfxPlayedDerable;

    /// <summary>プレイヤーの耐久値</summary>
    [SerializeField] private NenchakHealth _health;

    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private NenchakScaler _scaler;

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
        CharacterMovement();
        if (_wallMove._wallRunVertical == true)
        {
            _wallMove._wallRunVerticalLast = _wallMove._wallRunVertical;
        }
    }

    private void Update()
    {
        _scaler.ScaleChangeForController();
        _scaler.ScaleChangeForMouse();

        // 大きさに合わせて速度を計算
        _groundSetMoveSpeed = AllmodeStateConf.ParameterMatchScale(_moveSpeed, _maxMoveSpeed, _scaler.Scale);

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
        // 切り替えた際にSEがオフになっていない場合はオフにする
        if (_sfxPlayedDerable == true)
        {
            _sfxPlayedDerable = false;
            StopCoroutine(SleepTimeSoundEffectDerableDecrease());
        }
    }

    /// <summary>
    /// モードチェンジで実行するメソッド
    /// </summary>
    public void OnChange()
    {
        var c = _health.ReadMaterial();
        _health.ReflectMaterial(c.r, c.g, c.b, _health._defaultAlpha);
        _health._blinkingMaterialStart = false;
        //_wallMove._rigidbodyVelocity = Vector3.zero;
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");

        if (0f < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunVertical == true && _wallMove._wallRunHorizontal == false && _wallMove._wallRunedHztl == false)
        {
            // 前後方向で登る制御
            _moveVelocity.x = h * _groundSetMoveSpeed;
            _moveVelocity.y = v * _groundSetMoveSpeed;
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        else if (0f < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunVertical == false && _wallMove._wallRunedVtcl == true && _wallMove._wallRunHorizontal == false && _wallMove._wallRunedHztl == false)
        {
            // 耐久値がある内は縦方向の入力で登る壁に対して壁の外に出ない挙動にする
            var wall = NenchakWallVerticalDecision.CheckClear(_wallMove._clearVtclWall);
            if (wall != null)
            {
                var direction = NenchakWallVerticalDecision.ShowDirection(wall);
                if ((v < 0f && direction == (int)WallRunVerticalMode.TOP) || (0f < v && direction == (int)WallRunVerticalMode.BOTTOM))
                {
                    // 上には移動出来ないが下には移動出来る
                    // 下には移動出来ないが上には移動出来る
                    _moveVelocity.y = v * _groundSetMoveSpeed;
                }
                else if ((h < 0f && direction == (int)WallRunVerticalMode.RIGHT) || (0f < h && direction == (int)WallRunVerticalMode.LEFT))
                {
                    // 右には移動出来ないが左には移動出来る
                    // 左には移動出来ないが右には移動出来る
                    _moveVelocity.x = h * _groundSetMoveSpeed;
                }
            }
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        else if (0 < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunHorizontal == true)
        {
            // 横方向で登る制御
            _moveVelocity.y = h * _groundSetMoveSpeed * _wallMove._wallRunHorizontalMode;
            _moveVelocity.z = v * _groundSetMoveSpeed;
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        else if (0f < _health.Parameter && _health.Adhesive == true && _wallMove._wallRunedHztl == true)
        {
            // 耐久値がある内は横方向の入力で登る壁に対して壁の外に出ない挙動にする
            var wall = NenchakWallHorizontalDecision.CheckClear(_wallMove._clearHztlWall);
            if (wall != null)
            {
                if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                {
                    var direction = NenchakWallHorizontalDecision.ShowDirection(wall);
                    if ((v < 0f && direction == (int)WallRunHorizontalMode.LEFT) || (0f < v && direction == (int)WallRunHorizontalMode.RIGHT))
                    {
                        // 奥には移動出来ないが手前には移動出来る
                        // 手前には移動出来ないが奥には移動出来る
                        _moveVelocity.z = v * _groundSetMoveSpeed;
                    }
                    else if ((h < 0f && direction == (int)WallRunHorizontalMode.TOP) || (0f < h && direction == (int)WallRunHorizontalMode.BOTTOM))
                    {
                        // 上には移動出来ないが下には移動出来る
                        // 下には移動出来ないが上には移動出来る
                        _moveVelocity.y = h * _groundSetMoveSpeed * _wallMove._wallRunHorizontalMode;
                    }
                }
                else if (_wallMove._wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                {
                    var direction = NenchakWallHorizontalDecision.ShowDirection(wall);
                    if ((v < 0f && direction == (int)WallRunHorizontalMode.LEFT) || (0f < v && direction == (int)WallRunHorizontalMode.RIGHT))
                    {
                        // 奥には移動出来ないが手前には移動出来る
                        // 手前には移動出来ないが奥には移動出来る
                        _moveVelocity.z = v * _groundSetMoveSpeed;
                    }
                    else if ((h < 0f && direction == (int)WallRunHorizontalMode.BOTTOM) || (0f < h && direction == (int)WallRunHorizontalMode.TOP))
                    {
                        // 上には移動出来ないが下には移動出来る
                        // 下には移動出来ないが上には移動出来る
                        _moveVelocity.y = h * _groundSetMoveSpeed * _wallMove._wallRunHorizontalMode;
                    }
                }
            }
            // 動く壁だった場合は壁の移動位置に合わせてプレイヤーを移動させる
            _moveVelocity += _wallMove._rigidbodyVelocity;
        }
        else if (_health.Parameter <= 0f && _health.Adhesive == false)
        {
            _moveVelocity.x = 0f;
            // 重力による加速
            _gravityAcceleration += Time.deltaTime;
            _moveVelocity.y = Physics.gravity.y * _gravityAcceleration;
        }

        MoveAndAnimation();

        // 値を0へ戻す
        _moveVelocity = Vector3.zero;
        _movedSpeedToAnimator = 0f;
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
        if (_positionCashDebugOff == false && (0 < _moveVelocity.x || 0 < _moveVelocity.y))
        {
            _positionCashDebugOff = true;
            StartCoroutine(PositionCash());
        }

        // 移動スピードをanimatorに反映
        _movedSpeedToAnimator = new Vector3(_moveVelocity.x, _moveVelocity.y, _moveVelocity.z).magnitude;

        var hAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        var vAxis = CrossPlatformInputManager.GetAxis("Vertical");
        if (0 < _health.Parameter && _health.Adhesive == true && (0.1f <= Mathf.Abs(hAxis) || 0.1f <= Mathf.Abs(vAxis)))
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
                _animation.PauseAnimation("Scotch_tape_outside");
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
            if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.z))
            {
                if (Mathf.Abs(_moveVelocity.y) < Mathf.Abs(_moveVelocity.z))
                {
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
        else
        {
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
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

        yield return new WaitForSeconds(1f/* * Time.deltaTime*/);

        // 距離を計測
        var pos2 = new Vector2(_transform.position.x, _transform.position.z);
        var distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);
        Debug.Log("計測終了");

        StopCoroutine(PositionCash());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller.Wall;
using Const.Tag;
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

    /// <summary>耐久ゲージ</summary>
    [SerializeField] private DurableValue _value;

    /// <summary>重力値の角度</summary>
    [SerializeField] private Vector3 _wallPosition;

    /// <summary>壁走り（縦）</summary>
    [SerializeField] private bool _wallRunVertical = false;
    /// <summary>壁走り（横）</summary>
    [SerializeField] private bool _wallRunHorizontal = false;
    /// <summary>RayCast判定の距離値</summary>
    [SerializeField] private float _maxDistance = 2.3f;
    /// <summary>RayCast判定の距離の処理内で扱う</summary>
    private float _registMaxDistance;
    /// <summary>RayCast判定の距離値（最大）</summary>
    [SerializeField] private float _maxMaxDistance = 8.5f;

    /// <summary>2点間の距離を測る際の一つ目を記録したか否か</summary>
    private bool _distanceFirstPointSaved;
    /// <summary>2点間の距離</summary>
    private Vector2 _distancePoint;

    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] private SfxPlay _sfxPlay;
    /// <summary>SE再生中フラグ</summary>
    private bool _sfxPlayedJump;

    /// <summary>
    /// 横にある壁に対して横方向へ入力すると登るモード<para/>
    /// 1：右方向入力で登り、左方向で下りる<para/>
    /// -1：左方向入力で登り、右方向で下りる
    /// </summary>
    private int _wallRunHorizontalMode = (int)WallRunHorizontalMode.RIGHT_IS_FRONT;

    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] private Animator _animator;

    /// <summary>テープ（外側）の位置情報</summary>
    [SerializeField] private Transform _tapeOutside;
    /// <summary>モルモットの位置情報</summary>
    [SerializeField] private Transform _morumotto;
    /// <summary>回転スピード</summary>
    [SerializeField] private float _rollSpeed = 5f;

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
    /// <summary>スケール拡大SE再生可フラグ</summary>
    private bool _sfxPlayedScaleUp;

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _groundSetMoveSpeed = _moveSpeed;
        _registMaxDistance = _maxDistance;
        _sfxPlayedScaleUp = true;

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
    }

    private void Update()
    {
        if (_wallRunVertical == false && _wallRunHorizontal == false)
        {
            if (IsGrounded() && _jumpAction != true)
            {
                _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
            }
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

    private void OnTriggerStay(Collider other)
    {
        // 前後にある壁に対して前後方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = true;
            _wallRunHorizontal = false;
            var r = other.gameObject.transform.position;
            _wallPosition = new Vector3(r.x, r.y, r.z);
        }

        // 横にある壁に対して横方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.HORIZONTAL_WALL))
        {
            _wallRunHorizontal = true;
            _wallRunVertical = false;

            var i = _transform.position;
            Debug.DrawRay(i, Vector3.right * _registMaxDistance, Color.green);
            Debug.DrawRay(i, Vector3.left * _registMaxDistance, Color.green);
            if (Physics.Raycast(i, Vector3.right, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int) WallRunHorizontalMode.RIGHT_IS_FRONT;
            }
            else if (Physics.Raycast(i, Vector3.left, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int) WallRunHorizontalMode.LEFT_IS_FRONT;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 前後方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = false;
        }

        // 横方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.HORIZONTAL_WALL))
        {
            _wallRunHorizontal = false;
        }
    }

    private void OnEnable()
    {
        _wallRunVertical = false;
        _wallRunHorizontal = false;
        _calamariStop = false;
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
        if (0 < _value._parameter && _value._adhesive == true && _wallRunVertical == true && _wallRunHorizontal == false)
        {
            Debug.DrawRay(_transform.position, Vector3.down * _registMaxDistance, Color.green);
            if (0f < v)
            {
                _moveVelocity.y = v * speed;
            }
            else if (v < 0f && Physics.Raycast(_transform.position, Vector3.down, _registMaxDistance) == true)
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
        }
        // 横方向で登る制御
        else if (0 < _value._parameter && _value._adhesive == true && _wallRunHorizontal == true)
        {
            // 右側に壁があった際の床上移動と壁移動
            if (_wallRunHorizontalMode == (int)WallRunHorizontalMode.RIGHT_IS_FRONT)
            {
                Debug.DrawRay(_transform.position, Vector3.down * _registMaxDistance, Color.green);
                if (0f < h)
                {
                    _moveVelocity.y = h * speed * _wallRunHorizontalMode;
                }
                else if (h < 0f && Physics.Raycast(_transform.position, Vector3.down, _registMaxDistance) == true)
                {
                    _moveVelocity.x = h * speed;
                }
                else if (h < 0f)
                {
                    _moveVelocity.y = h * speed * _wallRunHorizontalMode;
                }
                else
                {
                    _moveVelocity.y = 0f;
                }
            }
            // 左側に壁があった際の床上移動と壁移動
            else if (_wallRunHorizontalMode == (int)WallRunHorizontalMode.LEFT_IS_FRONT)
            {
                Debug.DrawRay(_transform.position, Vector3.down * _registMaxDistance, Color.green);
                if (h < 0f)
                {
                    _moveVelocity.y = h * speed * _wallRunHorizontalMode;
                }
                else if (0f < h && Physics.Raycast(_transform.position, Vector3.down, _registMaxDistance) == true)
                {
                    _moveVelocity.x = h * speed;
                }
                else if (0f < h)
                {
                    _moveVelocity.y = h * speed * _wallRunHorizontalMode;
                }
                else
                {
                    _moveVelocity.y = 0f;
                }
            }
            _moveVelocity.z = v * speed;
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

        if (_wallRunVertical == false && _wallRunHorizontal == false)
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

        if ((_wallRunVertical == false && _wallRunHorizontal == false) || _value._parameter <= 0)
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
                // 重力による加速
                _gravityAcceleration += Time.deltaTime;
                _moveVelocity.y = Physics.gravity.y * _gravityAcceleration;
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
            //StartCoroutine(PositionCash());
        }

        // 移動スピードをanimatorに反映
        if (_wallRunVertical == true && 0 < _value._parameter && _value._adhesive == true)
        {
            _movedSpeedToAnimator = new Vector3(_moveVelocity.x, _moveVelocity.y, 0).magnitude;
        }
        else
        {
            _movedSpeedToAnimator = new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude;
        }
        _animator.SetFloat("MoveSpeed", _movedSpeedToAnimator);
        if (0 < _movedSpeedToAnimator)
        {
            PlaySoundEffectMove();
        }
        else
        {
            _sfxPlayedMove = false;
        }

        // 2点間の距離を測って一時的に停止する処理を呼び出す
        if (0 < _movedSpeedToAnimator)
        {
            RollObject();
            if (_distanceFirstPointSaved == false)
            {
                _distanceFirstPointSaved = true;
                // 壁を登らない
                if (_wallRunVertical == false && _wallRunHorizontal == false)
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
                if (_wallRunVertical == false && _wallRunHorizontal == false)
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

        // テープの耐久ゲージを減らす
        if (0 < _movedSpeedToAnimator && 0 < _value._parameter && _value._adhesive == true && (_wallRunVertical == true || _wallRunHorizontal == true))
        {
            _value._parameter -= Time.deltaTime;
            PlaySoundEffectDerableDecrease();

            Debug.Log("耐久値：" + _value._parameter);
            if (_value._parameter <= 0)
            {
                _value._parameter = 0f;
                _value._adhesive = false;
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
        if (_wallRunVertical == true)
        {
            if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.x))
            {
                if (Mathf.Abs(_moveVelocity.x) < Mathf.Abs(_moveVelocity.y))
                {
                    // 上方向なら縦向き
                    if (0f < _moveVelocity.y)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, 0f);
                    }
                    // 下向きなら縦向き
                    else if (_moveVelocity.y < 0f)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 0f);
                    }
                }
                else
                {
                    // 左向きなら横向き
                    if (0f < _moveVelocity.x)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 90f);
                    }
                    // 右向きなら横向き
                    else if (_moveVelocity.x < 0f)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, -90f);
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
        else
        {
            _calamariStop = false;
        }

        yield return new WaitForSeconds(1f);

        if (_calamariStop == false)
        {
            _calamariStop = true;
        }
        else
        {
            _calamariStop = false;
        }
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
    /// 移動速度に応じて各オブジェクトを回転させる
    /// </summary>
    private void RollObject()
    {
        if (_calamariStop == false)
        {
            if (_wallRunVertical == true)
            {
                _tapeOutside.eulerAngles += new Vector3(0, 0, _rollSpeed/* * -1*/);
            }
            else
            {
                _tapeOutside.eulerAngles += new Vector3(0, 0, _rollSpeed * -1);
            }
            _morumotto.eulerAngles += new Vector3(_rollSpeed, 0, 0);
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
    /// 拡大SEを再生
    /// </summary>
    private void PlaySoundEffectScaleUp()
    {
        if (_sfxPlayedScaleUp == true)
        {
            _sfxPlay.PlaySFX("se_player_expansion");
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
                PlaySoundEffectScaleUp();
            }
            else
            {
                _scale = 4.0f;
                _sfxPlayedScaleUp = false;
            }
        }
        // 縮小
        else if (CrossPlatformInputManager.GetButton("ScaleDown") == true && CrossPlatformInputManager.GetButton("ScaleUp") == false)
        {
            if (0.99f < _scale)
            {
                _scale -= 0.01f;
                _sfxPlayedScaleUp = true;
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
                PlaySoundEffectScaleUp();
            }
            else
            {
                _scale = 4.0f;
                _sfxPlayedScaleUp = false;
            }
        }
        // 縮小
        else if (m_scroll < 0.0f)
        {
            if (0.99f < _scale + m_scroll)
            {
                _scale += m_scroll;
                _sfxPlayedScaleUp = true;
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

        yield return new WaitForSeconds(1f);

        // 距離を計測
        var pos2 = new Vector2(_transform.position.x, _transform.position.z);
        var distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);
        Debug.Log("計測終了");

        StopCoroutine(PositionCash());
    }
}

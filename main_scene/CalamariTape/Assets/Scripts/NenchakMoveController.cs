using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller.WallHorizontal;
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

    /// <summary>ジャンプ制御値</summary>
    private float _jumpVelocity;
    /// <summary>ジャンプ制御の最大値の一時保存</summary>
    private float _registedJumpMax;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;

    /// <summary>位置フラグを一時保存</summary>
    [SerializeField] private bool _positionCashDebugOff;

    /// <summary>アニメーション</summary>
    [SerializeField] private float _movedSpeedToAnimator;

    /// <summary>重力値の加速度</summary>
    private float _gravityAcceleration;
    /// <summary>重力値の角度</summary>
    [SerializeField] private Vector3 _wallPosition;

    /// <summary>壁走り（縦）</summary>
    [SerializeField] private bool _wallRunVertical = false;
    /// <summary>壁走り（縦）直前のフラグ情報</summary>
    private bool _wallRunVerticalLast = false;
    /// <summary>壁走り（横）</summary>
    [SerializeField] private bool _wallRunHorizontal = false;
    /// <summary>RayCast判定の距離値</summary>
    [SerializeField] private float _maxDistance = 2.3f;
    /// <summary>RayCast判定の距離の処理内で扱う</summary>
    private float _registMaxDistance;
    /// <summary>RayCast判定の距離値（最大）</summary>
    [SerializeField] private float _maxMaxDistance = 8.5f;

    /// <summary>
    /// 横にある壁に対して横方向へ入力すると登るモード<para/>
    /// 1：右方向入力で登り、左方向で下りる<para/>
    /// -1：左方向入力で登り、右方向で下りる
    /// </summary>
    private int _wallRunHorizontalMode = (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT;

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
    /// <summary>スケール拡大SE再生可フラグ</summary>
    private bool _sfxPlayedScaleUp;

    /// <summary>ネンチャク用の壁（縦）</summary>
    private GameObject _clearVtclWall;
    /// <summary>一度でも壁（縦）に衝突したことがある</summary>
    private bool _wallRunedVtcl;
    /// <summary>ネンチャク用の壁（横）</summary>
    private GameObject _clearHztlWall;
    /// <summary>一度でも壁（横）に衝突したことがある</summary>
    private bool _wallRunedHztl;

    /// <summary>プレイヤーの耐久値</summary>
    [SerializeField] private NenchakHealth _health;

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _groundSetMoveSpeed = _moveSpeed;
        _gravityAcceleration = 0f;
        _registMaxDistance = _maxDistance;
        _sfxPlayedScaleUp = true;

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
        if (_wallRunVertical == true)
        {
            _wallRunVerticalLast = _wallRunVertical;
        }
    }

    private void Update()
    {
        if (_wallRunVertical == false && _wallRunHorizontal == false)
        {
            if (_characterController.isGrounded && _jumpAction != true)
            {
                _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        ScaleChangeForController();
        ScaleChangeForMouse();

        _transform.localScale = new Vector3(1, 1, 1) * _scale;
        // 大きさに合わせて速度を計算
        _groundSetMoveSpeed = ParameterMatchScale(_moveSpeed, _maxMoveSpeed);

        // 大きさに合わせてRayの距離を計算
        _registMaxDistance = ParameterMatchScale(_maxDistance, _maxMaxDistance);

        // デバッグ：移動計測のコルーチンを起動する
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PositionCash());
        }

        // アニメーションのループ対策
        if (_wallRunVertical == false && _wallRunHorizontal == false)
        {
            if (_animation.getAnimationLoop("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator) == true)
            {
                _animation.setAnimetionParameters("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 前後にある壁に対して前後方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = true;
            _wallRunVerticalLast = false;
            _wallRunHorizontal = false;
            var r = other.gameObject.transform.position;
            _wallPosition = new Vector3(r.x, r.y, r.z);
            _wallRunedVtcl = true;
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
                _wallRunHorizontalMode = (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT;
            }
            else if (Physics.Raycast(i, Vector3.left, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT;
            }
            _wallRunedHztl = true;
        }

        // 一度でも壁に衝突
        if (_wallRunedVtcl == true)
        {
            _clearVtclWall = NenchakWallVerticalDecision.CheckClear(other.gameObject);
        }
        if (_wallRunedHztl == true)
        {
            _clearHztlWall = NenchakWallHorizontalDecision.CheckClear(other.gameObject);
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
        _scale = 1.0f;

        // 切り替えた際にSEがオフになっていない場合はオフにする
        if (_sfxPlayedDerable == true)
        {
            _sfxPlayedDerable = false;
            StopCoroutine(SleepTimeSoundEffectDerableDecrease());
        }

        // 壁に衝突した情報をリセット
        _clearVtclWall = null;
        _wallRunedVtcl = false;
        _clearHztlWall = null;
        _wallRunedHztl = false;
    }

    /// <summary>
    /// モードチェンジで実行するメソッド
    /// </summary>
    public void OnChange()
    {
        var c = _health.ReadMaterial();
        _health.ReflectMaterial(c.r, c.g, c.b, _health._defaultAlpha);
        _health._blinkingMaterialStart = false;
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");

        if (0 < _health.Parameter && _health.Adhesive == true && _wallRunVertical == true && _wallRunHorizontal == false)
        {
            // 前後方向で登る制御
            _moveVelocity.x = h * _groundSetMoveSpeed;
            _moveVelocity.y = v * _groundSetMoveSpeed;
        }
        else if (0 < _health.Parameter && _health.Adhesive == true && _wallRunHorizontal == true)
        {
            // 横方向で登る制御
            _moveVelocity.y = h * _groundSetMoveSpeed * _wallRunHorizontalMode;
            _moveVelocity.z = v * _groundSetMoveSpeed;
        }
        else if (0 < _health.Parameter && _health.Adhesive == true && _wallRunedVtcl == true && _wallRunedHztl == false)
        {
            // 耐久値がある内は縦方向の入力で登る壁に対して壁の外に出ない挙動にする
            var wall = NenchakWallVerticalDecision.CheckClear(_clearVtclWall);
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
        }
        else if (0 < _health.Parameter && _health.Adhesive == true && _wallRunedHztl == true)
        {
            // 耐久値がある内は横方向の入力で登る壁に対して壁の外に出ない挙動にする
            var wall = NenchakWallHorizontalDecision.CheckClear(_clearHztlWall);
            if (wall != null)
            {
                if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
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
                        _moveVelocity.y = h * _groundSetMoveSpeed * _wallRunHorizontalMode;
                    }
                }
                else if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
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
                        _moveVelocity.y = h * _groundSetMoveSpeed * _wallRunHorizontalMode;
                    }
                }
            }
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
        if (0 < _health.Parameter && _health.Adhesive == true)
        {
            _animation.setAnimetionParameters("Morumotto", "MoveSpeed", _movedSpeedToAnimator);
            _animation.setAnimetionParameters("Scotch_tape_outside", "MoveSpeed", _movedSpeedToAnimator);
        }
        if (0 < _movedSpeedToAnimator)
        {
            PlaySoundEffectMove();
        }
        else
        {
            _sfxPlayedMove = false;
        }

        // テープの耐久ゲージを減らす
        if (0 < _movedSpeedToAnimator && 0 < _health.Parameter && _health.Adhesive == true && (_wallRunVertical == true || _wallRunHorizontal == true))
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
        if (_wallRunVertical == true)
        {
            if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.x))
            {
                if (Mathf.Abs(_moveVelocity.x) < Mathf.Abs(_moveVelocity.y))
                {
                    // 上方向なら縦向き
                    if (0f < _moveVelocity.y)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 0f);
                    }
                    // 下向きなら縦向き
                    else if (_moveVelocity.y < 0f)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, 0f);
                    }
                }
                else
                {
                    // 左向きなら横向き
                    if (0f < _moveVelocity.x)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, -90f);
                    }
                    // 右向きなら横向き
                    else if (_moveVelocity.x < 0f)
                    {
                        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 90f);
                    }
                }
            }
        }
        else if (_wallRunHorizontal == true)
        {
            if (0 < Mathf.Abs(_moveVelocity.y) || 0 < Mathf.Abs(_moveVelocity.z))
            {
                if (Mathf.Abs(_moveVelocity.y) < Mathf.Abs(_moveVelocity.z))
                {
                    if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                    {
                        // 正面なら縦向き
                        if (0f < _moveVelocity.z)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, 90f);
                        }
                        else if (_moveVelocity.z < 0f)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, -90f);
                        }
                    }
                    else if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                    {
                        // 正面なら縦向き
                        if (0f < _moveVelocity.z)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 0f, -90f);
                        }
                        else if (_moveVelocity.z < 0f)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 180f, 90f);
                        }
                    }
                }
                else
                {
                    // 右側に壁があった際の床上移動と壁移動
                    if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT)
                    {
                        // 左向きなら横向き
                        if (0f < _moveVelocity.y)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, 90f, 0f);
                        }
                        // 右向きなら横向き
                        else if (_moveVelocity.y < 0f)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, -90f, 0f);
                        }
                    }
                    // 左側に壁があった際の床上移動と壁移動
                    else if (_wallRunHorizontalMode == (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT)
                    {
                        // 左向きなら横向き
                        if (0f < _moveVelocity.y)
                        {
                            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, -90f, 0f);
                        }
                        // 右向きなら横向き
                        else if (_moveVelocity.y < 0f)
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

        yield return new WaitForSeconds(1f/* * Time.deltaTime*/);

        // 距離を計測
        var pos2 = new Vector2(_transform.position.x, _transform.position.z);
        var distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);
        Debug.Log("計測終了");

        StopCoroutine(PositionCash());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller.Wall;
using Const.Tag;

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

    /// <summary>耐久ゲージ</summary>
    [SerializeField] private DurableValue _value;

    /// <summary>アニメーション</summary>
    [SerializeField] private float _movedSpeedToAnimator;

    /// <summary>重力値の加速度</summary>
    private float _gravityAcceleration;
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

    void Start()
    {
        _transform = this.transform;
        _registedScale = _scale;
        _groundSetMoveSpeed = _moveSpeed;
        _gravityAcceleration = 0f;
        _registMaxDistance = _maxDistance;

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
                _wallRunHorizontalMode = (int)WallRunHorizontalMode.RIGHT_IS_FRONT;
            }
            else if (Physics.Raycast(i, Vector3.left, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int)WallRunHorizontalMode.LEFT_IS_FRONT;
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
    }

    /// <summary>
    /// キャラクターの操作制御
    /// </summary>
    private void CharacterMovement()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");

        // 前後方向で登る制御
        if (0 < _value._parameter && _value._adhesive == true && _wallRunVertical == true && _wallRunHorizontal == false)
        {
            _moveVelocity.x = h * _groundSetMoveSpeed;
            _moveVelocity.y = v * _groundSetMoveSpeed;
        }
        // 横方向で登る制御
        else if (0 < _value._parameter && _value._adhesive == true && _wallRunHorizontal == true)
        {
            _moveVelocity.y = h * _groundSetMoveSpeed * _wallRunHorizontalMode;
            _moveVelocity.z = v * _groundSetMoveSpeed;
        }
        // 壁を登らない
        else
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

    [SerializeField] private Vector3 _vt;
    [SerializeField] private Vector3 _vm;

    /// <summary>
    /// 移動速度に応じて各オブジェクトを回転させる
    /// </summary>
    private void RollObject()
    {
        _tapeOutside.eulerAngles += new Vector3(0, 0, _rollSpeed * -1);
        _morumotto.eulerAngles += new Vector3(_rollSpeed, 0, 0);

        //tapeOutside = new Vector3(tapeOutside.x, tapeOutside.y, tapeOutside.z + (_rollSpeed * -1));
        //_tapeOutside.eulerAngles += new Vector3(0, 0, 0) + _vt;
        //morumotto = new Vector3(morumotto.x + (_rollSpeed), morumotto.y, morumotto.z);
        //_morumotto.eulerAngles = new Vector3(0, 0, 0) + _vm;
    }

    /// <summary>
    /// キャラクターを動かす
    /// </summary>
    private void MoveAndAnimation()
    {
        // 移動方向に向く
        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.y));

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
        _animator.SetFloat("MoveSpeed", _movedSpeedToAnimator);

        if (0 < _movedSpeedToAnimator && 0 < _value._parameter && _value._adhesive == true && _wallRunVertical == true)
        {
            RollObject();

            _value._parameter -= Time.deltaTime;
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

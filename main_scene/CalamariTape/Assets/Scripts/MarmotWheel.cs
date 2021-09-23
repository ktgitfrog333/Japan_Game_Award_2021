using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Animator;
using Controller.Gimmicks;

/// <summary>
/// 回し車を回転させる
/// </summary>
public class MarmotWheel : MonoBehaviour
{
    /// <summary>回転量</summary>
    [SerializeField, Range(0, 3)] public int _spinLevel;
    /// <summary>減速量</summary>
    [SerializeField, Range(0, 3)] public int _frictionLevel;
    /// <summary>反対方向に回転し始めるまでの時間</summary>
    [SerializeField] private float _sleepTime;
    /// <summary>回し車の回転アニメーション</summary>
    [SerializeField] private Animator _animator;
    /// <summary>回転を実行</summary>
    private bool _spinStart;
    /// <summary>回転を反転させるフラグ</summary>
    public bool _mirror { set; get; }
    /// <summary>ギミック実行範囲内検知</summary>
    private bool _tsurutsuruIn;
    /// <summary>プレイヤーのモード管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>プレイヤーを止めるフラグ</summary>
    private bool _onTriggerExit;
    /// <summary>回し車と扉の耐久値を管理</summary>
    [SerializeField] private MarmotHealth _health;
    /// <summary>自動で回転を戻すフラグ</summary>
    private bool _returnRotateAuto;

    private void Start()
    {
        _health._health = 0f;
        CheckConfig();
    }

    private void Update()
    {
        InputAxis();
        SwitchRotatePattern();
        _health.UpdateHealth(_tsurutsuruIn, _spinStart, _mirror, _spinLevel);
        ReturnRotateAuto();
        _health.ReturnUpdateHealth();

        // 自動で反対に回転している際に止める
        if (_returnRotateAuto == true && _health._returnUpdateHealth == true)
        {
            if (_health._health <= 0f)
            {
                _health._health = _health._healthMin;
                _animator.SetBool(AnimatorControllerManager.MAWASHIGURUMA_ROTATEBOOL, false);
                _returnRotateAuto = false;
                _health._returnUpdateHealth = false;
                _onTriggerExit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 自動で反対に回転している際に止める
        if (_returnRotateAuto == true && _health._returnUpdateHealth == true)
        {
            _returnRotateAuto = false;
            _health._returnUpdateHealth = false;
            _onTriggerExit = false;
        }
        var r = GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, GetType().ToString());
        if (r == 1)
        {
            _tsurutsuruIn = true;
        }
        else if (r == 0 && _tsurutsuruIn == true)
        {
            _tsurutsuruIn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var r = GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, GetType().ToString());
        if (r != -1 && _onTriggerExit == false)
        {
            _onTriggerExit = true;
            _tsurutsuruIn = false;
            StartCoroutine(ActiveGimmick());
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_spinLevel == 0)
        {
            Debug.Log("回転量（_spinLevel）の未設定");
        }
        if (_frictionLevel == 0)
        {
            Debug.Log("減速量（_frictionLevel）の未設定");
        }
        if (_sleepTime == 0f)
        {
            Debug.Log("反対方向に回転し始めるまでの時間（_sleepTime）の未設定");
        }
    }

    /// <summary>
    /// ギミックを時間差で開始させる
    /// </summary>
    /// <param name="gameObject">プレイヤー</param>
    /// <returns></returns>
    private IEnumerator ActiveGimmick()
    {
        yield return new WaitForSeconds(_sleepTime);
        _returnRotateAuto = true;
        _health._returnUpdateHealth = true;
    }

    /// <summary>
    /// 操作入力の検知
    /// </summary>
    private void InputAxis()
    {
        if (_tsurutsuruIn == true)
        {
            var h = _playerManager._tsurutsuruController._horizontal;
            if (0.1f <= h)
            {
                _spinStart = true;
                _mirror = false;
            }
            else if (h <= -0.1f)
            {
                _spinStart = true;
                _mirror = true;
            }
            else
            {
                _spinStart = false;
            }
        }
    }

    /// <summary>
    /// 回し車を回転させる
    /// </summary>
    private void SwitchRotatePattern()
    {
        if (_returnRotateAuto == false)
        {
            if (_tsurutsuruIn == true)
            {
                // 回転を開始
                if (_spinStart == true)
                {
                    _animator.SetInteger(AnimatorControllerManager.MAWASHIGURUMA_ROTATELEVEL, _spinLevel);
                    if (_playerManager._tsurutsuru.activeSelf == true)
                    {
                        _playerManager._tsuruTsuruAnimation.PlayFirstSpin(_spinLevel);
                    }
                    _animator.SetBool(AnimatorControllerManager.MAWASHIGURUMA_ROTATEBOOL, true);
                    // 回転方向
                    if (_mirror == false)
                    {
                        _animator.SetFloat(AnimatorControllerManager.MAWASHIGURUMA_ROTATEFLOAT, -1f);
                    }
                    else
                    {
                        _animator.SetFloat(AnimatorControllerManager.MAWASHIGURUMA_ROTATEFLOAT, 1f);
                    }
                }
                else
                {
                    _animator.SetBool(AnimatorControllerManager.MAWASHIGURUMA_ROTATEBOOL, false);
                    if (_playerManager._tsurutsuru.activeSelf == true)
                    {
                        _playerManager._tsuruTsuruAnimation.PlayFirstSpin(0);
                    }
                }
            }
            else
            {
                _animator.SetBool(AnimatorControllerManager.MAWASHIGURUMA_ROTATEBOOL, false);
                if (_playerManager._tsurutsuru.activeSelf == true)
                {
                    _playerManager._tsuruTsuruAnimation.PlayFirstSpin(0);
                }
            }
        }
    }

    /// <summary>
    /// 自動で回転を戻す
    /// </summary>
    private void ReturnRotateAuto()
    {
        if (_returnRotateAuto == true)
        {
            _animator.SetInteger(AnimatorControllerManager.MAWASHIGURUMA_ROTATELEVEL, _frictionLevel);
            _animator.SetBool(AnimatorControllerManager.MAWASHIGURUMA_ROTATEBOOL, true);
            _animator.SetFloat(AnimatorControllerManager.MAWASHIGURUMA_ROTATEFLOAT, 1f);
        }
    }
}

using Controller.Gimmicks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadException;
using UnityStandardAssets.CrossPlatformInput;
using Const.Component;

/// <summary>
/// ターザン
/// </summary>
public class Tarzan : MonoBehaviour
{
    /// <summary>飛んだ後、元の場所に戻る時間</summary>
    [SerializeField] private float _pendulumSleepTime = 10f;
    /// <summary>プレイヤー管理</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>ポーズ画面表示制御スクリプトクラス</summary>
    [SerializeField] private PauseWindowManager _pauseWindowManager;
    /// <summary>ネンチャクモードでロープに掴まっている</summary>
    private bool _nenchakIn;
    /// <summary>ネンチャクモード制御</summary>
    private GameObject _nenchak;
    /// <summary>ロープの位置・角度・大きさ</summary>
    [SerializeField] private Transform _ropeTransform;
    /// <summary>ロープの物理演算</summary>
    [SerializeField] private Rigidbody _rigidbody;
    /// <summary></summary>
    [SerializeField] private HingeJoint _hingeJoint;
    /// <summary>ロープへエネルギーを加えることを有効にするフラグ</summary>
    private bool _impact;
    /// <summary>現在のロープにかかっているエネルギー</summary>
    private float _currentMagnitude;
    /// <summary>ロープにかかっているエネルギーの最大値</summary>
    private float _maxMagnitude;
    /// <summary>インパクト可能な範囲を計算する際の補正値</summary>
    private readonly float ENABLED_RANGE_MAGNITUDE = 0.99f;
    /// <summary>現在の運動量・運動量の最大値を記録するコルーチンの継続判定フラグ</summary>
    private bool _saveStrokeEnergyRuning;
    /// <summary>ロープに振られている方向（左：-1、右：1）</summary>
    private int _ropeDirection = 0;
    /// <summary>ロープの振り子の方向を計算する処理を排他的にする</summary>
    private bool _registRopeDirectionRuning;
    /// <summary>プレイヤーがロープから飛ぶ際にかかるエネルギーを計算して動かす処理を排他的にする</summary>
    private bool _throwsPlayerRuning;
    /// <summary>プレイヤーが離れてから少しずつ振り子の勢いと弱めていく処理を排他的にする</summary>
    private bool _stopPendulumRuning;

    void Update()
    {
        if (_nenchakIn == true)
        {
            var hztl = CrossPlatformInputManager.GetAxis("Horizontal");
            if (_impact == false && AllowInputImpact(hztl) == true)
            {
                _impact = true;
                if (_rigidbody.velocity.magnitude < 3.0f)
                {
                    _rigidbody.AddForce(new Vector3(hztl * 10f, 0f, 0f), ForceMode.Impulse);
                }
                if (_saveStrokeEnergyRuning == true)
                {
                    StopCoroutine(SaveStrokeEnergy());
                    _saveStrokeEnergyRuning = false;
                }
                StartCoroutine(SaveStrokeEnergy());
                if (_registRopeDirectionRuning == true)
                {
                    StopCoroutine(RegistRopeDirection());
                    _registRopeDirectionRuning = false;
                }
                StartCoroutine(RegistRopeDirection());
            }
        }
    }

    /// <summary>
    /// 入力方向と移動方向が同じ方向の時にインパクトを有効にする
    /// </summary>
    /// <param name="horizontal">移動入力（横方向）</param>
    /// <returns></returns>
    private bool AllowInputImpact(float horizontal)
    {
        var result = false;
        if (0.1f <= Mathf.Abs(horizontal))
        {
            if (0 < _ropeDirection && 0f < horizontal)
            {
                result = true;
            }
            else if (_ropeDirection < 0 && horizontal < 0f)
            {
                result = true;
            }
            else if (0 == _ropeDirection)
            {
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// 現在の運動量・運動量の最大値を記録する
    /// </summary>
    /// <returns></returns>
    private IEnumerator SaveStrokeEnergy()
    {
        if (_saveStrokeEnergyRuning == true)
        {
            yield break;
        }
        _saveStrokeEnergyRuning = true;
        yield return new WaitForSeconds(0.5f);
        while (_saveStrokeEnergyRuning == true)
        {
            _currentMagnitude = _ropeTransform.localEulerAngles.magnitude;
            if (_maxMagnitude < _currentMagnitude)
            {
                _maxMagnitude = _currentMagnitude;
            }
            if (_currentMagnitude <= (_maxMagnitude * ENABLED_RANGE_MAGNITUDE))
            {
                _impact = false;
            }
            else
            {
                _impact = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        _saveStrokeEnergyRuning = false;
    }

    /// <summary>
    /// ロープの振り子の方向を計算
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegistRopeDirection()
    {
        if (_registRopeDirectionRuning == true)
        {
            yield break;
        }
        _registRopeDirectionRuning = true;

        var pointA = 0f;
        var pointB = 0f;
        while (_registRopeDirectionRuning == true)
        {
            if (_nenchak != null)
            {
                if (pointA == 0f && pointB == 0f)
                {
                    pointA = _nenchak.transform.position.x;
                }
                else if (pointB == 0f)
                {
                    pointB = _nenchak.transform.position.x;
                }
            }

            // 2点間の距離を調べる
            if (pointA != 0f && pointB != 0f)
            {
                if (pointA < pointB)
                {
                    // 右の方向へ動いている
                    _ropeDirection = 1;
                    pointA = 0f;
                    pointB = 0f;
                }
                else if (pointB < pointA)
                {
                    // 左の方向へ動いている
                    _ropeDirection = -1;
                    pointA = 0f;
                    pointB = 0f;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        _registRopeDirectionRuning = false;
    }

    /// <summary>
    /// ロープを掴む
    /// </summary>
    /// <param name="other">プレイヤー</param>
    public void GrabAndReleaseRopeToPlayer(Collider other)
    {
        var r = GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, GetType().ToString());
        if (r == 1 && _nenchak == false)
        {
            // ロープを掴む
            _nenchakIn = true;
            var g = other.gameObject;
            // Rigidbodyコンポーネントを追加
            if (g.GetComponent<Rigidbody>() == null)
            {
                g.AddComponent<Rigidbody>();
            }
            // ロープとプレイヤーを繋ぐコンポーネントを追加
            if (g.GetComponent<HingeJoint>() == null)
            {
                g.AddComponent<HingeJoint>();
                var j = g.GetComponent<HingeJoint>();
                j.connectedBody = _rigidbody;
                j.axis = Vector3.up;
            }
            // ネンチャクモードの操作ロジックを一時的に無効
            var n = g.GetComponent<NenchakMoveController>();
            if (DeadNullReference.CheckReferencedComponent(g, n.GetType().ToString()) == true)
            {
                n.enabled = false;
            }
            _nenchak = g;
            // ポーズ画面の使用を禁止
            if (_pauseWindowManager.isActiveAndEnabled == true)
            {
                _pauseWindowManager.enabled = false;
            }
        }
        else if (r == 0 && _nenchakIn == true)
        {
            // ロープを離す又は、ロープに掴まらない
            if (_nenchak != null)
            {
                var g = _nenchak;
                // ロープとプレイヤーを繋ぐコンポーネントを削除
                if (g.GetComponent<HingeJoint>() != null)
                {
                    Destroy(g.GetComponent<HingeJoint>());
                }
                // Rigidbodyを削除
                if (g.GetComponent<Rigidbody>() != null)
                {
                    StartCoroutine(ThrowsPlayer());
                    Destroy(g.GetComponent<Rigidbody>());
                }
                // ネンチャクモードの操作ロジックを有効に戻す（モード切り替え後に判定に入る為、例外回避を行わない）
                if (g.GetComponent<NenchakMoveController>() != null)
                {
                    g.GetComponent<NenchakMoveController>().enabled = true;
                }
                _nenchak = null;
            }
            // ポーズ画面の使用を許可
            if (_pauseWindowManager.isActiveAndEnabled == false)
            {
                _pauseWindowManager.enabled = true;
            }
            StartCoroutine(StopPendulum());
            _nenchakIn = false;
        }
    }

    /// <summary>
    /// プレイヤーがロープから飛ぶ際にかかるエネルギーを計算して動かす
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThrowsPlayer()
    {
        if (_throwsPlayerRuning == true)
        {
            yield break;
        }
        _throwsPlayerRuning = true;

        // 飛ばす速度
        var speed = 0.1f;
        // 飛ばす最高速度
        var speedMax = 0.6f;
        // 速度の増加補正値
        var fade = 1.25f;

        while(speed <= speedMax)
        {
            if (DeadNullReference.CheckReferencedComponent(_playerManager._calamari, ComponentManager.CHARACTER_CONTROLLER) == true)
            {
                _playerManager._calamari.GetComponent<CharacterController>().Move(_rigidbody.velocity * speed);
            }
            speed = speed * fade;
            yield return new WaitForSeconds(0.01f);
        }

        _throwsPlayerRuning = false;
    }

    /// <summary>
    /// プレイヤーが離れてから少しずつ振り子の勢いと弱めていく
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopPendulum()
    {
        if (_stopPendulumRuning == true)
        {
            yield break;
        }
        _stopPendulumRuning = true;

        // 振り子の力の最大値情報
        var limits = _hingeJoint.limits;
        // 振り子の力の最大値情報の一時保存
        var limitsWork = limits;
        var cnt = 0;
        while (-1 < cnt && cnt < 11)
        {
            var j = new JointLimits();
            j.max = limitsWork.max * (1f - 0.1f * cnt);
            j.min = limitsWork.min * (1f - 0.1f * cnt);
            j.bounceMinVelocity = 0f;
            _hingeJoint.limits = j;
            cnt++;

            yield return new WaitForSeconds(_pendulumSleepTime / 10f);
        }

        // 振り子の力の最大値情報を戻す
        _hingeJoint.limits = limits;
        _stopPendulumRuning = false;
    }
}

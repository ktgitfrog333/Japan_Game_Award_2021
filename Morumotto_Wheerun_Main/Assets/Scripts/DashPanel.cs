using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Gimmicks;
using DeadException;
using Const.Component;

/// <summary>
/// ダッシュパネル
/// </summary>
public class DashPanel : MonoBehaviour
{
    /// <summary>加速度</summary>
    [SerializeField, Range(2, 4)] private int _dashLevel = 2;
    /// <summary>ダッシュ速度</summary>
    private float _dashSpeed;
    /// <summary>ダッシュ速度（デフォルト）</summary>
    private readonly float DEFAULT_DASH_SPEED = 1f;
    /// <summary>加速オフまでの時間（カラマリモードのみ）</summary>
    [SerializeField] private float _calamariDashOffTime = 3f;
    /// <summary>加速方向の設定</summary>
    [SerializeField] private Vector3 _dashDirection = Vector3.back;
    /// <summary>プレイヤーのモード管理スクリプトクラス</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>プレイヤーの各モードオブジェクト</summary>
    private GameObject _player;
    /// <summary>プレイヤーの各モード管理</summary>
    private string _playerMode;
    /// <summary>ダッシュ中</summary>
    private bool _dash;
    /// <summary>ダッシュ中のカラマリモードを止める処理を排他的にする</summary>
    private bool _stopCalamariDashedRuning;
    /// <summary>ブレーキをかけるように徐々に速度を落とす処理を排他的にする</summary>
    private bool _stopNenchakDashedRuning;

    private void Start()
    {
        CheckConfig();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, _dashDirection * _dashLevel * 5f, Color.green);
        ChangeModeAndAddComponent();
    }

    private void FixedUpdate()
    {
        if (_dash == true && _player != null)
        {
            if (DeadNullReference.CheckReferencedComponent(_player, ComponentManager.CHARACTER_CONTROLLER) == true)
            {
                DashColliderPlayer();
                DownDashSpeed();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_dash == false && GimmicksDecision.DecisionCollisionPlayerMode(other.gameObject, _playerManager, "") == 1)
        {
            _dash = true;
            _player = other.gameObject;
            _playerMode = _player.name;
            if (_player.GetComponent<DashPlayer>() == null)
            {
                _player.AddComponent<DashPlayer>()._dashPanel = this;
                _dashSpeed = GetDashSpeed();
            }
        }
    }

    /// <summary>
    /// モードチェンジに合わせて速度を調整
    /// </summary>
    public void ChangeModeAndAddComponent()
    {
        if (_dash == true)
        {
            var speedChangeEnabled = false;
            if (_playerManager._calamari.activeSelf == true)
            {
                // ツルツルからカラマリへは速度変更を行う
                // ネンチャクからカラマリへは速度変更を行わない
                if (_player != null && _player.name.Equals(_playerManager._tsurutsuru.name))
                {
                    speedChangeEnabled = true;
                }
                _player = _playerManager._calamari;
            }
            else if (_playerManager._nenchak.activeSelf == true)
            {
                // カラマリからネンチャクへは速度変更を行う
                speedChangeEnabled = true;
                _player = _playerManager._nenchak;
            }
            else if (_playerManager._tsurutsuru.activeSelf == true)
            {
                // カラマリからツルツルへは速度変更を行わない
                speedChangeEnabled = false;
                _player = _playerManager._tsurutsuru;
            }
            if (_player.GetComponent<DashPlayer>() == null)
            {
                _player.AddComponent<DashPlayer>()._dashPanel = this;
                if (speedChangeEnabled == true)
                {
                    _dashSpeed = GetDashSpeed();
                }
            }
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_calamariDashOffTime == 0f)
        {
            Debug.Log("加速オフまでの時間（カラマリモードのみ）の未設定");
        }
        if (_dashDirection == Vector3.zero)
        {
            Debug.Log("加速方向の設定の未設定");
        }
    }

    /// <summary>
    /// 加速させる
    /// </summary>
    private void DashColliderPlayer()
    {
        var ctrl = _player.GetComponent<CharacterController>();
        ctrl.Move(_dashDirection * _dashSpeed * _dashLevel * Time.deltaTime);
    }

    /// <summary>
    /// 減速させる
    /// </summary>
    private void DownDashSpeed()
    {
        if (_player.name.Equals(_playerManager._calamari.name))
        {
            StartCoroutine(StopCalamariDashed());
        }
        else if (_player.name.Equals(_playerManager._nenchak.name))
        {
            StartCoroutine(StopNenchakDashed());
        }
    }

    /// <summary>
    /// プレイヤー情報から速度を計算する
    /// </summary>
    /// <returns>ダッシュ速度</returns>
    private float GetDashSpeed()
    {
        var speed = DEFAULT_DASH_SPEED;
        if (_player.name.Equals(_playerManager._calamari.name))
        {
            if (DeadNullReference.CheckReferencedComponent(_player, new CalamariMoveController().GetType().ToString()) == true)
            {
                speed = _player.GetComponent<CalamariMoveController>()._groundSetMoveSpeed;
            }
        }
        else if (_player.name.Equals(_playerManager._tsurutsuru.name))
        {
            if (DeadNullReference.CheckReferencedComponent(_player, new TsuruTsuruMoveController().GetType().ToString()) == true)
            {
                speed = _player.GetComponent<TsuruTsuruMoveController>()._groundSetMoveSpeed;
            }
        }

        return speed;
    }

    /// <summary>
    /// ダッシュ中のカラマリモードを止める
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopCalamariDashed()
    {
        if (_stopCalamariDashedRuning == true)
        {
            yield break;
        }
        _stopCalamariDashedRuning = true;

        yield return new WaitForSeconds(_calamariDashOffTime);
        if (_player != null && _player.GetComponent<DashPlayer>() != null)
        {
            StopAndDestroyDashPlayer(_player.GetComponent<DashPlayer>());
        }

        _stopCalamariDashedRuning = false;
    }

    /// <summary>
    /// ブレーキをかけるように徐々に速度を落とす
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopNenchakDashed()
    {
        if (_stopNenchakDashedRuning == true)
        {
            yield break;
        }
        _stopNenchakDashedRuning = true;

        var cnt = 0;
        while (-1 < cnt && cnt < 11)
        {
            _dashSpeed = _dashSpeed * (1f - 0.1f * cnt);
            cnt++;

            yield return new WaitForSeconds(1.25f);
        }

        _stopNenchakDashedRuning = false;
    }

    /// <summary>
    /// ダッシュ状態のプレイヤーの速度を停止して、DashPlayerコンポーネントを削除
    /// </summary>
    /// <param name="dashPlayer">プレイヤーダッシュ用スクリプト</param>
    public void StopAndDestroyDashPlayer(DashPlayer dashPlayer)
    {
        if (_dash == true)
        {
            _dash = false;
            if (_player != null)
            {
                _player = null;
            }
            Destroy(dashPlayer);
        }
    }
}

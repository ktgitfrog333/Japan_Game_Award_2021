using Controller.Gimmicks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadException;
using Const.Component;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// トランポリン
/// </summary>
public class Trampoline : MonoBehaviour
{
    /// <summary>弾力性（トランポリンに触れた際の跳躍力）</summary>
    [SerializeField,Range(1, 3)] private int _boundsLevel = 1;
    /// <summary>ジャンプさせる速度</summary>
    private readonly float JUMP_SPEED = 45f;
    /// <summary>ハイジャンプ入力時間</summary>
    [SerializeField] private float _hightPowerTime = 0.5f;
    /// <summary>飛ばす方向（デフォルトは真上）</summary>
    [SerializeField] private Vector3 _boundsDirection = Vector3.up;
    /// <summary>プレイヤーのモード管理スクリプトクラス</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>キャラクターコントローラー</summary>
    private CharacterController _characterController;
    /// <summary>ジャンプフラグ</summary>
    private bool _jump;
    /// <summary>ハイジャンプフラグ</summary>
    private bool _hightJump;
    /// <summary>ハイジャンプの速度係数</summary>
    private readonly float HIGHT_JUMP_POWER = 1.2f;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _sfxPlayedJump;
    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] SfxPlay _sfxPlay;
    /// <summary>ダッシュ速度</summary>
    private float _jumpSpeed;
    /// <summary>ブレーキをかけるように徐々に速度を落とす処理を排他的にする</summary>
    private bool _stopSpringPowerRuning;
    /// <summary>ジャンプ入力チェック</summary>
    [SerializeField] private bool _checkJumpInput;
    /// <summary>ジャンプ中の判定フラグ</summary>
    private bool _jumpAction;
    /// <summary>ジャンプ入力判定を戻す</summary>
    private bool _reverseJumpInput;

    private void Start()
    {
        CheckConfig();
    }

    private void OnTriggerEnter(Collider other)
    {
        var g = other.gameObject;
        if (_jump == false && GimmicksDecision.DecisionCollisionPlayerMode(g, _playerManager, "") == 1)
        {
            if (DeadNullReference.CheckReferencedComponent(g, ComponentManager.CHARACTER_CONTROLLER) == true)
            {
                _characterController = g.GetComponent<CharacterController>();
            }
            _jumpSpeed = JUMP_SPEED * GetBounds();
            _jump = true;
            StartCoroutine(CheckJumpInput());
            if (g.name.Equals(_playerManager._calamari.name))
            {
                // カラマリモードにて無重力状態にして重力値をリセット
                _playerManager._calamariScaler._zeroGravity = true;
                _playerManager._calamariController._gravityAcceleration = 0f;
            }
            else if (g.name.Equals(_playerManager._nenchak.name))
            {
                // ネンチャクモードにて無重力状態にして重力値をリセット
                _playerManager._nenchakScaler._zeroGravity = true;
            }
            else if (g.name.Equals(_playerManager._tsurutsuru.name))
            {
                // ツルツルモードにて無重力状態にして重力値をリセット
                _playerManager._tsuruTsuruScaler._zeroGravity = true;
                _playerManager._tsurutsuruController._gravityAcceleration = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_jump == true && _characterController != null)
        {
            if (_hightJump == false && _checkJumpInput == true && _jumpAction == true)
            {
                _hightJump = true;
                _jumpSpeed = JUMP_SPEED * GetBounds() * HIGHT_JUMP_POWER;
            }
            PlaySoundEffectJump();
            _characterController.Move(_boundsDirection * _jumpSpeed * Time.deltaTime);
            StartCoroutine(StopSpringPower());
        }
    }

    private void Update()
    {
        if (_jumpAction == true)
        {
            StartCoroutine(ReverseJumpInput());
        }
        else
        {
            _jumpAction = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_boundsLevel == 0)
        {
            Debug.Log("弾力性（トランポリンに触れた際の跳躍力）の未設定");
        }
        if (_boundsDirection == Vector3.zero)
        {
            Debug.Log("飛ばす方向（デフォルトは真上）の未設定");
        }
        if (_hightPowerTime == 0f)
        {
            Debug.Log("ハイジャンプ入力時間の未設定");
        }
    }

    /// <summary>
    /// 弾力性の設定値に応じて跳躍速度を計算する
    /// </summary>
    /// <returns>跳躍速度</returns>
    private float GetBounds()
    {
        var result = 1f;
        var bounds = _boundsLevel - 1;
        if (0 < bounds)
        {
            result = result + (0.5f * bounds);
        }
        return result;
    }

    /// <summary>
    /// ジャンプ入力チェック
    /// </summary>
    private IEnumerator CheckJumpInput()
    {
        if (_checkJumpInput == true)
        {
            yield break;
        }
        _checkJumpInput = true;
        yield return new WaitForSeconds(_hightPowerTime);
        _checkJumpInput = false;
    }

    /// <summary>
    /// ジャンプ効果音を再生する
    /// </summary>
    private void PlaySoundEffectJump()
    {
        if (_sfxPlayedJump == false)
        {
            _sfxPlayedJump = true;
            if (_hightJump == false)
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
    /// ジャンプ入力判定を戻す
    /// </summary>
    private IEnumerator ReverseJumpInput()
    {
        if (_reverseJumpInput == true)
        {
            yield break;
        }
        _reverseJumpInput = true;
        yield return new WaitForSeconds(0.5f);
        _jumpAction = false;
        _reverseJumpInput = false;
    }

    /// <summary>
    /// ブレーキをかけるように徐々に速度を落とす
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopSpringPower()
    {
        if (_stopSpringPowerRuning == true)
        {
            yield break;
        }
        _stopSpringPowerRuning = true;

        var cnt = 0;
        while (-1 < cnt && cnt < 11)
        {
            _jumpSpeed = _jumpSpeed * (1f - 0.1f * cnt);
            cnt++;

            yield return new WaitForSeconds(0.05f);
        }

        _stopSpringPowerRuning = false;
        // ジャンプ状態とキャラクターコントローラーをリセット
        _jump = false;
        _characterController = null;
        _sfxPlayedJump = false;
        // カラマリモードの無重力状態を解除
        _playerManager._calamariScaler._zeroGravity = false;
        // ネンチャクモードの無重力状態を解除
        _playerManager._nenchakScaler._zeroGravity = false;
        // ツルツルモードの無重力状態を解除
        _playerManager._tsuruTsuruScaler._zeroGravity = false;
        _hightJump = false;
    }
}

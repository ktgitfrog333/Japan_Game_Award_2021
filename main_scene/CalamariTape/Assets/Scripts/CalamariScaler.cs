using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DeadException;
using Const.Component;
using Controller.AllmodeState;
using Controller.Gravity;

/// <summary>
/// カラマリモードの大きさを調整
/// </summary>
public class CalamariScaler : MonoBehaviour
{
    /// <summary>カラマリモードの状態</summary>
    [SerializeField] private CalamariState _state;

    /// <summary>拡大率</summary>
    [SerializeField, Range(1, 4)] private float _scale = 1;
    /// <summary>拡大率</summary>
    public float Scale { get { return _scale; } }

    /// <summary>スケール拡大SE再生可フラグ</summary>
    private bool _sfxPlayedScaleUp;

    /// <summary>カラマリモードにて壁移動を行う</summary>
    [SerializeField] private CalamariWallMove _wallMove;
    /// <summary>プレイヤーの耐久値</summary>
    [SerializeField] private CalamariHealth _health;
    /// <summary>無重力状態</summary>
    public bool _zeroGravity { get; set; } = false;

    private void Start()
    {
        _sfxPlayedScaleUp = true;
    }

    private void Update()
    {
        if (DeadNullReference.CheckReferencedComponent(gameObject, ComponentManager.CALAMARI_STATE) == true)
        {
            _state._transform.localScale = new Vector3(1, 1, 1) * _scale;
        }
        // 耐久ゲージが0になったら落下させる
        if (_health.Parameter <= 0 && _health.Adhesive == false && _zeroGravity == true)
        {
            _zeroGravity = false;
        }
    }

    private void OnEnable()
    {
        _scale = 1.0f;
    }

    /// <summary>
    /// コントーローラーによる拡大・縮小
    /// </summary>
    public void ScaleChangeForController()
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
            GravityAction();
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
            GravityAction();
        }
    }

    /// <summary>
    /// マウスホイールによる拡大・縮小
    /// </summary>
    public void ScaleChangeForMouse()
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
            GravityAction();
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
            GravityAction();
        }
    }

    /// <summary>
    /// 重力による挙動
    /// </summary>
    private void GravityAction()
    {
        // 耐久ゲージがある間のみ壁に張り付く
        if (0 < _health.Parameter && _health.Adhesive == true)
        {
            _zeroGravity = true;
        }
        // 壁の接触判定
        var isWalled = AllmodeStateConf.IsWalled(_state._transform, _wallMove._registMaxDistance);
        if (isWalled != (int)GravityDirection.AIR && 0 < _health.Parameter && _health.Adhesive == true)
        {
            var ctrl = _state._characterController;
            var vector = new Vector3();
            if (isWalled == (int)GravityDirection.VERTICAL)
            {
                // 横向きの壁に対する重力修正
                vector.z = Physics.gravity.y * Time.deltaTime * -1;
            }
            else if (isWalled == (int)GravityDirection.HORIZONTAL_LEFT)
            {
                // 縦向きの壁（左）に対する重力修正
                vector.x = Physics.gravity.y * Time.deltaTime;
            }
            else if (isWalled == (int)GravityDirection.HORIZONTAL_RIGHT)
            {
                // 縦向きの壁（右）に対する重力修正
                vector.x = Physics.gravity.y * Time.deltaTime * -1;
            }
            ctrl.Move(vector);
        }
        else if (AllmodeStateConf.IsGrounded(_state._characterController, _state._transform, _wallMove._registMaxDistance) == false/* || (_health.Parameter <= 0 && _health.Adhesive == false)*/)
        {
            // 空中
            var c = _state._characterController;
            var v = new Vector3();
            v.y = Physics.gravity.y * Time.deltaTime;
            c.Move(v);
        }
    }

    /// <summary>
    /// 拡大SEを再生
    /// </summary>
    private void PlaySoundEffectScaleUp()
    {
        if (_sfxPlayedScaleUp == true)
        {
            _state._sfxPlay.PlaySFX("se_player_expansion");
        }
    }
}

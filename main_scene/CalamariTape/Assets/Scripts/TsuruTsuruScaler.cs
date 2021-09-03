using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DeadException;
using Const.Component;

/// <summary>
/// カラマリモードの大きさを調整
/// </summary>
public class TsuruTsuruScaler : MonoBehaviour
{
    /// <summary>カラマリモードの状態</summary>
    [SerializeField] private TsuruTsuruState _state;

    /// <summary>拡大率</summary>
    [SerializeField, Range(1, 4)] private float _scale = 1;
    /// <summary>拡大率</summary>
    public float Scale {
        get
        {
            return _scale;
        }
    }

    /// <summary>スケール拡大SE再生可フラグ</summary>
    private bool _sfxPlayedScaleUp;

    private void Start()
    {
        _sfxPlayedScaleUp = true;
    }

    private void Update()
    {
        _state._transform.localScale = new Vector3(1, 1, 1) * _scale;
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

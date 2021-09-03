using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ツルツルモードのアニメーション
/// </summary>
public class TsuruTsuruAnimation : MonoBehaviour
{
    /// <summary>ツルツルモードの状態</summary>
    [SerializeField] private TsuruTsuruState _state;

    /// <summary>アニメーションスピード</summary>
    private float _animationSpeed = 0f;

    /// <summary>
    /// アニメーションループチェック
    /// </summary>
    /// <param name="animatorName">アニメータ名</param>
    /// <param name="name">パラメータ名</param>
    /// <param name="value">値</param>
    /// <returns></returns>
    public bool getAnimationLoop(string animatorName, string name, float value)
    {
        var result = false;
        if (animatorName.Equals(_state._animatorTape.name))
        {
            if (0f < Mathf.Abs(_state._animatorTape.speed) && Mathf.Abs(value) == 0f)
            {
                result = true;
            }
        }
        else
        {
            Debug.Log("不明なAnimator指定:[" + animatorName + "]");
        }

        return result;
    }

    /// <summary>
    /// アニメーションをセット
    /// </summary>
    /// <param name="animatorName">アニメータ名</param>
    /// <param name="name">パラメータ名</param>
    /// <param name="value">値</param>
    public void setAnimetionParameters(string animatorName, string name, float value)
    {
        if (animatorName.Equals(_state._animatorTape.name))
        {
            if (0f < Mathf.Abs(_state._animatorTape.GetFloat(name)))
            {
                if (value < 1.5f)
                {
                    _state._animatorTape.speed = 0f;
                }
                else
                {
                    _state._animatorTape.speed = 1f;
                }
            }
            else
            {
                _state._animatorTape.SetFloat(name, value);
            }
        }
        else if (animatorName.Equals(_state._animatorMarmot.name))
        {
            _state._animatorMarmot.SetFloat(name, value);
        }
        else
        {
            Debug.Log("不明なAnimator指定:[" + animatorName + "]");
        }
    }

    /// <summary>
    /// アニメーションを一時停止
    /// </summary>
    /// <param name="animatorName">アニメータ名</param>
    public void PauseAnimation(string animatorName)
    {
        if (animatorName.Equals(_state._animatorTape.name))
        {
            _animationSpeed = _state._animatorTape.speed;
            _state._animatorTape.speed = 0f;
        }
        else
        {
            Debug.Log("不明なAnimator指定:[" + animatorName + "]");
        }
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="animatorName">アニメータ名</param>
    public void PlayAnimation(string animatorName)
    {
        if (animatorName.Equals(_state._animatorTape.name))
        {
            _state._animatorTape.speed = _animationSpeed;
        }
        else
        {
            Debug.Log("不明なAnimator指定:[" + animatorName + "]");
        }
    }

    /// <summary>
    /// プレイヤーの回転スピードを変更
    /// </summary>
    /// <param name="spinLevel">回転レベル</param>
    public void PlayFirstSpin(int spinLevel)
    {
        _state._animatorTape.SetInteger("RotateLevel", spinLevel);
    }
}

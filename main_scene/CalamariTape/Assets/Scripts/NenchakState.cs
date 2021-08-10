using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ネンチャクモードの状態
/// </summary>
public class NenchakState : MonoBehaviour
{
    /// <summary>テープのアニメーター</summary>
    [SerializeField] public Animator _animatorTape;
    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] public Animator _animatorMarmot;
    /// <summary>見た目</summary>
    [SerializeField] public Renderer _renderer;
    /// <summary>耐久ゲージ</summary>
    [SerializeField] public DurableValue _value;
}

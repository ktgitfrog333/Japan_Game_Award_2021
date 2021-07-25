using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カラマリモードの状態
/// </summary>
public class CalamariState : MonoBehaviour
{
    /// <summary>テープのアニメーター</summary>
    [SerializeField] public Animator _animatorTape;
    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] public Animator _animatorMarmot;
}

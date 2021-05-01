using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 耐久ゲージのスクリプトクラス
/// </summary>
public class DurableValue : MonoBehaviour
{
    /// <summary>パラメータの初期値</summary>
    [SerializeField] private int _parameterOffSet = 10;
    /// <summary>パラメータ</summary>
    public float _parameter { get; set; }
    /// <summary>粘着フラグ</summary>
    public bool _adhesive { get; set; } = true;

    private void OnEnable()
    {
        _parameter = _parameterOffSet;
        _adhesive = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転リングにリンクした足場
/// </summary>
public class CircleScaffold : MonoBehaviour
{
    /// <summary>位置・角度・大きさ</summary>
    public Transform _transform { get; set; }

    private void Start()
    {
        _transform = transform;
    }
}

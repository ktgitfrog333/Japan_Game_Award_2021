using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドアの開閉
/// </summary>
public class DoorOpener : MonoBehaviour
{
    /// <summary>開く速度</summary>
    [SerializeField, Range(0, 3)] private int _openSpeed;
    /// <summary>閉じる速度</summary>
    [SerializeField, Range(0, 3)] private int _closeSpeed;
    /// <summary>回し車</summary>
    [SerializeField] private MarmotWheel _wheel;

    /// <summary>ドア開く範囲</summary>
    [SerializeField, Range(90f, 130f)] private float _openRange = 130f;
    /// <summary>右扉の軸</summary>
    [SerializeField] private Transform _rightPoint;
    /// <summary>左扉の軸</summary>
    [SerializeField] private Transform _leftPoint;
    /// <summary>軸の回転運動量</summary>
    private float _shaft;
    /// <summary>耐久値</summary>
    [SerializeField] private MarmotHealth _health;

    private void Start()
    {
        var rTran = _rightPoint.localEulerAngles;
        _shaft = rTran.y;
    }

    private void Update()
    {
        _openSpeed = _wheel._spinLevel;
        _closeSpeed = _wheel._frictionLevel;
        var rTran = _rightPoint.localEulerAngles;
        _shaft = ((_health._health / _health._healthMax) * _openRange) * -1f;
        _rightPoint.localEulerAngles = new Vector3(rTran.x, _shaft, rTran.z);
        _leftPoint.localEulerAngles = new Vector3(rTran.x, -_shaft, rTran.z);
    }
}

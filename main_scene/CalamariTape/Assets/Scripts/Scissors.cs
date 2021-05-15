using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// はさみオブジェクトを扱うスクリプトクラス
/// </summary>
public class Scissors : MonoBehaviour
{
    /// <summary>追跡開始フラグ</summary>
    public bool _tracking { get; set; }
    /// <summary>追跡対象の位置情報</summary>
    public Transform _target { get; set; }
    /// <summary>位置情報をキャッシュ</summary>
    private Transform _transform;
    /// <summary>移動速度</summary>
    [SerializeField] private float _speed = 8f;

    void Start()
    {
        _transform = this.transform;
    }

    void Update()
    {
        if (_tracking == true)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _tracking = true;
    }

    private void OnDisable()
    {
        _tracking = false;
    }
}

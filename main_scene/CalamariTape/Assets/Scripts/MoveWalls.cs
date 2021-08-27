using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.WallVertical;

[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// 動く壁
/// </summary>
public class MoveWalls : MonoBehaviour
{
    /// <summary>移動方向</summary>
    [SerializeField, Range(0, 2)] private int _moveVector = (int)WallRunMoveVerticalMode.X;
    /// <summary>距離</summary>
    [SerializeField] private float _distance;
    /// <summary>移動速度</summary>
    [SerializeField] private float _speed;
    /// <summary>停止時間</summary>
    [SerializeField] private float _sleepTime = 1f;
    /// <summary>位置情報のキャッシュ</summary>
    private Transform _transform;
    /// <summary>開始位置</summary>
    private Vector3 _startPoint;
    /// <summary>終了位置</summary>
    private Vector3 _endPoint;
    /// <summary>正/負</summary>
    private int _symbol;
    /// <summary>摩擦判定</summary>
    private Rigidbody _rigidbody;
    /// <summary>摩擦判定</summary>
    public Vector3 RigidbodyVelocity
    {
        get
        {
            return _rigidbody.velocity;
        }
    }
    /// <summary>開始位置から終了位置へループ移動</summary>
    private IEnumerator _moveLoop;

    private void Awake()
    {
        // 初期設定
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void Start()
    {
        _transform = this.gameObject.transform;
        CheckConfig();
        SetLoopPoint();

        _moveLoop = MoveLoop();
        StartCoroutine(_moveLoop);
    }

    private void Update()
    {
        DrawRayIsRoot();
    }

    /// <summary>
    /// ギミックの停止・開始を外部から制御
    /// </summary>
    /// <param name="flag">停止（false）・再生（true）</param>
    public void SetActive(bool flag)
    {
        if (flag == true)
        {
            StartCoroutine(_moveLoop);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            StopCoroutine(_moveLoop);
        }
    }

    /// <summary>
    /// 設定値のチェック
    /// </summary>
    private void CheckConfig()
    {
        if (_distance == 0f)
        {
            Debug.Log("距離（_distance）の未設定");
        }
        if (_speed == 0f)
        {
            Debug.Log("移動速度（_speed）の未設定");
        }
    }

    /// <summary>
    /// 現在位置を元に開始位置と終了位置をセット
    /// </summary>
    private void SetLoopPoint()
    {
        var p = _transform.position;
        _startPoint = new Vector3(p.x, p.y, p.z);
        if (_moveVector == (int)WallRunMoveVerticalMode.X)
        {
            _endPoint = new Vector3(p.x + _distance, p.y, p.z);
        }
        else if (_moveVector == (int)WallRunMoveVerticalMode.Y)
        {
            _endPoint = new Vector3(p.x, p.y + _distance, p.z);
        }
        else if (_moveVector == (int)WallRunMoveVerticalMode.Z)
        {
            _endPoint = new Vector3(p.x, p.y, p.z + _distance);
        }

        // 移動方向の正負を設定
        if (0f < _distance)
        {
            _symbol = 1;
        }
        else
        {
            _symbol = -1;
        }
    }

    /// <summary>
    /// 開始位置から終了位置へループ移動
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return null;

            if (_moveVector == (int)WallRunMoveVerticalMode.X)
            {
                _rigidbody.velocity = new Vector3(1f, 0f, 0f) * _speed * _symbol;
            }
            else if (_moveVector == (int)WallRunMoveVerticalMode.Y)
            {
                _rigidbody.velocity = new Vector3(0f, 1f, 0f) * _speed * _symbol;
            }
            else if (_moveVector == (int)WallRunMoveVerticalMode.Z)
            {
                _rigidbody.velocity = new Vector3(0f, 0f, 1f) * _speed * _symbol;
            }

            var d = CalcDistance(_transform.position, _endPoint, _moveVector);
            if (d <= 0f)
            {
                TradeLoopPoint();
                _rigidbody.velocity = Vector3.zero;
                yield return new WaitForSeconds(_sleepTime);
            }
        }
    }

    /// <summary>
    /// 開始位置と終了位置の距離を計算
    /// </summary>
    /// <returns>2点間の距離</returns>
    private float CalcDistance(Vector3 startPosition, Vector3 endPosition, int moveVector)
    {
        var distance = 0f;
        var pointA = 0f;
        var pointB = 0f;
        if (moveVector == (int)WallRunMoveVerticalMode.X)
        {
            pointA = startPosition.x;
            pointB = endPosition.x;
        }
        else if (moveVector == (int)WallRunMoveVerticalMode.Y)
        {
            pointA = startPosition.y;
            pointB = endPosition.y;
        }
        else if (moveVector == (int)WallRunMoveVerticalMode.Z)
        {
            pointA = startPosition.z;
            pointB = endPosition.z;
        }
        if (0f < Mathf.Abs(pointA) || 0f < Mathf.Abs(pointB))
        {
            distance = Vector2.Distance(new Vector2(pointA, 0f), new Vector2(pointB, 0f));
            if (Mathf.Floor(distance) <= 0)
            {
                distance = 0f;
            }
        }
        return distance;
    }

    /// <summary>
    /// 開始位置と終了位置を交換する
    /// </summary>
    private void TradeLoopPoint()
    {
        var w = _startPoint;
        _startPoint = _endPoint;
        _endPoint = w;
        if (_symbol == 1)
        {
            _symbol = -1;
        }
        else
        {
            _symbol = 1;
        }
    }

    /// <summary>
    /// 移動先のルートをデバッグ表示する
    /// </summary>
    private void DrawRayIsRoot()
    {
        if (_moveVector == (int)WallRunMoveVerticalMode.X)
        {
            Debug.DrawRay(_startPoint, Vector3.right * Mathf.Abs(_distance) * _symbol, Color.green);
        }
        else if (_moveVector == (int)WallRunMoveVerticalMode.Y)
        {
            Debug.DrawRay(_startPoint, Vector3.up * Mathf.Abs(_distance) * _symbol, Color.green);
        }
        else if (_moveVector == (int)WallRunMoveVerticalMode.Z)
        {
            Debug.DrawRay(_startPoint, Vector3.forward * Mathf.Abs(_distance) * _symbol, Color.green);
        }
    }
}

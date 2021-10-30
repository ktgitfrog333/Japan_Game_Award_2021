using Controller.Gimmicks;
using Controller.WallVertical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadException;

/// <summary>
/// モード強制解除
/// </summary>
public class TippedSaw : MonoBehaviour
{
    /// <summary>移動方向</summary>
    [SerializeField, Range(0, 2)] private int _moveVector = (int)WallRunMoveVerticalMode.X;
    /// <summary>距離</summary>
    [SerializeField] private float _distance = 10f;
    /// <summary>移動速度</summary>
    [SerializeField] private float _speed = 0.1f;
    /// <summary>プレイヤーのモード管理スクリプトクラス</summary>
    [SerializeField] private PlayerManager _playerManager;
    /// <summary>位置・回転・大きさ</summary>
    private Transform _transform;
    /// <summary>開始位置</summary>
    private Vector3 _startPoint;
    /// <summary>終了位置</summary>
    private Vector3 _endPoint;
    /// <summary>正/負</summary>
    private int _symbol;
    /// <summary>開始位置から終了位置へループ移動</summary>
    private IEnumerator _moveLoop;
    /// <summary>回転</summary>
    private IEnumerator _spin;

    void Start()
    {
        _transform = transform;
        CheckConfig();
        SetLoopPoint();

        _moveLoop = MoveLoop();
        StartCoroutine(_moveLoop);

        _spin = Spin();
        StartCoroutine(_spin);
    }

    void Update()
    {
        DrawRayIsRoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        var g = other.gameObject;
        if (GimmicksDecision.DecisionCollisionPlayerMode(g, _playerManager, "") == 1)
        {
            if (g.name.Equals(_playerManager._calamari.name) && DeadNullReference.CheckReferencedComponent(g, new CalamariHealth().GetType().ToString()) == true)
            {
                // カラマリモードの耐久値をゼロにする
                var h = g.GetComponent<CalamariHealth>();
                h.Parameter = 0f;
                h.Adhesive = false;
            }
            else if (g.name.Equals(_playerManager._nenchak.name) && DeadNullReference.CheckReferencedComponent(g, new NenchakHealth().GetType().ToString()) == true)
            {
                // ネンチャクモードの耐久値をゼロにする
                var h = g.GetComponent<NenchakHealth>();
                h.Parameter = 0f;
                h.Adhesive = false;
            }
        }
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
            StartCoroutine(_spin);
        }
        else
        {
            StopCoroutine(_moveLoop);
            StopCoroutine(_spin);
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
            _transform.position = Vector3.MoveTowards(_transform.position, _endPoint, _speed);

            var d = CalcDistance(_transform.position, _endPoint, _moveVector);
            if (d <= 0f)
            {
                TradeLoopPoint();
            }
            yield return new WaitForSeconds(0.01f);
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


    /// <summary>
    /// 回転
    /// </summary>
    private IEnumerator Spin()
    {
        while (true)
        {
            var e = _transform.eulerAngles;
            _transform.eulerAngles = new Vector3(e.x, e.y, e.z + 1f);
            yield return new WaitForSeconds(0.005f);
        }
    }
}

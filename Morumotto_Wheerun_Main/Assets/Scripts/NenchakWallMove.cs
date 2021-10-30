using System.Collections;
using UnityEngine;
using Const.Tag;
using Const.Layer;
using Controller.AllmodeState;
using Controller.WallHorizontal;
using DeadException;
using Controller.WallVertical;
using Const.Component;

/// <summary>
/// ネンチャクモードにて壁移動を行う
/// </summary>
public class NenchakWallMove : MonoBehaviour
{
    /// <summary>ネンチャクモードの状態</summary>
    [SerializeField] private NenchakState _state;

    /// <summary>壁走り（縦）</summary>
    public bool _wallRunVertical { private set; get; } = false;
    /// <summary>壁走り（縦）直前のフラグ情報</summary>
    public bool _wallRunVerticalLast { set; get; } = false;
    /// <summary>壁走り（横）</summary>
    public bool _wallRunHorizontal { private set; get; } = false;
    /// <summary>RayCast判定の距離の処理内で扱う</summary>
    public float _registMaxDistance { private set; get; }
    /// <summary>
    /// 横にある壁に対して横方向へ入力すると登るモード<para/>
    /// 1：右方向入力で登り、左方向で下りる<para/>
    /// -1：左方向入力で登り、右方向で下りる
    /// </summary>
    public int _wallRunHorizontalMode { set; get; } = (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT;

    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private NenchakScaler _scaler;

    /// <summary>摩擦判定</summary>
    public Vector3 _rigidbodyVelocity { set; get; }

    /// <summary>一度でも壁（縦）に衝突したことがある</summary>
    public bool _wallRunedVtcl { private set; get; }
    /// <summary>一度でも壁（横）に衝突したことがある</summary>
    public bool _wallRunedHztl { private set; get; }
    /// <summary>ネンチャク用の壁（縦）</summary>
    public GameObject _clearVtclWall { private set; get; }
    /// <summary>ネンチャク用の壁（横）</summary>
    public GameObject _clearHztlWall { private set; get; }

    private void Start()
    {
        _registMaxDistance = _state._maxDistance;
    }

    private void Update()
    {
        // 大きさに合わせてRayの距離を計算
        _registMaxDistance = AllmodeStateConf.ParameterMatchScale(_state._maxDistance, _state._maxMaxDistance, _scaler.Scale);
    }

    private void OnTriggerStay(Collider other)
    {
        // 前後にある壁に対して前後方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = true;
            _wallRunVerticalLast = false;
            _wallRunHorizontal = false;
            _wallRunedVtcl = true;

            // 動く壁の情報を取得
            var parentObject = other.gameObject.transform.parent.gameObject;
            if (DeadNullReference.CheckReferencedComponent(parentObject, ComponentManager.MOVE_WALLS) == true)
            {
                var p = parentObject.GetComponent<MoveWalls>();
                _rigidbodyVelocity = p.RigidbodyVelocity;
            }
        }

        // 横にある壁に対して横方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.HORIZONTAL_WALL))
        {
            _wallRunHorizontal = true;
            _wallRunVertical = false;

            var i = _state._transform.position;
            Debug.DrawRay(i, Vector3.right * _registMaxDistance, Color.green);
            Debug.DrawRay(i, Vector3.left * _registMaxDistance, Color.green);
            if (Physics.Raycast(i, Vector3.right, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int)WallRunHorizontalFrontMode.RIGHT_IS_FRONT;
            }
            else if (Physics.Raycast(i, Vector3.left, _registMaxDistance) == true)
            {
                _wallRunHorizontalMode = (int)WallRunHorizontalFrontMode.LEFT_IS_FRONT;
            }
            _wallRunedHztl = true;

            // 動く壁の情報を取得
            var parentObject = other.gameObject.transform.parent.gameObject;
            if (DeadNullReference.CheckReferencedComponent(parentObject, ComponentManager.MOVE_WALLS) == true)
            {
                var p = parentObject.GetComponent<MoveWalls>();
                _rigidbodyVelocity = p.RigidbodyVelocity;
            }
        }

        // 一度でも壁に衝突
        if (_wallRunedVtcl == true)
        {
            _clearVtclWall = NenchakWallVerticalDecision.CheckClear(other.gameObject);
        }
        if (_wallRunedHztl == true)
        {
            _clearHztlWall = NenchakWallHorizontalDecision.CheckClear(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 前後方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = false;
            //_rigidbodyVelocity = Vector3.zero;
        }

        // 横方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.HORIZONTAL_WALL))
        {
            _wallRunHorizontal = false;
        }
    }

    private void OnEnable()
    {
        _wallRunVertical = false;
        _wallRunHorizontal = false;

        // 壁に衝突した情報をリセット
        _clearVtclWall = null;
        _wallRunedVtcl = false;
        _clearHztlWall = null;
        _wallRunedHztl = false;
        //_rigidbodyVelocity = Vector3.zero;
    }
}

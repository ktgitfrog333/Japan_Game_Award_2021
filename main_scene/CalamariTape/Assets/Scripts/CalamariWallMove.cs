using System.Collections;
using UnityEngine;
using Const.Tag;
using Const.Layer;
using Controller.AllmodeState;
using Controller.WallHorizontal;
using DeadException;
using Const.Component;

/// <summary>
/// カラマリモードにて壁移動を行う
/// </summary>
public class CalamariWallMove : MonoBehaviour
{
    /// <summary>カラマリモードの状態</summary>
    [SerializeField] private CalamariState _state;

    /// <summary>壁走り（縦）</summary>
    public bool _wallRunVertical { private set; get; } = false;
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

    /// <summary>無重力状態フラグ</summary>
    public bool _zeroGravity { set; get; }
    /// <summary>重力有効状態フラグ</summary>
    public bool _enableGravity { set; get; }

    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private CalamariScaler _scaler;

    /// <summary>摩擦判定</summary>
    public Vector3 _rigidbodyVelocity { set; get; }

    private void Start()
    {
        _registMaxDistance = _state._maxDistance;
    }

    private void Update()
    {
        // 大きさに合わせてRayの距離を計算
        _registMaxDistance = AllmodeStateConf.ParameterMatchScale(_state._maxDistance, _state._maxMaxDistance, _scaler.Scale);

        // 壁の接着判定
        if (IsWallGrounded() == true)
        {
            StartCoroutine(EnableGravity());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 前後にある壁に対して前後方向へ入力すると登る
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = true;
            _wallRunHorizontal = false;

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

            // 動く壁の情報を取得
            var parentObject = other.gameObject.transform.parent.gameObject;
            if (DeadNullReference.CheckReferencedComponent(parentObject, ComponentManager.MOVE_WALLS) == true)
            {
                var p = parentObject.GetComponent<MoveWalls>();
                _rigidbodyVelocity = p.RigidbodyVelocity;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 前後方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.VERTICAL_WALL))
        {
            _wallRunVertical = false;
            _rigidbodyVelocity = Vector3.zero;
        }

        // 横方向で登る挙動を不可にする
        if (other.gameObject.tag.Equals(TagManager.HORIZONTAL_WALL))
        {
            _wallRunHorizontal = false;
            StartCoroutine(ZeroGravity());
            _rigidbodyVelocity = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        var i = transform.position;
        Debug.DrawRay(i, Vector3.right * _registMaxDistance, Color.green);
        Debug.DrawRay(i, Vector3.left * _registMaxDistance, Color.green);
        // 左右に壁が無くかつ壁昇りモードが残っていた場合はフラグをリセット
        if (Physics.Raycast(i, Vector3.right, _registMaxDistance) == false && Physics.Raycast(i, Vector3.left, _registMaxDistance) == false && _wallRunHorizontal == true)
        {
            _wallRunHorizontal = false;
        }
        // 前後に壁が無くかつ壁昇りモードが残っていた場合はフラグをリセット
        if (Physics.Raycast(i, Vector3.forward, _registMaxDistance) == false && Physics.Raycast(i, Vector3.back, _registMaxDistance) == false && _wallRunVertical == true)
        {
            _wallRunVertical = false;
        }
        _rigidbodyVelocity = Vector3.zero;
    }

    /// <summary>
    /// 一定時間重力有効フラグを有効にする
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableGravity()
    {
        _enableGravity = true;
        yield return new WaitForSeconds(0.5f);
        _enableGravity = false;
        StopCoroutine(EnableGravity());
    }

    /// <summary>
    /// 一定時間無重力フラグを有効にする
    /// </summary>
    /// <returns></returns>
    private IEnumerator ZeroGravity()
    {
        _zeroGravity = true;
        yield return new WaitForSeconds(0.5f);
        _zeroGravity = false;
        StopCoroutine(ZeroGravity());
    }

    /// <summary>
    /// 壁の接地判定
    /// </summary>
    /// <returns>接地状態か否か</returns>
    private bool IsWallGrounded()
    {
        var result = false;

        if (result == false)
        {
            Debug.DrawRay(_state._transform.position + Vector3.up * 0.1f, Vector3.down * _registMaxDistance, Color.green);
            var ray = new Ray(_state._transform.position + Vector3.up * 0.1f, Vector3.down);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _registMaxDistance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.WALL)
                {
                    result = true;
                }
            }
        }

        return result;
    }
}

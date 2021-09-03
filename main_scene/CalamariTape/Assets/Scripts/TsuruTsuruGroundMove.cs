using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.AllmodeState;

/// <summary>
/// 地面や壁を移動
/// </summary>
public class TsuruTsuruGroundMove : MonoBehaviour
{
    /// <summary>ツルツルモードの状態</summary>
    [SerializeField] private TsuruTsuruState _state;

    /// <summary>RayCast判定の距離の処理内で扱う</summary>
    public float _registMaxDistance { private set; get; }
    /// <summary>プレイヤーの大きさ</summary>
    [SerializeField] private TsuruTsuruScaler _scaler;

    void Start()
    {
        _registMaxDistance = _state._maxDistance;
    }

    void Update()
    {
        // 大きさに合わせてRayの距離を計算
        _registMaxDistance = AllmodeStateConf.ParameterMatchScale(_state._maxDistance, _state._maxMaxDistance, _scaler.Scale);
    }
}

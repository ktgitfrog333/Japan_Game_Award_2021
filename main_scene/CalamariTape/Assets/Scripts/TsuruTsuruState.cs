using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ツルツルモードの状態
/// </summary>
public class TsuruTsuruState : MonoBehaviour
{
    /// <summary>テープのアニメーター</summary>
    [SerializeField] public Animator _animatorTape;
    /// <summary>モルモットのアニメーター</summary>
    [SerializeField] public Animator _animatorMarmot;

    /// <summary>RayCast判定の距離値</summary>
    [SerializeField] public float _maxDistance = 2.3f;
    /// <summary>RayCast判定の距離値（最大）</summary>
    [SerializeField] public float _maxMaxDistance = 8.5f;
    /// <summary>移動前の位置・回転を保存</summary>
    public Transform _transform { set; get; }
    /// <summary>SE再生用のゲームオブジェクト</summary>
    [SerializeField] public SfxPlay _sfxPlay;

    private void Start()
    {
        _transform = this.transform;
    }
}

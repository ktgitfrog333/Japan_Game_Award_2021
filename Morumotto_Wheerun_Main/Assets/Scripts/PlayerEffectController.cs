using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーエフェクトスクリプトクラス
/// </summary>
public class PlayerEffectController : MonoBehaviour
{
    /// <summary>衝突した際のエフェクト</summary>
    [SerializeField] private GameObject _collisionEffect;
    /// <summary>パーティクルシステム</summary>
    [SerializeField] private ParticleSystem _particleSystem;
    /// <summary>効果発生させるかのフラグ</summary>
    public bool _effectActive { set; get; } = false;

    /// <summary>
    /// 衝突エフェクトを発生
    /// </summary>
    /// <param name="other"></param>
    public void AppearEffectCollision(Collider other)
    {
        if (_effectActive == false)
        {
            _effectActive = true;

            var hitPos = other.ClosestPointOnBounds(this.transform.position);
            _collisionEffect.SetActive(true);
            _collisionEffect.transform.position = hitPos;
            _particleSystem.Play();
        }
    }
}

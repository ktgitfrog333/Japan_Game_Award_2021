using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ロープ
/// </summary>
public class Rope : MonoBehaviour
{
    /// <summary>ターザン</summary>
    [SerializeField] private Tarzan _tarzan;

    private void OnTriggerEnter(Collider other)
    {
        _tarzan.GrabAndReleaseRopeToPlayer(other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのモード管理スクリプトクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>カラマリモードオブジェクト</summary>
    [SerializeField] public GameObject _calamari;
    /// <summary>ネンチャクモードオブジェクト</summary>
    [SerializeField] public GameObject _nenchak;
    /// <summary>ツルツルモードオブジェクト</summary>
    [SerializeField] public GameObject _tsurutsuru;
}

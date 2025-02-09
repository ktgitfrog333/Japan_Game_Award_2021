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

    /// <summary>カラマリモードの操作スクリプト</summary>
    [SerializeField] public CalamariMoveController _calamariController;
    /// <summary>ネンチャクモードの操作スクリプト</summary>
    [SerializeField] public NenchakMoveController _nenchakController;
    /// <summary>ツルツルモードの操作スクリプト</summary>
    [SerializeField] public TsuruTsuruMoveController _tsurutsuruController;
}

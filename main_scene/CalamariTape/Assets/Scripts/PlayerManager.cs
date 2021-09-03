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
    /// <summary>カラマリモードのアニメーションスクリプト</summary>
    [SerializeField] public CalamariAnimation _calamariAnimation;
    /// <summary>カラマリモードの大きさ変更スクリプト</summary>
    [SerializeField] public CalamariScaler _calamariScaler;
    /// <summary>ネンチャクモードの操作スクリプト</summary>
    [SerializeField] public NenchakMoveController _nenchakController;
    /// <summary>ネンチャクモードのアニメーションスクリプト</summary>
    [SerializeField] public NenchakAnimation _nenchakAnimation;
    /// <summary>ネンチャクモードの大きさ変更スクリプト</summary>
    [SerializeField] public NenchakScaler _nenchakScaler;
    /// <summary>ツルツルモードの操作スクリプト</summary>
    [SerializeField] public TsuruTsuruMoveController _tsurutsuruController;
    /// <summary>ツルツルモードのアニメーションスクリプト</summary>
    [SerializeField] public TsuruTsuruAnimation _tsuruTsuruAnimation;
    /// <summary>ツルツルモードの大きさ変更スクリプト</summary>
    [SerializeField] public TsuruTsuruScaler _tsuruTsuruScaler;
}

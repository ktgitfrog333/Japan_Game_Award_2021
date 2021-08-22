using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Tag;
using DeadException;

/// <summary>
/// ギミックを指定する
/// </summary>
public class StopGimmick : MonoBehaviour
{
    /// <summary>動く壁の処理</summary>
    private MoveVerticalWall[] _moveWalls;

    private void Start()
    {
        SearchMoveWalls();
    }

    /// <summary>
    /// レベルデザイン内にある横向きの動く壁の制御スクリプトをリスト化
    /// </summary>
    private void SearchMoveWalls()
    {
        var ary = GameObject.FindGameObjectsWithTag(TagManager.VERTICAL_WALL);
        var workLst = new List<MoveVerticalWall>();
        foreach (var obj in ary)
        {
            if (DeadNullReference.CheckReferencedComponent(obj, "MoveVerticalWall") == true)
            {
                workLst.Add(obj.GetComponent<MoveVerticalWall>());
            }
        }
        if (0 < workLst.Count)
        {
            _moveWalls = workLst.ToArray();
        }
    }

    /// <summary>
    /// 全てのギミックを停止
    /// </summary>
    public void StopAllGimmik()
    {
        if (_moveWalls != null && 0 < _moveWalls.Length)
        {
            foreach (var script in _moveWalls)
            {
                script.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 全てのギミックを再開
    /// </summary>
    public void StartAllGimmik()
    {
        if (_moveWalls != null && 0 < _moveWalls.Length)
        {
            foreach (var script in _moveWalls)
            {
                script.SetActive(true);
            }
        }
    }
}

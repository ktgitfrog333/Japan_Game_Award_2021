using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Tag;
using Const.Component;
using DeadException;

/// <summary>
/// ギミックを指定する
/// </summary>
public class StopGimmick : MonoBehaviour
{
    /// <summary>動く壁の処理</summary>
    private MoveWalls[] _moveWalls;

    private void Start()
    {
        SearchMoveWalls();
    }

    /// <summary>
    /// レベルデザイン内にある横向きの動く壁の制御スクリプトをリスト化
    /// </summary>
    private void SearchMoveWalls()
    {
        var workLst = getScriptList(TagManager.VERTICAL_WALL);
        workLst.AddRange(getScriptList(TagManager.HORIZONTAL_WALL));

        if (0 < workLst.Count)
        {
            _moveWalls = workLst.ToArray();
        }
    }

    /// <summary>
    /// 動くギミックをタグ名から取得
    /// </summary>
    /// <param name="tagName">タグ名</param>
    /// <returns>動くギミックオブジェクト</returns>
    private List<MoveWalls> getScriptList(string tagName)
    {
        var ary = GameObject.FindGameObjectsWithTag(tagName);
        var workLst = new List<MoveWalls>();
        foreach (var obj in ary)
        {
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.MOVE_WALLS) == true)
            {
                workLst.Add(obj.GetComponent<MoveWalls>());
            }
        }

        return workLst;
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

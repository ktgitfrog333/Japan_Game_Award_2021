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
    /// <summary>回し車と扉、制御値の処理</summary>
    private MarmotHealth[] _marmotHealths;
    /// <summary>ベルトコンベアの処理</summary>
    private ConveyorMoveCharacter[] _conveyors;

    private void Start()
    {
        SearchMoveWalls();
        SearchMarmotHealths();
        SearchConveyors();
    }

    /// <summary>
    /// レベルデザイン内にある横向きの動く壁の制御スクリプトをリスト化
    /// </summary>
    private void SearchMoveWalls()
    {
        var workLst = getScriptMoveWallsList(TagManager.VERTICAL_WALL);
        workLst.AddRange(getScriptMoveWallsList(TagManager.HORIZONTAL_WALL));

        if (0 < workLst.Count)
        {
            _moveWalls = workLst.ToArray();
        }
    }

    /// <summary>
    /// レベルデザイン内にある回し車の制御スクリプトをリスト化
    /// </summary>
    private void SearchMarmotHealths()
    {
        var workLst = getScriptMarmotHealthsList(TagManager.MARMOT_HEALTH);

        if (0 < workLst.Count)
        {
            _marmotHealths = workLst.ToArray();
        }
    }

    /// <summary>
    /// レベルデザイン内にあるベルトコンベアの制御スクリプトをリスト化
    /// </summary>
    private void SearchConveyors()
    {
        var workLst = getScriptConveyorsList(TagManager.CONVEYOR);

        if (0 < workLst.Count)
        {
            _conveyors = workLst.ToArray();
        }
    }

    /// <summary>
    /// 動くギミックをタグ名から取得
    /// </summary>
    /// <param name="tagName">タグ名</param>
    /// <returns>動くギミックオブジェクト</returns>
    private List<MoveWalls> getScriptMoveWallsList(string tagName)
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
    /// 動くギミックをタグ名から取得
    /// </summary>
    /// <param name="tagName">タグ名</param>
    /// <returns>動くギミックオブジェクト</returns>
    private List<MarmotHealth> getScriptMarmotHealthsList(string tagName)
    {
        var ary = GameObject.FindGameObjectsWithTag(tagName);
        var workLst = new List<MarmotHealth>();
        foreach (var obj in ary)
        {
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.MARMOT_HEALTH) == true)
            {
                workLst.Add(obj.GetComponent<MarmotHealth>());
            }
        }

        return workLst;
    }

    /// <summary>
    /// 動くギミックをタグ名から取得
    /// </summary>
    /// <param name="tagName">タグ名</param>
    /// <returns>動くギミックオブジェクト</returns>
    private List<ConveyorMoveCharacter> getScriptConveyorsList(string tagName)
    {
        var ary = GameObject.FindGameObjectsWithTag(tagName);
        var workLst = new List<ConveyorMoveCharacter>();
        foreach (var obj in ary)
        {
            if (DeadNullReference.CheckReferencedComponent(obj, ComponentManager.CONVEYOR_MOVE_CHARACTER) == true)
            {
                workLst.Add(obj.GetComponent<ConveyorMoveCharacter>());
            }
        }

        return workLst;
    }

    /// <summary>
    /// 全てのギミックを停止
    /// </summary>
    public void StopAllGimmik()
    {
        // 動く壁
        if (_moveWalls != null && 0 < _moveWalls.Length)
        {
            foreach (var script in _moveWalls)
            {
                script.SetActive(false);
            }
        }
        // 回し車
        if (_marmotHealths != null && 0 < _marmotHealths.Length)
        {
            foreach (var script in _marmotHealths)
            {
                script.SetActiveLinkGimmick(false);
            }
        }
        // ベルトコンベア
        if (_conveyors != null && 0 < _conveyors.Length)
        {
            foreach (var script in _conveyors)
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
        // 動く壁
        if (_moveWalls != null && 0 < _moveWalls.Length)
        {
            foreach (var script in _moveWalls)
            {
                script.SetActive(true);
            }
        }
        // 回し車
        if (_marmotHealths != null && 0 < _marmotHealths.Length)
        {
            foreach (var script in _marmotHealths)
            {
                script.SetActiveLinkGimmick(true);
            }
        }
        // ベルトコンベア
        if (_conveyors != null && 0 < _conveyors.Length)
        {
            foreach (var script in _conveyors)
            {
                script.SetActive(true);
            }
        }
    }
}

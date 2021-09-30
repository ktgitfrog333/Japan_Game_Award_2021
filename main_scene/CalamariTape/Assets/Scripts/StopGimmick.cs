using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelDesign.ActiveManager;

/// <summary>
/// ギミックを指定する
/// </summary>
public class StopGimmick : MonoBehaviour
{
    /// <summary>動く壁の処理</summary>
    private MoveWallsActiveManager _activeMoveWalls;
    /// <summary>回し車と扉、制御値の処理</summary>
    private MarmotHealthActiveManager _activeMarmotHealths;
    /// <summary>ベルトコンベアの処理</summary>
    private ConveyorMoveCharacterActiveManager _activeConveyorMoveCharacters;
    /// <summary>ダッシュパネルの処理</summary>
    private DashPanelActiveManager _activeDashPanel;

    private void Start()
    {
        // 動くギミック処理情報をオブジェクト化して管理
        _activeMoveWalls = new MoveWallsActiveManager();
        _activeMarmotHealths = new MarmotHealthActiveManager();
        _activeConveyorMoveCharacters = new ConveyorMoveCharacterActiveManager();
        _activeDashPanel = new DashPanelActiveManager();
    }

    /// <summary>
    /// 全てのギミックを停止
    /// </summary>
    public void StopAllGimmik()
    {
        // 動く壁
        _activeMoveWalls.StopGimmik();
        // 回し車
        _activeMarmotHealths.StopGimmik();
        // ベルトコンベア
        _activeConveyorMoveCharacters.StopGimmik();
        // ダッシュパネル
        _activeDashPanel.StopGimmik();
    }

    /// <summary>
    /// 全てのギミックを再開
    /// </summary>
    public void StartAllGimmik()
    {
        // 動く壁
        _activeMoveWalls.StartGimmik();
        // 回し車
        _activeMarmotHealths.StartGimmik();
        // ベルトコンベア
        _activeConveyorMoveCharacters.StartGimmik();
        // ダッシュパネル
        _activeDashPanel.StartGimmik();
    }
}

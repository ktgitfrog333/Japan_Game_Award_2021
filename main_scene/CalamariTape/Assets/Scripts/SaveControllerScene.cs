using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene.StageIndex;
using Scene.StageName;

/// <summary>
/// セーブデータ操作を実施するスクリプトクラス
/// </summary>
public class SaveControllerScene : MonoBehaviour
{
    /// <summary>プレイヤーデータ管理スクリプトクラス</summary>
    [SerializeField] private Player_Data_Main _player_data;
    /// <summary>シーン名</summary>
    [SerializeField] private string _stageName;

    void Start()
    {
        _player_data.Load_Data(_player_data, _player_data.datapath);
    }

    /// <summary>
    /// セーブデータを書き込む
    /// </summary>
    public void SaveDataWrite()
    {
        var index = SceneIndexSerch(_stageName);
        if (-1 != index)
        {
            _player_data.stage_clear_number[index] = true;
            _player_data.Save_Data(_player_data);
        }
        else
        {
            Debug.Log("セーブデータの書き込みに失敗しました。");
            Debug.Log("シーン名[" + _stageName + "]に紐づくシーン番号が見つかりません。");
        }
    }
    
    /// <summary>
    /// 指定されたシーン名とシーン番号を紐づける
    /// ※将来的に他のクラスへ切り離したい
    /// </summary>
    /// <param name="name">ステージのシーン名</param>
    /// <returns>シーン番号</returns>
    private int SceneIndexSerch(string name)
    {
        int index = -1;
        if (name.Equals(StageNameManager.SECTION_000_STAGE_001))
        {
            index = (int)StageClearNumber.SECTION_000_STAGE_001;
        }
        return index;
    }
}

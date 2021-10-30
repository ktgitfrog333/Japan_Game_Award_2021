using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// プレイヤーデータ管理スクリプトクラス
/// </summary>
[System.Serializable]
public class Player_Data_Main : MonoBehaviour, DebugDemo
{
    public string datapath;

    [SerializeField] public bool[] stage_clear_number;   // クリア取得

    [SerializeField] private VisualizeDebugMode _debug;

    public void Save_Data(Player_Data_Main player_data)
    {
        //// ユーザごとに保管するディレクトリが異なる為、Pathを再度設定
        //datapath = Application.dataPath + "/data/data.json";
        ////DebugDemo1("datapath:" + datapath);
        //// JSONに変換
        //string json = JsonUtility.ToJson(player_data);
        ////DebugDemo1("json:" + json);
        //// 保存先を開く
        //StreamWriter writer = new StreamWriter(datapath, false);
        ////DebugDemo1("writer:" + writer);
        //// JSONデータ書き込み
        //writer.WriteLine(json);
        ////DebugDemo1("JSONデータ書き込み");
        //// バッファクリア
        //writer.Flush();
        ////DebugDemo1("バッファクリア");
        //// ファイルを閉じる
        //writer.Close();
        ////DebugDemo1("ファイルを閉じる");
    }

    public void Load_Data(Player_Data_Main player_data, string datapath)
    {
        // ユーザごとに保管するディレクトリが異なる為、Pathを再度設定
        //datapath = Application.dataPath + "/data/data.json";
        //// パスを読み込む
        //StreamReader reader = new StreamReader(datapath);
        //// ファイルを読み込む
        //string data = reader.ReadToEnd();
        //// ファイルを閉じる
        //reader.Close();

        //JsonUtility.FromJsonOverwrite(data,player_data);
    }

    private void Awake()
    {
        //datapath = Application.dataPath + "/data/data.json";
        //DebugDemo1(datapath);
    }

    // ステージクリア記録消去(データ削除)
    public void delete_Data(int max_stage)
    {
        //// 最大のステージ数分だけfalseにする。
        //for (int i = 0; i < max_stage; i++)
        //{
        //    stage_clear_number[i] = false;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DebugDemo1(string message)
    {
        _debug.Log(message);
    }
}

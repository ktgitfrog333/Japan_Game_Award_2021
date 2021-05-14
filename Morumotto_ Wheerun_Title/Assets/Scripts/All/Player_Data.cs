using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Player_Data : MonoBehaviour
{
    public string datapath;

    [SerializeField] public bool[] stage_clear_number;   // クリア取得
    [SerializeField] public int max_stage;
    [SerializeField] public bool delete_start;           // データ削除開始

    public void Save_Data(Player_Data player_data)
    {
        // JSONに変換
        string json = JsonUtility.ToJson(player_data);
        // 保存先を開く
        StreamWriter writer = new StreamWriter(datapath, false);
        // JSONデータ書き込み
        writer.WriteLine(json);
        // バッファクリア
        writer.Flush();
        // ファイルを閉じる
        writer.Close();
    }

    public void Load_Data(Player_Data player_data,string datapath)
    {
        // パスを読み込む
        StreamReader reader = new StreamReader(datapath);
        // ファイルを読み込む
        string data = reader.ReadToEnd();
        // ファイルを閉じる
        reader.Close();

        JsonUtility.FromJsonOverwrite(data,player_data);
    }

    private void Awake()
    {
        datapath = Application.dataPath + "/data/data.json";
    }

    // ステージクリア記録消去(データ削除)
    public void delete_Data(int max_stage)
    {
        // 最大のステージ数分だけfalseにする。
        for (int i = 0; i < max_stage; i++)
        {
            stage_clear_number[i] = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

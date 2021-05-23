using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    public enum Character_Sence
    {
        OPENING_MOVIE,         // オープニングムービー
        PUSH_GAME_START,       // ゲーム開始時に表示
        GAME_SELECT,           // ゲームモードを選択
        GAME_START,            // ゲームスタート(セレクト画面に遷移)
        GAME_DATADELETE_CHECK, // ゲームデータ消去を確認
        GAME_DATADELETE,       // ゲームデータ消去
        GAME_END_CHECK,        // ゲーム終了を確認
        GAME_END,              // ゲーム終了(exeを終了)
        NEXT_STAGESELECT,      // ステージセレクトの遷移
        GAME_STAGESELECT,      // ステージセレクト画面
        NEXT_GAMESTART,        // オープニング画面からタイトルへの遷移
        NEXT_GAMEMAIN,         // ゲームメインの遷移
        NEXT_GAMETITLE         // ステージセレクトからタイトルシーンの遷移
    };

    public enum Player_Input
    {
        UP,                    // 上
        CENTER,                // 真ん中
        DOWN,                  // 下
    }

    public Character_Sence sence;                   // 現在のシーン
    public static Character_Sence before_sence;     // 以前のシーン
    public Character_Sence before_check_sence;      // 現在→以前のシーンに格納する為のシーン
    public Player_Input input;
    public int select_stage_number;

    [SerializeField] private bool game_datacomplete_flg;

    /** 
     * シーン取得
     */
    public void setSence(Character_Sence sence)
    {
        this.sence = sence;
    }

    public void before_setSence(Character_Sence sence)
    {
        before_sence = sence;
    }

    public Character_Sence brfore_getSence()
    {
        return before_sence;
    }

    public void setInput(Player_Input input)
    {
        this.input = input;
    }

    public void set_Data_Complete_TrueFlg()
    {
        game_datacomplete_flg = true;
    }

    public void set_Data_Complete_FalseFlg()
    {
        game_datacomplete_flg = false;
    }

    public bool Data_Complete_FlgCheck()
    {
        if (game_datacomplete_flg == true)
        {
            return true;
        }
        return false;
    }

    public void set_stage_select_number(int select_stage_number)
    {
        this.select_stage_number = select_stage_number;
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

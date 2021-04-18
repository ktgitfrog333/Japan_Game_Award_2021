using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Character_Sence
    {
        PUSH_GAME_START,       // ゲーム開始時に表示
        GAME_SELECT,           // ゲームモードを選択
        GAME_START,            // ゲームスタート(セレクト画面に遷移)
        GAME_DATADELETE_CHECK, // ゲームデータ消去を確認
        GAME_DATADELETE,       // ゲームデータ消去
        GAME_END_CHECK,        // ゲーム終了を確認
        GAME_END               // ゲーム終了(exeを終了)
    };

    private Character_Sence sence;

    public Player()
    {

    }

    public Player(Character_Sence sence)
    {
        this.sence = sence;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player player = new Player(Character_Sence.PUSH_GAME_START);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

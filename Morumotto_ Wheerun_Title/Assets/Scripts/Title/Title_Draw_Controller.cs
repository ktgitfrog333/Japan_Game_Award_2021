using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Draw_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject title_background_object;
    [SerializeField] GameObject push_game_object;
    [SerializeField] GameObject select_object;
    [SerializeField] GameObject game_datadelete_check;
    [SerializeField] GameObject game_end_check;
    [SerializeField] GameObject icon;
    [SerializeField] GameObject load_now;
    [SerializeField] GameObject data_dalete_complete;

    private GameObject player_Draw;
    private Image fade_Draw;
    private float load_now_x;
    private float data_delete_complete_y;
    private float timer;
    private RectTransform icon_rect;
    private Player player;
    
    void Start()
    {
        player_Draw = GameObject.Find("Canvas");
        player = player_Draw.GetComponent<Player>();
        Player_Init();
    }
    public void Player_Init()
    {
        player.sence = Player.Character_Sence.PUSH_GAME_START;
        player.input = Player.Player_Input.UP;
        load_now_x = -1920.0f;        // ロード画面の初期座標位置
        title_background_object.SetActive(true);
        icon_rect = icon.GetComponent<RectTransform>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Draw_Texture(player);
    }

    public void Draw_Texture(Player player)
    {
        switch(player.sence)
        {
            case Player.Character_Sence.PUSH_GAME_START:
            {
                PushGameStart_Draw();
                break;
            }
            case Player.Character_Sence.GAME_SELECT:
            {
                GameSelect_Draw(player);
                break;
            }
            case Player.Character_Sence.GAME_START:
            {
                GameStart_Draw();
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE_CHECK:
            {
                GameDatadeleteCheck_Draw(player);
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE:
            {
                GameDatadelete_Draw();
                break;
            }
            case Player.Character_Sence.GAME_END_CHECK:
            {
                GameEndCheck_Draw(player);
                break;
            }
            case Player.Character_Sence.GAME_END:
            {
                GameEnd_Draw();
                break;
            }
        }
    }

    public void PushGameStart_Draw()
    {
        push_game_object.SetActive(true);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(false);
        game_end_check.SetActive(false);
        icon.SetActive(false);
    }

    public void GameSelect_Draw(Player player)
    {
        push_game_object.SetActive(false);
        select_object.SetActive(true);
        game_datadelete_check.SetActive(false);
        game_end_check.SetActive(false);
        icon.SetActive(true);

        switch(player.input)
        {
            case Player.Player_Input.UP:
            {
                icon.transform.position = new Vector2(-3.0f, -1.0f);
                break;
            }
            case Player.Player_Input.CENTER:
            {
                icon.transform.position = new Vector2(-3.0f,-1.0f);
                break;
            }
            case Player.Player_Input.DOWN:
            {
                icon.transform.position = new Vector2(-2.25f,-2.27f);
                break;
            }
        }
    }

    public void GameStart_Draw()
    {
        load_now.SetActive(true);
        if (load_now.transform.position.x <= 0.0)
        {
            load_now.transform.position -= new Vector3(load_now_x * Time.deltaTime, 0, 0);
        }
        else
        {
            // セレクトシーンに切り替える処理を記載（a版の結合時実施）
        }
    }

    public void GameDatadeleteCheck_Draw(Player player)
    {
        push_game_object.SetActive(false);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(true);
        game_end_check.SetActive(false);
        icon.SetActive(true);

        switch (player.input)
        {
            case Player.Player_Input.UP:
            {
                icon.transform.position = new Vector2(-1.56f, -0.11f);
                break;
            }

            case Player.Player_Input.DOWN:
            {
                icon.transform.position = new Vector2(-1.84f, -1.91f);
                break;
            }
        }
    }

    public void GameDatadelete_Draw()
    {
        game_datadelete_check.SetActive(false);
        icon.SetActive(false);
        data_dalete_complete.SetActive(true);
        select_object.SetActive(true);
        if (data_dalete_complete.transform.position.y > 3.55f)
        {
            data_dalete_complete.transform.position -= new Vector3(0, data_delete_complete_y * Time.deltaTime, 0);
        }
        else
        {
            timer += Time.deltaTime;
            // 2秒経過した後に「GameSelect_Draw」へ遷移
            if (timer >= 2)
            {
                timer = 0;
                data_dalete_complete.SetActive(false);
                player.getSence(Player.Character_Sence.GAME_SELECT);
            }
        }
 
    }

    public void GameEndCheck_Draw(Player player)
    {
        push_game_object.SetActive(false);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(false);
        game_end_check.SetActive(true);
        icon.SetActive(true);

        switch (player.input)
        {
            case Player.Player_Input.UP:
            {
                icon.transform.position = new Vector2(-1.56f, -0.11f);
                break;
            }
            case Player.Player_Input.DOWN:
            {
                icon.transform.position = new Vector2(-1.84f, -1.91f);
                break;
            }
        }
    }

    public void GameEnd_Draw()
    {
        timer += Time.deltaTime;
        // ３秒経過後にフェードイン (ゲーム終了の処理)
        if (timer >=3)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

}

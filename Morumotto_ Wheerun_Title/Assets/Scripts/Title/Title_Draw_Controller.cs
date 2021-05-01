using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Draw_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject title_background_object;
    [SerializeField] private GameObject push_game_object;
    [SerializeField] private GameObject select_object;
    [SerializeField] private GameObject game_datadelete_check;
    [SerializeField] private GameObject game_end_check;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject load_now;
    [SerializeField] private GameObject data_dalete_complete;
    [SerializeField] private GameObject fade_in;

    private RectTransform icon_rect;
    private RectTransform load_now_rect;
    private RectTransform data_delete_complete_rect;
    private Image fade_Draw;
    private GameObject player_Draw;
    private Player player;
    
    private float load_now_position_x;
    private float data_delete_complete_position_y;
    private float data_delete_complete_position_x;
    private float timer;
    private float alpha;
    private float alpha_speed;
    
    void Start()
    {
        Player_Init();
        Texture_Draw_Init();
    }
    public void Player_Init()
    {
        player_Draw = GameObject.Find("Canvas");
        player = player_Draw.GetComponent<Player>();
        player.getSence(Player.Character_Sence.PUSH_GAME_START);
        player.getInput(Player.Player_Input.UP);
        player.set_Data_Complete_FalseFlg();
    }

    public void Texture_Draw_Init()
    {
        title_background_object.SetActive(true);
        icon_rect = icon.GetComponent<RectTransform>();
        load_now_rect = load_now.GetComponent<RectTransform>();
        data_delete_complete_rect = data_dalete_complete.GetComponent<RectTransform>();
        fade_Draw = fade_in.GetComponent<Image>();
        load_now_position_x = -1920.0f;        // ロード画面の初期座標位置
        data_delete_complete_position_x = 650.0f;
        data_delete_complete_position_y = 700.0f;
        alpha = 0;
        alpha_speed = 0.001f;
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
        float updown_tex_position_x = -200.0f;
        float center_tex_position_x = -310.0f;
        float up_tex_position_y = -50.0f;
        float center_tex_position_y = -230.0f;
        float center_tex_position_z = -380.0f;

        if(player.Data_Complete_FlgCheck())
        {
            GameDatadelete_Draw();
        }

        switch (player.input)
        {
            case Player.Player_Input.UP:
            {
                icon_rect.anchoredPosition = new Vector2(updown_tex_position_x,up_tex_position_y);
                break;
            }
            case Player.Player_Input.CENTER:
            {
                icon_rect.anchoredPosition = new Vector2(center_tex_position_x,center_tex_position_y);
                break;
            }
            case Player.Player_Input.DOWN:
            {
                icon_rect.anchoredPosition = new Vector2(updown_tex_position_x, center_tex_position_z);
                break;
            }
        }
    }

    public void GameStart_Draw()
    {
        load_now.SetActive(true);
        float Load_Now_Max_Position = 0.0f;
        if (load_now_rect.anchoredPosition.x <= Load_Now_Max_Position)
        {
            load_now_rect.anchoredPosition -= new Vector2(load_now_position_x * Time.deltaTime, 0);
        }
        else
        {
            // セレクトシーンに切り替える処理を記載（a版の結合時実施）
            SceneManager.LoadScene("Select_Sence");
        }
    }

    public void GameDatadeleteCheck_Draw(Player player)
    {
        push_game_object.SetActive(false);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(true);
        game_end_check.SetActive(false);
        icon.SetActive(true);
        YesNo_Draw(player);
    }

    public void GameDatadelete_Draw()
    {
        data_dalete_complete.SetActive(true);
        float max_rect_y = 490.0f;
        if (data_delete_complete_rect.anchoredPosition.y >= max_rect_y)
        {
            data_delete_complete_rect.anchoredPosition -= new Vector2(0.0f, data_delete_complete_position_y * Time.deltaTime * 0.5f);
        }
        else
        {
           timer += Time.deltaTime;
            float EndTime = 2.0f;
            // 2秒経過した後に「GameSelect_Draw」へ遷移
            if (timer >= EndTime)
            {
                timer = 0;
                data_dalete_complete.SetActive(false);
                player.set_Data_Complete_FalseFlg() ;
                data_delete_complete_position_y = 600.0f;
                data_delete_complete_rect.anchoredPosition = new Vector2(data_delete_complete_position_x, data_delete_complete_position_y);
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
        YesNo_Draw(player);
    }

    public void YesNo_Draw(Player player)
    {
        float yes_position = -150.0f;
        float no_position_x = -200.0f;
        float no_position_y = -350.0f;
        switch (player.input)
        {
            case Player.Player_Input.UP:
                {
                    icon_rect.anchoredPosition = new Vector2(yes_position, yes_position);
                    break;
                }

            case Player.Player_Input.DOWN:
                {
                    icon_rect.anchoredPosition = new Vector2(no_position_x, no_position_y);
                    break;
                }
        }
    }

    public void GameEnd_Draw()
    {
        fade_in.SetActive(true);
        timer += Time.deltaTime;
        float EndTimer = 2.0f;
        int MaxColor = 255;

        fade_Draw.color = new Color(MaxColor, MaxColor, MaxColor,alpha);
        alpha += alpha_speed;        

        // ３秒経過後にフェードイン (ゲーム終了の処理)
        if (timer >= EndTimer || alpha >= 255)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

}

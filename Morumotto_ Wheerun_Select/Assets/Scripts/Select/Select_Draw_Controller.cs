using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_Draw_Controller : MonoBehaviour
{
    // 各種画像のオブジェクト
    [SerializeField] private GameObject select_background;          // 背景
    [SerializeField] private GameObject select_stage_screenimage;   // 各種ステージアイコン(小)
    [SerializeField] private GameObject icon;                       // アイコン
    [SerializeField] private GameObject load_now;                   // ロード中

    // 
    [SerializeField] private GameObject select_stage_images;        // 各種ステージアイコン(大)
    [SerializeField] private Sprite[] stage_image_sprite;           // 各種ステージアイコンのスプライト
    public Image stage_image;                                       // ステージ

    private RectTransform load_now_rect;
    private GameObject player_Draw;                                 // プレイヤーオブジェクト
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
       // 各種初期化
       Player_Init();
       Texture_Draw_Init();
    }

    public void Player_Init()
    {
        // プレイヤーのコンポーネントを取得
        player_Draw = GameObject.Find("Canvas");
        player = player_Draw.GetComponent<Player>();
        player.getSence(Player.Character_Sence.NEXT_STAGESELECT);
        // ステージアイコンのテクスチャのコンポーネントを追加。
        stage_image = select_stage_images.AddComponent<Image>();
    }

    public void Texture_Draw_Init()
    {
        // 各種画像の表示設定
        select_background.SetActive(true);
        select_stage_screenimage.SetActive(true);
        icon.SetActive(true);
        load_now.SetActive(true);
        select_stage_images.SetActive(true);
        stage_image.sprite = stage_image_sprite[player.select_stage_number];
        load_now_rect = load_now.GetComponent<RectTransform>();
        player.select_stage_number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 画像表示
        Draw_Texture(player);
    }

    public void Draw_Texture(Player player)
    {
        // プレイヤーのシーン遷移
        switch(player.sence)
        {
            case Player.Character_Sence.NEXT_STAGESELECT:
            {
                Draw_LoadNow_FadeIn(Player.Character_Sence.GAME_STAGESELECT);
                break;
            }
            case Player.Character_Sence.GAME_STAGESELECT:
            {
                Draw_StageSelect();
                break;
            }
            case Player.Character_Sence.NEXT_GAMEMAIN:
            {
                Draw_LoadNow_FadeOut(Player.Character_Sence.NEXT_GAMEMAIN);
                break;
            }
            case Player.Character_Sence.NEXT_GAMETITLE:
            {
                Draw_LoadNow_FadeOut(Player.Character_Sence.NEXT_GAMETITLE);
                break;
            }
        }
    }

    public void Draw_StageSelect()
    {
        //  ここでスクショした各種ステージアイコン(大)の切り替えを配列で行う。
            stage_image.sprite = stage_image_sprite[player.select_stage_number];
    }

    // フェードイン処理
    public void Draw_LoadNow_FadeIn(Player.Character_Sence Next_Scene)
    {
        float Load_Now_Max_Position = -1920.0f;
        if (load_now_rect.anchoredPosition.x >= Load_Now_Max_Position)
        {
            load_now_rect.anchoredPosition += new Vector2(-1000 * Time.deltaTime, 0);
        }
        else
        {
            player.getSence(Next_Scene);
        }
    }

    // フェードアウト処理
    public void Draw_LoadNow_FadeOut(Player.Character_Sence Next_Scene)
    {
        float Load_Now_Min_Position = 0.0f;
        if (load_now_rect.anchoredPosition.x <= Load_Now_Min_Position)
        {
            load_now_rect.anchoredPosition -= new Vector2(-1000 * Time.deltaTime, 0);
        }
        else
        {
            player.getSence(Next_Scene);
            SenceChange(Next_Scene);
        }
    }

    public void SenceChange(Player.Character_Sence Next_Scene)
    {
        if(Next_Scene == Player.Character_Sence.NEXT_GAMEMAIN)
        {
            SceneManager.LoadScene("main");
        }
        else if(Next_Scene == Player.Character_Sence.NEXT_GAMETITLE)
        {
            SceneManager.LoadScene("Title_Scene");
        }
    }
}

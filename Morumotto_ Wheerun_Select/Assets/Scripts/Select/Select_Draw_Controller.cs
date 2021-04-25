using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Draw_Controller : MonoBehaviour
{
    [SerializeField] private GameObject select_background;
    [SerializeField] private GameObject select_stage_image;
    [SerializeField] private GameObject select_stage_screenimage;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject load_now;

    private RectTransform load_now_rect;
    private GameObject player_Draw;
    private Player player;

    private float load_now_position_x;

    // Start is called before the first frame update
    void Start()
    {
        Player_Init();
        Texture_Draw_Init();
    }

    public void Player_Init()
    {
        player_Draw = GameObject.Find("Canvas");
        player = player_Draw.GetComponent<Player>();
        player.getSence(Player.Character_Sence.NEXT_STAGESELECT);
    }

    public void Texture_Draw_Init()
    {
        select_background.SetActive(true);
        select_stage_image.SetActive(true);
        select_stage_screenimage.SetActive(true);
        icon.SetActive(true);
        load_now.SetActive(true);
        load_now_rect = load_now.GetComponent<RectTransform>();
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
            case Player.Character_Sence.NEXT_STAGESELECT:
            {
                Draw_LoadNow_FadeIn(Player.Character_Sence.GAME_STAGESELECT);
                break;
            }
            case Player.Character_Sence.GAME_STAGESELECT:
            {

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
        }
        
    }
}

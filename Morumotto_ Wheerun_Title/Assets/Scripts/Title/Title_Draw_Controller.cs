using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Draw_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject push_game_object;
    [SerializeField] GameObject select_object;
    [SerializeField] GameObject game_datadelete_check;
    [SerializeField] GameObject game_end_check;

    private GameObject player_Draw;

    private Player player;

    void Start()
    {
        player_Draw = GameObject.Find("DrawController");
        player = player_Draw.GetComponent<Player>();
        player.sence = Player.Character_Sence.PUSH_GAME_START;
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
                GameSelect_Draw();
                break;
            }
            case Player.Character_Sence.GAME_START:
            {
                GameStart_Draw();
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE_CHECK:
            {
                GameDatadeleteCheck_Draw();
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE:
            {
                GameDatadelete_Draw();
                break;
            }
            case Player.Character_Sence.GAME_END_CHECK:
            {
                GameEndCheck_Draw();
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
    }

    public void GameSelect_Draw()
    {
        push_game_object.SetActive(false);
        select_object.SetActive(true);
        game_datadelete_check.SetActive(false);
        game_end_check.SetActive(false);
    }

    public void GameStart_Draw()
    {

    }

    public void GameDatadeleteCheck_Draw()
    {
        push_game_object.SetActive(false);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(true);
        game_end_check.SetActive(false);
    }

    public void GameDatadelete_Draw()
    {
        // 2秒経過した後に「GameSelect_Draw」へ遷移
    }

    public void GameEndCheck_Draw()
    {
        push_game_object.SetActive(false);
        select_object.SetActive(false);
        game_datadelete_check.SetActive(false);
        game_end_check.SetActive(true);
    }

    public void GameEnd_Draw()
    {
        // フェードイン
    }

}

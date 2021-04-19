using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Input_Controller : MonoBehaviour
{
    private GameObject player_input;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player_input = GameObject.Find("DrawController");
        player = player_input.GetComponent<Player>();
        player.sence = Player.Character_Sence.PUSH_GAME_START;
    }

    // Update is called once per frame
    void Update()
    {
        Sence_Input(player);
    }

    public void Sence_Input(Player player)
    {
        switch(player.sence)
        {
            case Player.Character_Sence.PUSH_GAME_START:
            {
                PushGameStart_Input(player);
                break;
            }
            case Player.Character_Sence.GAME_SELECT:
            {
                GameSelect_Input(player);
                break;
            }
            case Player.Character_Sence.GAME_START:
            {
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE_CHECK:
            {
                GameDatadeleteCheck_Input(player);
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE:
            {
                break;
            }
            case Player.Character_Sence.GAME_END_CHECK:
            {
                GameEndCheck_Input(player);
                break;
            }
            case Player.Character_Sence.GAME_END:
            {
                break;
            }
        }
    }

    public void PushGameStart_Input(Player player)
    {
        // 何かボタンを押した時に処理が発生し、「GAME_SELECT」へ遷移する。
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.sence = Player.Character_Sence.GAME_SELECT;
        }
    }

    public void GameSelect_Input(Player player)
    {

    }

    public void GameDatadeleteCheck_Input(Player player)
    {

    }

    public void GameEndCheck_Input(Player player)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Input_Controller : MonoBehaviour
{
    private GameObject player_input;
    private Player player;
    public Select_Draw_Screenimage screen_image;

    // Start is called before the first frame update
    void Start()
    {
        player_input = GameObject.Find("Canvas");
        player = player_input.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.sence == Player.Character_Sence.GAME_STAGESELECT)
        {
            Player_Input();
        }
    }

    public void Player_Input()
    {
        Left_Button_Input();
        Right_Button_Input();
        B_Button_Input();
        A_Button_Input();
    }

    public void Left_Button_Input()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(player.select_stage_number > 0)
            {
                player.select_stage_number--;
            }           
        }
    }

    public void Right_Button_Input()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (player.select_stage_number < 4)
            {
                 player.select_stage_number++;
            }
        }
    }

    public void B_Button_Input()
    {
        if (Input.GetButtonDown("Decision"))
        {
            player.getSence(Player.Character_Sence.NEXT_GAMEMAIN);
        }
    }

    public void A_Button_Input()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            player.getSence(Player.Character_Sence.NEXT_GAMETITLE);
        }
    }
}

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
        player_input = GameObject.Find("Canvas");
        player = player_input.GetComponent<Player>();
        player.sence = Player.Character_Sence.PUSH_GAME_START;
        player.input = Player.Player_Input.UP;
        player.set_Data_Complete_FalseFlg();
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
        if (Input.GetButtonDown("Decision"))
        {
            player.getSence(Player.Character_Sence.GAME_SELECT);
        }
    }

    public void GameSelect_Input(Player player)
    {
        switch (player.input)
        {
            case Player.Player_Input.UP:
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.getInput(Player.Player_Input.CENTER);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_START);
                }
                break;
            }
            case Player.Player_Input.CENTER:
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.getInput(Player.Player_Input.UP);
                }
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.getInput(Player.Player_Input.DOWN);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_DATADELETE_CHECK);
                    player.getInput(Player.Player_Input.UP);
                }
                break;
            }
            case Player.Player_Input.DOWN:
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.getInput(Player.Player_Input.CENTER);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_END_CHECK);
                    player.getInput(Player.Player_Input.UP);
                }
                break;
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            player.getSence(Player.Character_Sence.PUSH_GAME_START);
        }
    }

    public void GameDatadeleteCheck_Input(Player player)
    {
        switch (player.input)
        {
            case Player.Player_Input.UP:
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.getInput(Player.Player_Input.DOWN);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.set_Data_Complete_TrueFlg();
                    player.getSence(Player.Character_Sence.GAME_SELECT);
                }
                break;
            }
            case Player.Player_Input.DOWN:
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.getInput(Player.Player_Input.UP);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_SELECT);
                    player.getInput(Player.Player_Input.CENTER);
                }
                break;
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            player.getSence(Player.Character_Sence.GAME_SELECT);
            player.getInput(Player.Player_Input.CENTER);
        }
    }

    public void GameEndCheck_Input(Player player)
    {
        switch(player.input)
        {
            case Player.Player_Input.UP:
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.getInput(Player.Player_Input.DOWN);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_END);
                }
                break;
            }
            case Player.Player_Input.DOWN:
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.getInput(Player.Player_Input.UP);
                }
                if (Input.GetButtonDown("Decision"))
                {
                    player.getSence(Player.Character_Sence.GAME_SELECT);
                }
                break;
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            player.getSence(Player.Character_Sence.GAME_SELECT);
            player.getInput(Player.Player_Input.DOWN);
        }
    }
}

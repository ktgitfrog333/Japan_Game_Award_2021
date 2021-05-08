using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Sound_Manager : MonoBehaviour
{
    [SerializeField] private AudioClip se_decision;
    [SerializeField] private AudioClip se_cancel;
    [SerializeField] private AudioClip se_scroll;

    private Player player;
    private AudioSource audioSource;
    private GameObject player_Draw;

    // Start is called before the first frame update
    void Start()
    {
        player_Draw = GameObject.Find("Canvas");
        player = player_Draw.GetComponent<Player>();
        player.setSence(Player.Character_Sence.PUSH_GAME_START);
        player.setInput(Player.Player_Input.UP);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Output_Sound();
    }

    public void Output_Sound()
    {
        switch (player.sence)
        {
            case Player.Character_Sence.PUSH_GAME_START:
            {
                PushGameStart_Output_Sound();
                break;
            }
            case Player.Character_Sence.GAME_SELECT:
            {
                GameSelect_Output_Sound();
                break;
            }
            case Player.Character_Sence.GAME_DATADELETE_CHECK:
            {
                GameDatadeleteCheck_Input();
                break;
            }
            case Player.Character_Sence.GAME_END_CHECK:
            {
                GameEndCheck_Input();
                break;
            }
        }
    }

    public void PushGameStart_Output_Sound()
    {
        Output_Sound_Now();
    }

    public void GameSelect_Output_Sound()
    {
        if (player.input == Player.Player_Input.UP)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }
        if (player.input == Player.Player_Input.CENTER)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }
        if (player.input == Player.Player_Input.DOWN)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }
        Output_Sound_Now();
    }

    public void GameDatadeleteCheck_Input()
    {
        YesNo_Output_Sound();
        Output_Sound_Now();
    }

    public void GameEndCheck_Input()
    {
        YesNo_Output_Sound();
        Output_Sound_Now();
    }

    public void Output_Sound_Now()
    {
        if (Input.GetButtonDown("Decision"))
        {
            audioSource.PlayOneShot(se_decision);
        }
        if(player.sence != Player.Character_Sence.PUSH_GAME_START)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                audioSource.PlayOneShot(se_cancel);
            }
        }
    }

    public void YesNo_Output_Sound()
    {
        if (player.input == Player.Player_Input.UP)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }

        if (player.input == Player.Player_Input.DOWN)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }
    }
}

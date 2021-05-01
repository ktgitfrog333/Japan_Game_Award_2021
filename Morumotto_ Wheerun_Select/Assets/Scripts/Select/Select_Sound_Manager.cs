using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Sound_Manager : MonoBehaviour
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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.sence == Player.Character_Sence.GAME_STAGESELECT)
        {
            Output_Sound();
        }
    }

    public void Output_Sound()
    {
        LeftRight_Button_Sound();
        A_Button_Sound();
        B_Button_Sound();
    }

    public void LeftRight_Button_Sound()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(player.select_stage_number > 0 && player.select_stage_number < 4)
            {
                audioSource.PlayOneShot(se_scroll);
            }
        }
    }

    public void B_Button_Sound()
    {
        if (Input.GetButtonDown("Decision"))
        {
            audioSource.PlayOneShot(se_decision);
        }
    }

    public void A_Button_Sound()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            audioSource.PlayOneShot(se_cancel);
        }
    }
}

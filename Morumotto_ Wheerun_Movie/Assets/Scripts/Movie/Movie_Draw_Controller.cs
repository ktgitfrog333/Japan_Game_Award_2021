using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movie_Draw_Controller : MonoBehaviour
{
    [SerializeField] private GameObject fade_in_object;
    [SerializeField] private float alpha;
    [SerializeField] private float alpha_speed;
    [SerializeField] private float timer;
    [SerializeField] private float fade_start_timer;
    private Player player;
    private GameObject player_Draw;
    private Image fade_Draw;
    
    private bool sence_change_flg;

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
        player.setSence(Player.Character_Sence.OPENING_MOVIE);
        timer = 0.0f;
        sence_change_flg = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Texture_Draw_Init()
    {
        fade_in_object.SetActive(false);
        fade_Draw = fade_in_object.GetComponent<Image>();
        alpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Sence_Change();
    }

    public void Sence_Change()
    {
        timer += Time.deltaTime;
        // Bボタン押下又は17秒経過した時
        if(!sence_change_flg)
        {
            if (Input.GetButtonDown("Decision") || timer >= fade_start_timer)
            {
                fade_in_object.SetActive(true);
                sence_change_flg = true;
            }
        }
        else
        {
            Fade_In();
        }
    }

    public void Fade_In()
    {   
        int MaxColor = 255;
        fade_Draw.color = new Color(MaxColor, MaxColor, MaxColor, alpha);
        alpha += alpha_speed * Time.deltaTime;
        if (alpha >= 1)
        {
            // 以前のシーンを格納
            player.before_setSence(Player.Character_Sence.OPENING_MOVIE);
            // タイトルシーンに切り替える処理を記載
            SceneManager.LoadScene("Title_Sence");
        }
    }
}

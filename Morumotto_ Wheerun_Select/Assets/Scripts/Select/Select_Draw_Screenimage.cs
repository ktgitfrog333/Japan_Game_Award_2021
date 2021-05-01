using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Draw_Screenimage : MonoBehaviour
{
    [SerializeField] private Sprite[] stage_image_sprite;
    private GameObject selectstage_object;
    private Image stage_image;
    public int stage_select_number;

    // Start is called before the first frame update
    void Start()
    {
        selectstage_object = GameObject.Find("SELECT_STAGE_IMAGES");
        selectstage_object.SetActive(true);
        stage_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Draw_Screen_Image();
    }

    public void Draw_Screen_Image()
    {
        stage_image.sprite = stage_image_sprite[stage_select_number];
    }
}

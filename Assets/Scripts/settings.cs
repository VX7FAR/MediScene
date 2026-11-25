using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;

    [Header("UI Components")]
    [SerializeField] Slider mouse_slider;
    [SerializeField] Text mouse_slider_text;
    [SerializeField] Slider fov;
    [SerializeField] Text fov_slider_text;


    void Start()
    {
        if (!PlayerPrefs.HasKey("first_launch"))
        {
            PlayerPrefs.SetInt("first_launch", 1);
            PlayerPrefs.SetFloat("mouse_spd", 5.0f);
            PlayerPrefs.SetFloat("fov", 60);
        }

        mouse_slider.value = PlayerPrefs.GetFloat("mouse_spd");
        cam.fieldOfView = PlayerPrefs.GetFloat("fov");
    }

    void Update()
    {
        mouse_speed();
        fov_change();
    }

    public void mouse_speed()
    {
        float value = mouse_slider.value;
        player.GetComponent<FPS>().mouse_Sen = value;
        PlayerPrefs.SetFloat("mouse_spd", value);
        mouse_slider_text.text = ((int)value).ToString();
    }

    public void fov_change()
    {
        float value = fov.value;
        cam.fieldOfView = value;
        PlayerPrefs.SetFloat("fov", value);
        fov_slider_text.text = ((int)value).ToString();
    }
}

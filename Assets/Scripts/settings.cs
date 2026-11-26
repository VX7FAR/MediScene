using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    public UniversalRenderPipelineAsset urp_Asset;

    [Header("UI Components")]
    [SerializeField] Slider mouse_slider;
    [SerializeField] Text mouse_slider_text;
    [SerializeField] Slider fov;
    [SerializeField] Text fov_slider_text;
    // [SerializeField]Dropdown screen_Resolution;
    //     int old_screen_res_state;
    [SerializeField] Toggle vsync_toggle;
    [SerializeField] Toggle shadow_toggle;
    float shadow_distance = 50f;
    [SerializeField]Dropdown res_dd;
    [SerializeField]Toggle fullscr;

    void Start()
    {
        // shadow_distance = urp_Asset.shadowDistance;
        if (!PlayerPrefs.HasKey("first_launch"))
        {
            PlayerPrefs.SetInt("first_launch", 1);
            PlayerPrefs.SetFloat("mouse_spd", 5.0f);
            PlayerPrefs.SetFloat("fov", 60);
            PlayerPrefs.SetInt("vsync", 0);
            PlayerPrefs.SetInt("resolution_i", 0);
            PlayerPrefs.SetInt("fullscreen", 1);
        }

        mouse_slider.value = PlayerPrefs.GetFloat("mouse_spd");
        cam.fieldOfView = PlayerPrefs.GetFloat("fov");

        vsync_toggle.isOn = PlayerPrefs.GetInt("vsync") == 1 ? true : false;
        vsync_change(vsync_toggle.isOn);    
    
        shadow_toggle.isOn = PlayerPrefs.GetInt("shadow") == 1 ? true : false;
        shadow_change(shadow_toggle.isOn);

        res_dd.value = PlayerPrefs.GetInt("resolution_i");
        resolution_change(res_dd.value);

        fullscr.isOn = PlayerPrefs.GetInt("fullscreen") == 1 ? true : false;
        fullscreen_toggle(fullscr.isOn);
    }

    void Update()
    {
        mouse_speed();
        fov_change();

        // QualitySettings.vSyncCount = vsync_toggle.isOn ? 1 : 0;
        // urp_Asset.shadowDistance = shadow_toggle.isOn ? shadow_distance : 0.0f;
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

    public void shadow_change(bool value)
    {
        if (value)
        {
            urp_Asset.shadowDistance = shadow_distance;
        }
        else
        {
            urp_Asset.shadowDistance = 0.0f;
        }

        // urp_Asset.shadowDistance = shadow_distance * (value ? 1 : 0);
        PlayerPrefs.SetInt("shadow", value ? 1 : 0);
    }

    public void vsync_change(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
        PlayerPrefs.SetInt("vsync", QualitySettings.vSyncCount);
    }

    public void fullscreen_toggle(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("fullscreen", value ? 1 : 0);
    }

    public void resolution_change(int value)
    {
        switch (value)
        {
            case 0: // DEFAULT
                Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreenMode);
                break;

            case 1: // 640 x 360
                Screen.SetResolution(640, 360, Screen.fullScreenMode);
                break;

            case 2: // 960 x 540
                Screen.SetResolution(960, 540, Screen.fullScreenMode);
                break;

            case 3: // 1280 x 720
                Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                break;

            case 4: // 1920 x 1080
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;

            case 5: // 2560 x 1440
                Screen.SetResolution(2560, 1440, Screen.fullScreenMode);
                break;
        }

        PlayerPrefs.SetInt("resolution_i", value);
    }
}


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
    [SerializeField]Toggle vsync_toggle;
    [SerializeField]Toggle shadow_toggle;
        float shadow_distance;

    void Start()
    {
        shadow_distance = urp_Asset.shadowDistance;
        if (!PlayerPrefs.HasKey("first_launch"))
        {
            PlayerPrefs.SetInt("first_launch", 1);
            PlayerPrefs.SetFloat("mouse_spd", 5.0f);
            PlayerPrefs.SetFloat("fov", 60);
            PlayerPrefs.SetInt("vsync", 0);
        }

        mouse_slider.value = PlayerPrefs.GetFloat("mouse_spd");
        cam.fieldOfView = PlayerPrefs.GetFloat("fov");
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("vsync");
        vsync_toggle.isOn = PlayerPrefs.GetInt("vsync") == 1 ? true : false;
    }

    void Update()
    {
        mouse_speed();
        fov_change();
        
        QualitySettings.vSyncCount = vsync_toggle.isOn ? 1 : 0;
        urp_Asset.shadowDistance = shadow_toggle.isOn ? shadow_distance : 0.0f;
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

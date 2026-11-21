    using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public enum ui_state
    {
        HUD, Pause
    }
    public ui_state state_of_ui;

    [Header("UI")]
    [SerializeField]GameObject HUD;
    [SerializeField]GameObject paused;

    [Header("Gameobjects")]
    [SerializeField]GameObject player;

    [Header("Input")]
    PlayerInputManager input_manager;
    bool pause_toggle;

    void Start()
    {
        set_UI(ui_state.HUD);
        input_manager = player.GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        pause_toggle = input_manager.pause_toggle_isPressed;

        if (pause_toggle)
        {
            if(state_of_ui == ui_state.HUD)
            {
                state_of_ui = ui_state.Pause;
            }
            else if(state_of_ui == ui_state.Pause)
            {
                state_of_ui = ui_state.HUD;
            }
        }

        set_UI(state_of_ui);    
    }

    public void set_UI(ui_state setto)
    {
        switch (setto)
        {
            case ui_state.HUD:
                HUD.SetActive(true);
                paused.SetActive(false);
                Time.timeScale = 1.0f;
                player.GetComponent<FPS>().enabled = true;
                break;

            case ui_state.Pause:
                paused.SetActive(true);
                HUD.SetActive(false);
                Time.timeScale = 0.0f;
                player.GetComponent<FPS>().enabled = false;
                break;

            default:
                break;
        }
        state_of_ui = setto;
    }

    public void button_ui(string setstate)
    {
        if(setstate == "hud")
        {
            set_UI(ui_state.HUD);
        }
        else if(setstate == "settings")
        {
            Debug.Log("settings");
        }
        else if(setstate == "re_day")
        {
            Debug.Log("Restart day");
        }
        else if(setstate == "quit")
        {
            Debug.Log("APP QUIT");      
        }
    }

    public void change_mouse_spd(float value)
    {
        FPS plr = player.GetComponent<FPS>();
        if (plr.let_mousesen_change)
        {
            plr.mouse_Sen = value;
        }
    }
}

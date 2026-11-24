using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public enum ui_state
    {
        HUD, Pause, settings
    }
    public ui_state state_of_ui;

    [Header("UI")]
    [SerializeField]GameObject HUD;
    [SerializeField]GameObject paused;
    [SerializeField]GameObject settings;
    [SerializeField]GameObject settings_controls;
    [SerializeField]GameObject settings_audio;
    [SerializeField]GameObject settings_graphics;

    [Header("Gameobjects")]
    [SerializeField]GameObject player;

    [Header("Input")]
    public PlayerInput input_system;
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
                set_UI(ui_state.Pause);    
            }
            else if(state_of_ui == ui_state.Pause)
            {
                set_UI(ui_state.HUD);
            }
        }

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
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case ui_state.Pause:
                paused.SetActive(true);
                HUD.SetActive(false);
                Time.timeScale = 0.0f;
                player.GetComponent<FPS>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                break;

            case ui_state.settings:
                paused.SetActive(false);
                HUD.SetActive(false);
                settings.SetActive(true);
                settings_ui("controls");
                break;

            default:
                break;
        }
        state_of_ui = setto;
    }

    public void uistate(string s)
    {
        switch (s)
        {
            case "hud":
                set_UI(ui_state.HUD);
                break;

            case "setting":
                set_UI(ui_state.settings);  
                settings_ui("controls");
                break;

            case "restart":
                Debug.Log("re_Day");
                break;

            case "quit":
                Debug.Log("quit");
                break;

            default:
                break;
        }
    }

    public void settings_ui(string setstate)
    {
        paused.SetActive(false);
        settings.SetActive(true);
        settings_controls.SetActive(false);
        settings_audio.SetActive(false);
        settings_graphics.SetActive(false);
        if(setstate == "controls")
        {
            settings_controls.SetActive(true);
            settings_audio.SetActive(false);
            settings_graphics.SetActive(false);
        }
        else if(setstate == "audio")
        {
            settings_controls.SetActive(false);
            settings_audio.SetActive(true);
            settings_graphics.SetActive(false);
        }
        else if(setstate == "graphics")
        {
            settings_controls.SetActive(false);
            settings_audio.SetActive(false);
            settings_graphics.SetActive(true);
        }
    }

    public void change_mouse_spd(float value)
    {
        FPS plr = player.GetComponent<FPS>();
        if (plr.let_mousesen_change)
        {
            plr.mouse_Sen = value;
        }
    
        // Debug.Log(value);
    }

    public void rebind_input(string toedit, int binding_index = 0)
    {
        InputAction action = input_system.actions[toedit];
        action.Disable();

        action.PerformInteractiveRebinding(binding_index).OnComplete(operation =>
        {
            operation.Dispose();
            action.Enable();
        }).Start();
    }

    public void Rebind(string bind)
    {
        switch (bind)
        {
            case "pause":
                rebind_input("Pause");
                break;

            default:
                break;
        }
    }
}

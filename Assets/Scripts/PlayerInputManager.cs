using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]InputActionAsset interaction_input;
    InputAction pick_n_interact;
    InputAction use_input;
    InputAction drop_input;
    InputAction wasd_input;
    InputAction drive_input;

    public bool lockmovement;

    public bool pick_interact {get; private set;}
    public bool use {get; private set;}
    public bool drop {get; private set;}
    public Vector2 wasd {get; private set;}
    public bool drive {get; private set;}


    void OnEnable()
    {
        interaction_input.FindActionMap("Interactions").Enable();
        interaction_input.FindActionMap("FPS").Enable();
    }

    void Awake()
    {
        pick_n_interact = interaction_input.FindActionMap("Interactions").FindAction("Interact");
        use_input = interaction_input.FindActionMap("Interactions").FindAction("Use");
        drop_input = interaction_input.FindActionMap("Interactions").FindAction("Drop");
        drive_input = interaction_input.FindActionMap("Interactions").FindAction("Drive");
        wasd_input = interaction_input.FindActionMap("FPS").FindAction("WASD");
    }

    void Update()
    {
        pick_interact = pick_n_interact.WasPressedThisFrame();
        use = use_input.WasPressedThisFrame();
        drop = drop_input.WasPressedThisFrame();
        drive = drive_input.WasPressedThisFrame();

        wasd = lockmovement ? new Vector2(0.0f,0.0f) : wasd_input.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        interaction_input.FindActionMap("Interactions").Disable();
    }
}

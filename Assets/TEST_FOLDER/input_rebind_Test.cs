using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class input_rebind_Test : MonoBehaviour
{
    [SerializeField]InputActionAsset inputaction;
    InputAction fire;

    void Start()
    {
        inputaction.FindActionMap("test1").Enable();
        fire = inputaction.FindActionMap("test1").FindAction("fire");
    }

    void Update()
    {
        if (fire.WasPressedThisFrame())
        {
            Debug.Log("Fire pressed");
        }
    }

    void OnDisable()
    {
        inputaction.FindActionMap("test1").Disable();
    }
}

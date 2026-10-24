using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPS : MonoBehaviour
{
    UnityEngine.Vector3 init_position;

    public float flip = 1.0f;
    public float flipmousey = 1;

    //Mouse Look
    float inputhorizontal;
    float inputvertical;
    float xrotation;
    [SerializeField] GameObject player;
    [SerializeField] CharacterController char_controller;
    [SerializeField] GameObject cam;
    public float mouse_Sen;

    //Movement
    public float movement_speed;
    float move_z;       //Forward n backward
    float move_x;       //Left n right
    UnityEngine.Vector3 move;

    //Gravity
    RaycastHit hit;
    Ray ray;
    [SerializeField] float gravity = -9.8f;
    bool onground = false;

    [SerializeField] InputActionAsset INPUT;
    InputAction wasd_input;
    Vector2 wasd_velocity;
    Vector2 smooth_wasd;

    public bool fixkey = false;
    Vector2 fixed_key2;
    [SerializeField] float fixed_key_spd;

    void Awake()
    {
        wasd_input = INPUT.FindActionMap("FPS").FindAction("WASD");
    }

    void OnEnable()
    {
        INPUT.FindActionMap("FPS").Enable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        init_position = gameObject.transform.position;
    }

    void Update()
    {
        ray = new Ray(player.transform.position, -player.transform.up);

        Vector2 wasd_target = wasd_input.ReadValue<Vector2>();
        smooth_wasd = Vector2.SmoothDamp(smooth_wasd, wasd_target, ref wasd_velocity, 0.1f);

        inputhorizontal = Input.GetAxis("Mouse X") * mouse_Sen * flip * flipmousey;
        inputvertical = Input.GetAxis("Mouse Y") * mouse_Sen * flip;
        if (!fixkey)
        {
            move_x = smooth_wasd.x * movement_speed * Time.deltaTime * flip * flipmousey;
            move_z = smooth_wasd.y * movement_speed * Time.deltaTime * flip;
        }
        else
        {
            if (smooth_wasd.x != 0)
            {
                fixed_key2.x = Mathf.Sign(smooth_wasd.x);
            }
            if (smooth_wasd.y != 0)
            {
                fixed_key2.y = Mathf.Sign(smooth_wasd.y);
            }
            move_x = fixed_key2.x * fixed_key_spd * Time.deltaTime * flip * flipmousey;
            move_z = fixed_key2.y * fixed_key_spd * Time.deltaTime * flip * flipmousey;
        }

        move = transform.forward * move_z + transform.right * move_x;
        if (Physics.Raycast(ray, out hit, 50f))
        {
            onground = hit.distance > 1f ? false : true;
        }

        if (onground)
        {
            move.y = gravity;
        }
        else
        {
            move.y += gravity * Time.deltaTime;
        }

        xrotation -= inputvertical;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        gameObject.transform.Rotate(0f, inputhorizontal, 0f);
        cam.transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);

        char_controller.Move(move);

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = init_position;
        }
    }

    void OnDisable()
    {
        INPUT.FindActionMap("Movement").Disable();
    }
}

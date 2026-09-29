using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FPS : MonoBehaviour
{
    //Mouse Look
    float inputhorizontal;
    float inputvertical;
    float xrotation;
    [SerializeField]GameObject player;
    [SerializeField]CharacterController char_controller;
    [SerializeField]GameObject cam;
    [SerializeField]float mouse_Sen;

    //Movement
    [SerializeField]float movement_speed;
    float move_z;       //Forward n backward
    float move_x;       //Left n right
    UnityEngine.Vector3 move;

    //Gravity
    RaycastHit hit;
    Ray ray;
    float gravity = -9.8f;
    bool onground = false;

    void setup()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        ray = new Ray(player.transform.position, -player.transform.up);
        inputhorizontal = Input.GetAxis("Mouse X") * mouse_Sen * Time.deltaTime;
        inputvertical = Input.GetAxis("Mouse Y") * mouse_Sen * Time.deltaTime * -1.0f;
        move_x = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;
        move_z = Input.GetAxis("Vertical") * movement_speed * Time.deltaTime;
        move = transform.forward * move_z + transform.right * move_x;
        if(Physics.Raycast(ray, out hit, 50f))
        {
            onground = hit.distance > 1f?false:true;
        }

        if (onground)
        {
            move.y = gravity;
        }
        else
        {
            move.y += gravity * Time.deltaTime;
        }

        xrotation += inputvertical;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        Debug.Log(cam.transform.rotation);

        player.transform.Rotate(0f, inputhorizontal,0f);
        if(xrotation < 85f && xrotation > -85f)
        {
            cam.transform.Rotate(inputvertical, 0f, 0f);
        }

        char_controller.Move(move);
    }
}

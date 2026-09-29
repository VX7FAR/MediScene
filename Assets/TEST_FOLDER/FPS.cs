using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    float inputhorizontal;
    float inputvertical;
    float xrotation;
    [SerializeField]GameObject player;
    [SerializeField]GameObject cam;
    [SerializeField]float mouse_Sen;

    void setup()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        inputhorizontal = Input.GetAxis("Mouse X") * mouse_Sen * Time.deltaTime;
        inputvertical = Input.GetAxis("Mouse Y") * mouse_Sen * Time.deltaTime * -1.0f;

        xrotation += inputvertical;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        Debug.Log(cam.transform.rotation);

        player.transform.Rotate(0f, inputhorizontal,0f);
        if(xrotation < 85f && xrotation > -85f)
        {
            cam.transform.Rotate(inputvertical, 0f, 0f);
        }
    }
}

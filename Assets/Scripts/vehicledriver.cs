using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicledriver : MonoBehaviour
{
    [SerializeField]GameObject vehicle;
    [SerializeField]GameObject player;
    [SerializeField]GameObject sit_At;
    PlayerInputManager inputsystem;
    bool candrive;
    bool is_driving;

    void Start()
    {
        inputsystem = gameObject.GetComponent<PlayerInputManager>();
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "drivable" && !is_driving)
        {
            candrive = true;
        }
    }

    void Update()
    {
        if(candrive && inputsystem.drive && !is_driving)
        {
            is_driving = true;
        }
        else if(is_driving && inputsystem.drive)
        {
            is_driving = false;
        }


        if (is_driving)
        {
            player.transform.position = sit_At.transform.position;
            player.transform.SetParent(vehicle.transform);
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            player.transform.SetParent(null);
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}

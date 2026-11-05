using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class automatic_door_controller : MonoBehaviour
{
    [SerializeField]string[] open_for = {"Player", "NPC"};
    Animator door_animation_controller;
    bool open;

    void OnTriggerStay(Collider human)
    {
        if (human.tag == "Player")
        {
            open = true;
            Debug.Log(open);
        }
    }

    void OnTriggerExit(Collider human)
    {
        if(human.tag == "Player")
        {
            open = false;
        }
    }

    void Start()
    {
        door_animation_controller = gameObject.GetComponent<Animator>();
        // toggle_animator(0);
    }

    void Update()
    {
        door_animation_controller.SetBool("open", open);
        Debug.Log(open);
    }

    public void toggle_animator(int set)
    {
        door_animation_controller.enabled = set == 1;
    }
}

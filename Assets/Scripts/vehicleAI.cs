using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class vehicleAI : MonoBehaviour
{
    [SerializeField]Transform start_At;

    [SerializeField]float speed;
    GameObject at;
    
    Transform move_to;
    GameObject next;
    Vector3 direction;

    bool takedecision = false;
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Waypoint>() != null)
        {
            at = other.gameObject;
            takedecision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Waypoint>() != null && at == other.gameObject)
        {
            takedecision = false;
        }
    }

    void Start()
    {
        gameObject.transform.position = start_At.transform.position;
    }

    void Update()
    {
        if (takedecision)
        {
            GameObject[] change_to = at.GetComponent<Waypoint>().next_waypoint;
            if(change_to.Length != 0)
            {
                next = change_to[UnityEngine.Random.Range(0, change_to.Length)];
                move_to = next.transform;   
            }
            Debug.Log("Move to: " + next.name);
            takedecision = false;
        }

        Quaternion rotate_to = Quaternion.LookRotation(move_to.position - transform.position);
        
        transform.rotation = rotate_to;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
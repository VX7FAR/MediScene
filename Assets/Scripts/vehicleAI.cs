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
    
    bool move;

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
            List<GameObject> change_to = at.GetComponent<Waypoint>().next_waypoint;
            if(change_to.Count != 0 && change_to != null)
            {
                next = change_to[UnityEngine.Random.Range(0, change_to.Count)];
                move_to = next.transform;   
                Debug.Log("Move to: " + next.name);
                takedecision = false;
                move = true;
            }
            else
            {
                move = false;
            }
        }

        if (move)
        {
            Vector3 direction = move_to.transform.position - transform.position;
            quaternion target_rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, 5f * Time.deltaTime);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
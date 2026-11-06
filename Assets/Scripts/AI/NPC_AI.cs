using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NPC_AI : MonoBehaviour
{
    enum task
    {
        idle, walking, takingitem, checkout, leavestore, takedecision
    }
    task action = task.idle;


    [SerializeField] Transform start_At;
    [SerializeField] float speed;
    [SerializeField] Transform at;
    Vector3 direction;
    Waypoint point;
    Transform move_to;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Waypoint>() != null && other.GetComponent<Waypoint>().next_waypoint != null && other.GetComponent<Waypoint>().next_waypoint.Count != 0)
        {
            point = other.GetComponent<Waypoint>();
            action = task.takedecision;
        }
        else
        {
            action = task.idle;
        }
    }

    void Start()
    {
        transform.position = start_At.position;
    }

    void Update()
    {
        if(action == task.idle)
        {
            direction = Vector3.zero;
            move_to = null;

        }

        if (action == task.takedecision)
        {
            List<GameObject> next = point.next_waypoint;
            if (next != null || next.Count > 0)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                move_to = next[UnityEngine.Random.Range(0, next.Count)].transform;
                action = task.walking;
            }
        }

        if(action == task.walking)
        {
            direction = move_to.transform.position - gameObject.transform.position;
            Quaternion target_rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, 4f * Time.deltaTime);
        
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class vehicleAI : MonoBehaviour
{
    [SerializeField]SplineContainer path;
    [SerializeField, Range(0f,1f)]float progress;

    [SerializeField]float carspeed;
    [SerializeField]float lookAhead;
    [SerializeField]float turn_speed = 15f;
    Quaternion target;

    bool reached = false;
    Vector3 to_knot;
    void Start()
    {
        to_knot = path.transform.TransformPoint((Vector3)path.Spline[2].Position);
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if(Vector3.Distance(to_knot, gameObject.transform.position) < 1f)
        {
            Debug.Log("Reached");
            reached = true;
        }

        if (reached)
        {
            int to = UnityEngine.Random.Range(0,3);
            to_knot = path.transform.TransformPoint((Vector3)path.Spline[to].Position);
            reached = false;
        }

        Vector3 position = path.EvaluatePosition(progress);
        Vector3 direction = path.EvaluateTangent(progress);
        // Vector3 target_position = path.EvaluatePosition(progress + lookAhead);   //Not yet being used
        Vector3 target_direction = path.EvaluateTangent((progress + lookAhead) > 1.0f ? 0f : progress + lookAhead);

        progress += carspeed *Time.fixedDeltaTime;
        if(progress > 1.0f)
        {
            progress = 0.0f;
        }

        transform.position = position;

        if(target_direction.sqrMagnitude > 0.0001f)
        {
            target = Quaternion.LookRotation(target_direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, turn_speed * Time.fixedDeltaTime);    
        }
    }
}   

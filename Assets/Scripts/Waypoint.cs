using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject[] next_waypoint;

    void OnDrawGizmos()
    {
        Mesh arrow = Resources.Load<Mesh>("pointer");

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(gameObject.transform.position, 0.75f);

        foreach(GameObject point in next_waypoint)
        {
            Vector3 direction = point.transform.position - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            float distance = Vector3.Distance(gameObject.transform.position, point.transform.position);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(gameObject.transform.position, point.transform.position);
            Gizmos.color = Color.blue;
            Gizmos.DrawMesh(arrow, gameObject.transform.position + direction.normalized * (distance / 2), rotation );
        }
    }
}

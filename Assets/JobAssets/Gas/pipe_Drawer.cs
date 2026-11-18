using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe_Drawer : MonoBehaviour
{
    [SerializeField]Transform nozzle;
    [SerializeField]Transform dispenser;

    [SerializeField]int segments;
    [SerializeField]float sag;

    [SerializeField]LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;
    }

    void LateUpdate()
    {
        Vector3 end = nozzle.transform.position;
        Vector3 start = dispenser.transform.position;

        for(int i=0; i < segments; i++)
        {
            float t = i/ (float)(segments - 1);

            Vector3 point = Vector3.Lerp(start, end, t);

            point.y -= sag * 4.0f * t * (1.0f - t); 

                line.SetPosition(i, point);
        }
    }
}

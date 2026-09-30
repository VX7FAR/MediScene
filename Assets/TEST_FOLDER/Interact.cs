using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Interact : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField]float range;
    GameObject looking_At;
    bool inhand;
    [SerializeField]GameObject pickup_position;
    string[] ignoreray = {"floor", "Player"};

    void Update()
    {
        ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
    
        if(Physics.Raycast(ray, out hit, range))
        {
            if(hit.collider != null && !ignoreray.Contains(hit.collider.tag))
            {
                looking_At = hit.collider.gameObject;
            }
            else
            {
                looking_At = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && looking_At != null && !inhand)
        {
            if(!looking_At.GetComponent<data_mounter>().data.canpick) return;

            looking_At.transform.localPosition = pickup_position.transform.position;
            looking_At.transform.parent = pickup_position.transform;
            looking_At.transform.localScale = looking_At.GetComponent<data_mounter>().data.pickup_size;
            inhand = true;
        }
    }
}

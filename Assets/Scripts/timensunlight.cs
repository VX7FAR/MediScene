using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timensunlight : MonoBehaviour
{
    [SerializeField]GameObject sun;
    Vector3 sundir;
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        sundir = (sun.transform.position - gameObject.transform.position).normalized;
        ray = new Ray(gameObject.transform.position, sundir);
        if(Physics.Raycast(ray, out hit, 1000000f) && hit.collider.gameObject == null)
        {
            Debug.Log("In shadow");
        }
        else
        {
            Debug.Log("In Sunlight");
        }
    }
}

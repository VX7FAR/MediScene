using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxicair : MonoBehaviour
{
    [SerializeField]float health = 40;

    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray = new Ray(gameObject.transform.position, gameObject.transform.up);
        if(Physics.Raycast(ray, out hit, 100000f))
        {
        }
        else
        {
            health -= Time.deltaTime;
            Debug.Log(health);            
        }

        if(health < 0)
        {
            Debug.Log("DED");
        }
    }
}

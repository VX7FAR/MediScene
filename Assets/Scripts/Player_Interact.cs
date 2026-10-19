using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public struct mounter_n_obj
    {
        public GameObject obj;
        public Itemdata data;
    }

    mounter_n_obj interacting_with;
    [SerializeField]GameObject cam;
    RaycastHit hit;
    float range = 100.0f;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, range) && hit.collider != null && hit.collider.gameObject.GetComponent<mounter>() != null)
        {
            interacting_with.obj = hit.collider.gameObject;
            interacting_with.data = hit.collider.gameObject.GetComponent<mounter>().itemdata;
        }
    }
}

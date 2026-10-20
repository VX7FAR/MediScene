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

    bool isholding = false;
    [SerializeField]Transform moveto;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isholding)
        {
            if(Physics.Raycast(ray, out hit, range) && hit.collider != null && hit.collider.gameObject.GetComponent<mounter>() != null)
            {
                interacting_with.obj = hit.collider.gameObject;
                interacting_with.data = hit.collider.gameObject.GetComponent<mounter>().itemdata;
                if(interacting_with.obj.GetComponent<Rigidbody>() != null)
                {
                    interacting_with.obj.GetComponent<Rigidbody>().isKinematic = true;  
                }
                isholding = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && isholding){
            interacting_with.data.use();
        }
        if(isholding && Input.GetKeyDown(KeyCode.G))
        {
            if(interacting_with.obj.GetComponent<Rigidbody>() != null)
            {
                interacting_with.obj.GetComponent<Rigidbody>().isKinematic = false;
            }
            isholding = false;  
            interacting_with.obj = null;
            interacting_with.data = null;
        }
        if (isholding)
        {
            interacting_with.obj.transform.position = moveto.transform.position;
            interacting_with.obj.transform.rotation = moveto.transform.rotation;
        }
    }
}
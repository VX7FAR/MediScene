using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasJob : MonoBehaviour
{
    public struct mounter_n_obj
    {
        public GameObject obj;
        public Itemdata data;
    }

    public mounter_n_obj inhand;
    public mounter_n_obj looking_At;
    public GameObject cam;
    RaycastHit hit;
    float range = 100.0f;
    [SerializeField] Transform moveto;

    public bool isholding = false;


    PlayerInputManager input_system;

    bool pick_n_interact;
    bool use_input;
    bool drop_input;

    void Start()
    {
        input_system = gameObject.GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        pick_n_interact = input_system.pick_interact;
        use_input = input_system.use;
        drop_input = input_system.drop;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null)
            {
                if (hit.collider.GetComponent<mounter>().itemdata is gas_itemdata)
                {
                    looking_At.obj = hit.collider.gameObject;
                    looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                    if (pick_n_interact && !isholding && looking_At.data.can_pick)
                    {
                        inhand = looking_At;
                        isholding = true;
                    }
                    if (use_input && !looking_At.data.can_pick)
                    {
                        UseItem(looking_At);
                    }
                }
            }
        }
        else
        {
            looking_At.data = null;
            looking_At.obj = null;
        }

        if (isholding)
        {
            inhand.obj.transform.position = moveto.transform.position;
            inhand.obj.transform.rotation = moveto.transform.rotation;
            if (inhand.obj.GetComponent<Rigidbody>() != null)
            {
                inhand.obj.GetComponent<Rigidbody>().isKinematic = true;
            }
            setcollider(inhand.obj, false);
        }

        if (drop_input && isholding && inhand.obj != null)
        {
            if (inhand.obj.GetComponent<Rigidbody>() != null)
            {
                inhand.obj.GetComponent<Rigidbody>().isKinematic = false;
            }
            setcollider(inhand.obj, true);

            inhand.obj = null;
            inhand.data = null;
            isholding = false;
        }

        if (use_input && isholding)
        {
            UseItem(inhand);
        }
    }

    void UseItem(mounter_n_obj to_use)
    {
        if (to_use.data is gas_itemdata gasdata)
        {
            switch (gasdata.gasitem)
            {
                case gas_itemdata.gasitem_category.product:
                    inhand.obj = to_use.obj;
                    inhand.data = gasdata;
                    isholding = true;
                    Debug.Log("Basic item picked");
                    break;

                default:
                    Debug.Log("Invalid item to use");
                    break;
            }
        }
    }

    void setcollider(GameObject obj, bool state)
    {
        if(obj.GetComponent<BoxCollider>() != null)
        {
            obj.GetComponent<BoxCollider>().enabled = state;
        }
    }
}

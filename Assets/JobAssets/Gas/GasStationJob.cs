using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStationJob : MonoBehaviour
{
    public struct mounter_n_obj
    {
        public GameObject obj;
        public Itemdata data;
    }

    public mounter_n_obj inhand;
    public  mounter_n_obj looking_At;
    public GameObject cam;
    RaycastHit hit;
    float range = 100.0f;
    [SerializeField] Transform holdat;

    public bool isholding = false;
    public bool product_checked = false;
    public GameObject counter;

    PlayerInputManager player_input;
    bool pick_n_interact;
    bool drop_input;
    bool use_input;

    void OnTriggerEnter(Collider colliding_to)
    {

    }

    void Start()
    {
        player_input = gameObject.GetComponent<PlayerInputManager>();

        if (gameObject.GetComponent<Interact>().isholding)
        {
            inhand.obj = gameObject.GetComponent<Interact>().inhand.obj;
            inhand.data = gameObject.GetComponent<Interact>().inhand.data;
            isholding = gameObject.GetComponent<Interact>().isholding;
        }
    }

    void Update()
    {
        pick_n_interact = player_input.pick_interact;
        use_input = player_input.use;
        drop_input = player_input.drop;

        //BASIC INTERACTION
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null)
            {
                if(hit.collider.GetComponent<mounter>().itemdata.obj_type == Itemdata.categories.GasStation)
                {
                    looking_At.obj = hit.collider.gameObject;
                    looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                    if (pick_n_interact && !isholding && looking_At.data.can_pick)
                    {
                        //Pick in hand
                        inhand = looking_At;
                        isholding = true;
                        if(inhand.obj.GetComponent<Rigidbody>() != null)
                        {
                            inhand.obj.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        foreach (Collider col in inhand.obj.GetComponents<Collider>())
                        {
                            col.enabled = false;
                        }   
                    }
                    if(pick_n_interact && !looking_At.data.can_pick)
                    {
                        //Dont pick but interact
                        use(looking_At);
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
            inhand.obj.transform.position = holdat.transform.position;
            inhand.obj.transform.rotation = holdat.transform.rotation;
        }

        if(drop_input && isholding && inhand.obj != null)
        {   
            //Drop
            if(inhand.obj.GetComponent<Rigidbody>() != null)
            {
                inhand.obj.GetComponent<Rigidbody>().isKinematic = false;
            }
            foreach (Collider col in inhand.obj.GetComponents<Collider>())
            {
                col.enabled = true;
            }
            inhand.obj = null;
            inhand.data = null;
            isholding = false;
        }

        if(use_input && isholding)
        {
            //When using the object in hand
            use(inhand);
        }
    }

    void use(mounter_n_obj touse)
    {
        if(touse.data is gas_itemdata gas_item_data)
        {
            switch (gas_item_data.gasitem)
            {
                case gas_itemdata.gasitem_category.product:
                    physics_disable(touse.obj);
                    touse.obj.transform.position = holdat.position;
                    touse.obj.transform.SetParent(holdat.transform);
                    Debug.Log("inhand: " + inhand.obj.name);
                    product_checked = true;
                    isholding = true;
                    break;

                case gas_itemdata.gasitem_category.cashier:
                    if (product_checked && looking_At.obj.tag == "cashmachine")
                    {
                        Counter cash_machine = counter.GetComponent<Counter>();
                        cash_machine.counted++;
                        product_checked = false;
                        Destroy(inhand.obj);
                        isholding = false;
                        Debug.Log("Item assigned");
                    }
                    break;

                default:
                    Debug.Log("Default item");
                    break;
            }
        }
        else
        {
            Debug.Log("Not pizza item");
        }
    } 

    public void physics_disable(GameObject topick, bool state = true)
    {
        if(topick.GetComponent<Rigidbody>() != null)
        {
            topick.GetComponent<Rigidbody>().isKinematic = state;
        }
        topick.GetComponent<Collider>().enabled = !state;
    } 
}

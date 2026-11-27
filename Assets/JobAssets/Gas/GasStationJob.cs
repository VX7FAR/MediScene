using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GasStationJob : MonoBehaviour
{
    public struct mounter_n_obj
    {
        public GameObject obj;
        public Itemdata data;
    }

    public mounter_n_obj inhand;
    Vector3 picked_from_position;
    Vector3 picked_from_rotation;
    public mounter_n_obj looking_At;
    public GameObject cam;
    RaycastHit hit;
    float range = 100.0f;
    [SerializeField] Transform holdat;

    public bool isholding = false;
    public bool product_checked = false;
    public GameObject customer_counter;

    PlayerInputManager player_input;
    bool interact;
    bool drop_input;
    bool use_input;
    bool use_held;

    GameObject tofuel;
    bool fuelpressed;

    GameObject holding;

    public gas_station_manager station_manager;

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
        holding = inhand.obj;
        interact = player_input.pick_interact;
        use_input = player_input.use;
        use_held = player_input.use_isPressed;
        drop_input = player_input.drop;

        //BASIC INTERACTION
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null)
            {
                //For normal item
                if (hit.collider.GetComponent<mounter>().itemdata.obj_type == Itemdata.categories.GasStation)
                {
                    looking_At.obj = hit.collider.gameObject;
                    looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                    if (interact && !isholding && looking_At.data.can_pick)
                    {
                        //Pick in hand
                        picked_from_position = looking_At.obj.transform.position;
                        picked_from_rotation = looking_At.obj.transform.localEulerAngles;
                        inhand = looking_At;
                        isholding = true;
                        if (inhand.obj.GetComponent<Rigidbody>() != null)
                        {
                            inhand.obj.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        foreach (Collider col in inhand.obj.GetComponents<Collider>())
                        {
                            col.enabled = false;
                        }
                    }
                    else if (interact && !looking_At.data.can_pick)
                    {
                        //Dont pick but interact
                        use(looking_At);
                    }
                    else if(isholding && use_input)
                    {
                        use(inhand);
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

        if (drop_input && isholding && inhand.obj != null)
        {
            //Drop
            dropitem();
        }

        // if (use_input && isholding)
        // {
        //     //When using the object in hand
        //     use(inhand);
        // }

        if (tofuel != null && use_held && Vector3.Distance(tofuel.GetComponent<gasvehicle_ai>().fuel_tank_position.position, transform.position) < 4.0f)
        {
            Debug.Log(Vector3.Distance(tofuel.GetComponent<gasvehicle_ai>().fuel_tank_position.position, transform.position));
            tofuel.GetComponent<gasvehicle_ai>().is_fueling = true;
        }
        else if (tofuel != null)
        {
            gasvehicle_ai gas_vehicle_script = tofuel.GetComponent<gasvehicle_ai>();
            gas_vehicle_script.is_fueling = false;
            if (gas_vehicle_script.fuel >= gas_vehicle_script.fuel_needed)
            {
                tofuel = null;
            }
        }
    }

    void use(mounter_n_obj touse)
    {
        if (touse.data is gas_itemdata gas_item_data)
        {
            switch (gas_item_data.gasitem)
            {
                case gas_itemdata.gasitem_category.cashier:
                    // if (product_checked && looking_At.obj.tag == "cashmachine")
                    if(inhand.data is gas_itemdata data && data.gasitem == gas_itemdata.gasitem_category.product)
                    {
                        Counter cash_machine = customer_counter.GetComponent<Counter>();
                        cash_machine.counted++;
                        product_checked = false;
                        Destroy(inhand.obj);
                        isholding = false;
                        Debug.Log("Item assigned");
                    }
                    break;

                case gas_itemdata.gasitem_category.nozzle:
                    physics_disable(touse.obj);
                    break;

                case gas_itemdata.gasitem_category.fuel_tank:
                    if (inhand.data is gas_itemdata gasdata)
                    {
                        // Debug.Log(inhand.obj.name);
                        tofuel = touse.obj;
                        // Debug.Log("Fueling started");
                    }
                    break;

                default:
                    Debug.Log("Default item");
                    break;
            }
        }
        else
        {
            Debug.Log("Not gas item");
        }
    }

    public void physics_disable(GameObject topick, bool state = true)
    {
        if (topick.GetComponent<Rigidbody>() != null)
        {
            topick.GetComponent<Rigidbody>().isKinematic = state;
        }
        topick.GetComponent<Collider>().enabled = !state;
    }

    void dropitem()
    {
        if (inhand.data != null && inhand.data is gas_itemdata data)
        {
            switch (data.gasitem)
            {
                case gas_itemdata.gasitem_category.nozzle:
                    inhand.obj.transform.position = picked_from_position;
                    inhand.obj.transform.localEulerAngles = picked_from_rotation;
                    foreach (Collider col in inhand.obj.GetComponents<Collider>())
                    {
                        col.enabled = true;
                    }
                    inhand.obj = null;
                    inhand.data = null;
                    isholding = false;
                    break;

                default:
                    if (inhand.obj.GetComponent<Rigidbody>() != null)
                    {
                        inhand.obj.GetComponent<Rigidbody>().isKinematic = false;
                    }
                    foreach (Collider col in inhand.obj.GetComponents<Collider>())
                    {
                        col.enabled = true;
                    }
                    inhand.obj.transform.SetParent(null);
                    inhand.obj = null;
                    inhand.data = null;
                    isholding = false;
                    break;
            }
        }
    }

    void OnDisable()
    {
        station_manager.enabled = false;
    }
}

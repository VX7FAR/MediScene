using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
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
    [SerializeField] Transform moveto;

    public bool isholding = false;

    [SerializeField] string[] joblist = { "pizza", "mart", "gas", "cook", "warehouse" };

    [SerializeField]InputActionAsset interaction_input;
    InputAction pick_n_interact;
    InputAction use_input;
    InputAction drop_input;

    void OnEnable()
    {
        interaction_input.FindActionMap("Interactions").Enable();
    }


    void Awake()
    {
        pick_n_interact = interaction_input.FindActionMap("Interactions").FindAction("Interact");
        use_input = interaction_input.FindActionMap("Interactions").FindAction("Use");
        drop_input = interaction_input.FindActionMap("Interactions").FindAction("Drop");
    }

    void Start()
    {

    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null && !(hit.collider.GetComponent<mounter>().itemdata is PizzaItemData))
            {
                looking_At.obj = hit.collider.gameObject;
                looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                if (pick_n_interact.WasPressedThisFrame() && !isholding && looking_At.data.can_pick)
                {
                    inhand = looking_At;
                    isholding = true;
                }
                if(pick_n_interact.WasPressedThisFrame() && !looking_At.data.can_pick)
                {
                    UseItem(looking_At);
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
            if(inhand.obj.GetComponent<Rigidbody>() != null)
            {
                inhand.obj.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if(drop_input.WasPressedThisFrame() && isholding && inhand.obj != null)
        {
            inhand.obj.GetComponent<Rigidbody>().isKinematic = false;
            inhand.obj = null;
            inhand.data = null;
            isholding = false;
        }

        if(use_input.WasPressedThisFrame() && isholding)
        {
            UseItem(inhand);
        }
    }

    void UseItem(mounter_n_obj to_use)
    {
        switch (to_use.data.obj_type)
        {
            case Itemdata.categories.capsule:
                gameObject.GetComponent<UseCapsule>().enabled = true;
                break;
            case Itemdata.categories.PizzaDelivery:
                if(to_use.data.obj_name == "Pizzatrigger")
                {
                    gameObject.GetComponent<PizzaDelivery>().enabled = true;
                    gameObject.GetComponent<Interact>().enabled = false;   
                }
                break;
            default:
                Debug.Log("Invalid item to use");
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using JetBrains.Rider.Unity.Editor;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PizzaDelivery : MonoBehaviour
{
    [SerializeField]GameObject job_manager;
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


    [SerializeField]InputActionAsset interaction_input;
    InputAction pick_n_interact;
    InputAction use_input;
    InputAction drop_input;

    public GameObject[] deliver_at;
    public int boxes = 0;
    public bool isdelivering;
    bool new_delivery = false;
    int previous_delivery;
    int delivery_index;

    void OnEnable()
    {
        interaction_input.FindActionMap("Interactions").Enable();
    }

    void OnTriggerEnter(Collider colliding_to)
    {
        if(colliding_to.tag == "DeliverAt" && use_input.WasPressedThisFrame())
        {
            DeliverAccomplished(colliding_to.gameObject);
        }
    }

    void Awake()
    {
        pick_n_interact = interaction_input.FindActionMap("Interactions").FindAction("Interact");
        use_input = interaction_input.FindActionMap("Interactions").FindAction("Use");
        drop_input = interaction_input.FindActionMap("Interactions").FindAction("Drop");
    }

    void Start()
    {
        if (gameObject.GetComponent<Interact>().isholding)
        {
            inhand.obj = gameObject.GetComponent<Interact>().inhand.obj;
            inhand.data = gameObject.GetComponent<Interact>().inhand.data;
            isholding = gameObject.GetComponent<Interact>().isholding;
        }
    }

    void Update()
    {
        //BASIC INTERACTION
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null)
            {
                if(hit.collider.GetComponent<mounter>().itemdata is PizzaItemData)
                {
                    looking_At.obj = hit.collider.gameObject;
                    looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                    if (pick_n_interact.WasPressedThisFrame() && !isholding && looking_At.data.can_pick)
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
                    if(pick_n_interact.WasPressedThisFrame() && !looking_At.data.can_pick)
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
            inhand.obj.transform.position = moveto.transform.position;
            inhand.obj.transform.rotation = moveto.transform.rotation;
        }

        if(drop_input.WasPressedThisFrame() && isholding && inhand.obj != null)
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

        if(use_input.WasPressedThisFrame() && isholding)
        {
            //When using the object in hand
            use(inhand);
        }

        //DELIVERY SYSTEM
        if(boxes > 0 && new_delivery)
        {
            delivery_index = Random.Range(0, deliver_at.Length);
            deliver_at[delivery_index].SetActive(true);
            new_delivery = false;
        }
        
        if(boxes < 1)
        {
            foreach(GameObject obj in deliver_at)
            {
                if(obj != null)
                {
                    obj.SetActive(false);
                }
            }
            isdelivering = false;
            new_delivery = false;
        }
    }

    void use(mounter_n_obj touse)
    {
        if(touse.data is PizzaItemData pizzadata)
        {
            switch (pizzadata.Pizza_Item_Category)
            {
                case PizzaItemData.pizza_category.pizzabox:
                    boxes++;
                    Destroy(touse.obj);
                    inhand.data = null;
                    inhand.obj = null;
                    isholding = false;
                    return;

                case PizzaItemData.pizza_category.scooter:
                    Debug.Log("Go Deliver");
                    isdelivering = boxes > 0 ? true : false;
                    new_delivery = boxes > 0 ? true : false;
                    break;
                
                case PizzaItemData.pizza_category.deliverat:
                    if (isdelivering && boxes != 0)
                    {
                        boxes--;
                        touse.obj.SetActive(false);
                        new_delivery = true;
                    }
                    break;
                
                case PizzaItemData.pizza_category.SpawnBox:
                    if(boxes == 0)
                    {
                        job_manager.GetComponent<jobmanager>().spawn_pizza_box = true;
                    }
                    break;

                default:
                    Debug.Log("Invalid item");
                    break;
            }
        }
        else
        {
            Debug.Log("Not pizza item");
        }
    }

    void DeliverAccomplished(GameObject delivered_At)
    {
        delivered_At.SetActive(false);
        boxes--;
    }
}

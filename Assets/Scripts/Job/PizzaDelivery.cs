using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PizzaDelivery : MonoBehaviour
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


    [SerializeField]InputActionAsset interaction_input;
    InputAction pick_n_interact;
    InputAction use_input;
    InputAction drop_input;

    void OnEnable()
    {
        Debug.Log("Enabled");
        interaction_input.FindActionMap("Interactions").Enable();
    }


    void Awake()
    {
        Debug.Log("Awake()");
        pick_n_interact = interaction_input.FindActionMap("Interactions").FindAction("Interact");
        use_input = interaction_input.FindActionMap("Interactions").FindAction("Use");
        drop_input = interaction_input.FindActionMap("Interactions").FindAction("Drop");
    }

    void Start()
    {
        Debug.Log("Started");
        if (gameObject.GetComponent<Interact>().isholding)
        {
            inhand.obj = gameObject.GetComponent<Interact>().inhand.obj;
            inhand.data = gameObject.GetComponent<Interact>().inhand.data;
            isholding = gameObject.GetComponent<Interact>().isholding;
        }
    }

    void Update()
    {
        if(pick_n_interact.WasPressedThisFrame()){ Debug.Log("Pick n interact");}
        if(drop_input.WasPressedThisFrame()){ Debug.Log("Pick n interact");}
        if(use_input.WasPressedThisFrame()){ Debug.Log("Pick n interact");}

        Debug.Log("Update");
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.collider.GetComponent<mounter>() != null && hit.collider.gameObject != null)
            {
                Debug.Log("Hit smthn");
                if(hit.collider.GetComponent<mounter>().itemdata is PizzaItemData)
                {
                    Debug.Log("Hit a pizza item");
                    looking_At.obj = hit.collider.gameObject;
                    looking_At.data = hit.collider.GetComponent<mounter>().itemdata;
                    if (pick_n_interact.WasPressedThisFrame() && !isholding && looking_At.data.can_pick)
                    {
                        //Pick in hand
                        Debug.Log("Is Pizza Itemm, pickable");
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
                       Debug.Log("Not Picked but interacted");
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
            Debug.Log("Used picked pizza item");
        }
    }

    void use(mounter_n_obj touse)
    {
        if(touse.data is PizzaItemData pizzadata)
        {
            Debug.Log(pizzadata.name);
        }
        else
        {
            Debug.Log("Not pizza item");
        }
    }

    void OnDisable()
    {
        interaction_input.FindActionMap("Interactions").Disable();
        Debug.Log("Interaction script disabled");
    }
}

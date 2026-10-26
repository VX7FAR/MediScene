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

    public mounter_n_obj interacting_with;
    [SerializeField] GameObject cam;
    RaycastHit hit;
    float range = 100.0f;
    [SerializeField] Transform moveto;

    public bool isholding = false;

    [SerializeField] string[] joblist = { "pizza", "mart", "gas", "cook", "warehouse" };

    void Start()
    {

    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (!isholding)
        {
            interacting_with.obj = null;
            interacting_with.data = null;
        }
        //Pick item
        if (Input.GetKeyDown(KeyCode.F) && !isholding)
        {
            if (Physics.Raycast(ray, out hit, range) && hit.collider != null && hit.collider.gameObject.GetComponent<mounter>() != null)
            {
                interacting_with.obj = hit.collider.gameObject;
                interacting_with.data = hit.collider.gameObject.GetComponent<mounter>().itemdata;
                if (interacting_with.obj.GetComponent<Rigidbody>() != null)
                {
                    interacting_with.obj.GetComponent<Rigidbody>().isKinematic = true;
                }
                isholding = true;
            }
        }
        //Drop item
        if (isholding && Input.GetKeyDown(KeyCode.G))
        {
            if (interacting_with.obj.GetComponent<Rigidbody>() != null)
            {
                interacting_with.obj.GetComponent<Rigidbody>().isKinematic = false;
            }
            isholding = false;
            interacting_with.obj = null;
            interacting_with.data = null;
        }

        //UseItem
        if (isholding && Input.GetKeyDown(KeyCode.Mouse0) && interacting_with.obj != null)
        {
            switch (interacting_with.data.obj_type)
            {
                case Itemdata.categories.regular:
                    isholding = false;
                    break;

                case Itemdata.categories.capsule:
                    gameObject.GetComponent<UseCapsule>().enabled = true;
                    isholding = false;
                    Destroy(interacting_with.obj);
                    break;
                case Itemdata.categories.jobtrigger:
                    // string job = joblist[Random.Range(0, joblist.Length)];
                    string job = joblist[0];

                    switch (job)
                    {
                        case "pizza":
                            // gameObject.GetComponent<PizzaDelivery>().enabled = true;
                            Debug.Log("Pizza");
                            break;
                        case "mart":
                            break;
                        case "cook":
                            break;
                        case "gas":
                            break;
                        case "warehouse":
                            break;
                        default:
                            Debug.Log("Invalid job");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        //Position the item
        if (isholding)
        {
            interacting_with.obj.transform.position = moveto.transform.position;
            interacting_with.obj.transform.rotation = moveto.transform.rotation;
        }
    }
}
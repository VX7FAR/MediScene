using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItems : MonoBehaviour
{
    bool isholding = false;
    GameObject interacting_obj;
    Itemdata interacting_data;
    [SerializeField]GameObject cam;

    string[] effect_list = {"rotatecam180", "toxicair", "oversteering"};
    bool capsule;
    public float multiplier = 30.0f;

    void Start()
    {
        capsule = false;
    }

    void Update()
    {

        isholding = gameObject.GetComponent<Interact>().isholding;
        interacting_obj = gameObject.GetComponent<Interact>().interacting_with.obj;
        interacting_data = gameObject.GetComponent<Interact>().interacting_with.data;

        if (interacting_obj != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isholding)
            {
                switch (interacting_data.obj_type)
                {
                    case Itemdata.categories.regular:
                    break;

                    case Itemdata.categories.capsule:
                    capsule = true;
                    break;

                    default:
                    break;
                }
            }
        }
        if (capsule)
        {
            // string effect = effect_list[Random.Range(0, effect_list.Length - 1)];
            string effect = effect_list[1];
            
            if(effect == "rotatecam180")
            {
                cam.transform.Rotate(0f,0f,180f);
                gameObject.GetComponent<FPS>().flipy = -1;
            }
            if(effect == "toxicair")
            {
                gameObject.GetComponent<toxicair>().enabled = true;
            }
            if(effect == "oversteering")
            {
                gameObject.GetComponent<FPS>().mouse_Sen *= multiplier;
                gameObject.GetComponent<FPS>().movement_speed *= multiplier;
            }

            capsule = false;
        }
    }
}

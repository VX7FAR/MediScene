using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UseItems : MonoBehaviour
{
    bool isholding = false;
    GameObject interacting_obj;
    Itemdata interacting_data;
    [SerializeField]GameObject cam;
    [SerializeField]GameObject hueshift;
    [SerializeField]GameObject colorblindness;
    
    string[] effect_list = {"rotatecam180", "toxicair", "oversteering", "flipcontrols", "fixedkey", "lockedinput", "hueshift", "colorblindness"};
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
            string effect = effect_list[7];
            
            if(effect == "rotatecam180")
            {
                cam.transform.Rotate(0f,0f,180f);
                gameObject.GetComponent<FPS>().flipmousey = -1;
            }
            else if(effect == "toxicair")
            {
                gameObject.GetComponent<toxicair>().enabled = true;
            }
            else if(effect == "oversteering")
            {
                gameObject.GetComponent<FPS>().mouse_Sen *= multiplier;
                gameObject.GetComponent<FPS>().movement_speed *= multiplier;
            }
            else if(effect == "flipcontrols")
            {
                gameObject.GetComponent<FPS>().flip = -1.0f;
            }
            else if(effect == "fixedkey")
            {
                gameObject.GetComponent <FPS>().fixkey = true;
            }
            else if(effect == "lockedinput")
            {
                bool blockx = Random.Range(0,2) == 1;
                if (blockx)
                {
                    gameObject.GetComponent<FPS>().lock_Axis.x = 0.0f;
                }
                else
                {
                    gameObject.GetComponent<FPS>().lock_Axis.y = 0.0f;
                }
                Debug.Log("APAKJKDWDAD");
            }
            else if(effect == "hueshift")
            {
                hueshift.SetActive(true);
            }
            else if(effect == "colorblindness")
            {
               colorblindness.SetActive(true); 
            }

            Debug.Log("Used");
            capsule = false;
        }
    }
}

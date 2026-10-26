using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UseCapsule : MonoBehaviour
{
    bool isholding = false;
    GameObject interacting_obj;
    Itemdata interacting_data;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject hueshift;
    [SerializeField] GameObject colorblindness;
    [SerializeField] Material nausea_mat;

    [SerializeField] UniversalRendererData URPrenderdata;
    string target = "FullScreenPassRendererFeature";

    string[] effect_list = { "rotatecam180", "toxicair", "oversteering", "flipcontrols", "fixedkey", "lockedinput", "hueshift", "colorblindness" };
    bool capsule;
    public float multiplier = 30.0f;
    bool used = true;

    void OnEnable()
    {
        setnausea(false);
        capsule = false;
        nausea_mat.SetFloat("_ActivateNausea", 0f);
        isholding = gameObject.GetComponent<Interact>().isholding;
        interacting_obj = gameObject.GetComponent<Interact>().inhand.obj;
        interacting_data = gameObject.GetComponent<Interact>().inhand.data;



        string effect = effect_list[Random.Range(0, effect_list.Length)];
        // string effect = effect_list[6];

        if (effect == "rotatecam180")
        {
            cam.transform.Rotate(0f, 0f, 180f);
            gameObject.GetComponent<FPS>().flipmousey = -1;
            Debug.Log("Rotatecam180");
        }
        else if (effect == "toxicair")
        {
            gameObject.GetComponent<toxicair>().enabled = true;
            Debug.Log("Toxic Air");
        }
        else if (effect == "oversteering")
        {
            gameObject.GetComponent<FPS>().mouse_Sen *= multiplier;
            gameObject.GetComponent<FPS>().movement_speed *= multiplier;
            Debug.Log("Oversteering");
        }
        else if (effect == "flipcontrols")
        {
            gameObject.GetComponent<FPS>().flip = -1.0f;
            Debug.Log("Flip Controls");
        }
        else if (effect == "fixedkey")
        {
            gameObject.GetComponent<FPS>().fixkey = true;
            Debug.Log("Fixed key");
        }
        else if (effect == "lockedinput")
        {
            bool blockx = Random.Range(0, 2) == 1;
            if (blockx)
            {
                gameObject.GetComponent<FPS>().lock_Axis.x = 0.0f;
            }
            else
            {
                gameObject.GetComponent<FPS>().lock_Axis.y = 0.0f;
            }
            Debug.Log("Locked input");
        }
        else if (effect == "hueshift")
        {
            hueshift.SetActive(true);
            setnausea(true);
            Debug.Log("Nausea");
        }
        else if (effect == "colorblindness")
        {
            colorblindness.SetActive(true);
            Debug.Log("Colorblindness");
        }
        gameObject.GetComponent<UseCapsule>().enabled = false;

    }

    void deleteitem()
    {
        Destroy(interacting_obj);
        interacting_obj = null;
        interacting_data = null;
        isholding = false;
    }

    void setnausea(bool set_To)
    {
        foreach (var feature in URPrenderdata.rendererFeatures)
        {
            if (feature != null && feature.name == target)
            {
                feature.SetActive(set_To);
                return;
            }
        }
        Debug.Log("Not found");
    }
}
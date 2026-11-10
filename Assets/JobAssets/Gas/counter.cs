using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class counter : MonoBehaviour
{
    [SerializeField]GameObject playerdata;

    [SerializeField]List<Transform> sort_At;

    public int item_number;
    public bool customer_waiting = false;

    [SerializeField]cashRegister register;
    public int item_cashed = -5;

    GameObject customer;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "NPC")
        {
            customer_waiting = true;
            customer = col.gameObject;
        }
    }

    public void placeitem(GameObject item_giver)
    {
        gasNPC npc = item_giver.GetComponent<gasNPC>();
        List<GameObject> itemlist = item_giver.GetComponent<gasNPC>().inventory;
        item_number = npc.inventory.Count;
        for(int i = 0; i < npc.inventory.Count; i++)
        {
            itemlist[i].transform.SetParent(null);
            itemlist[i].transform.position = sort_At[i].transform.position;
            itemlist[i].GetComponent<BoxCollider>().enabled = true;
            customer_waiting = true;
        }
    }

    void FixedUpdate()
    {
        if (register.itemadded)
        {
            if(item_cashed < 0)
            {
                item_cashed = 0;    
            }
            item_cashed++;
            register.itemadded = false;   
        }
        if(item_cashed == item_number)
        {
            playerdata.GetComponent<playerdata>().money += item_number * 3;
            item_cashed = -5;
            item_number = 0;
            customer.GetComponent<gasNPC>().moveto(customer.GetComponent<gasNPC>().leave.position);
            customer.GetComponent<gasNPC>().isleaving = true;
        }
    }
}

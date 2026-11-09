using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class counter : MonoBehaviour
{
    [SerializeField]List<Transform> sort_At;

    bool customer_waiting = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "NPC")
        {
            customer_waiting = true;
        }
    }

    public void placeitem(GameObject item_giver)
    {
        gasNPC npc = item_giver.GetComponent<gasNPC>();
        List<GameObject> itemlist = item_giver.GetComponent<gasNPC>().inventory;
        for(int i = 0; i < npc.inventory.Count; i++)
        {
            itemlist[i].transform.SetParent(null);
            itemlist[i].transform.position = sort_At[i].transform.position;
        }
    }
}

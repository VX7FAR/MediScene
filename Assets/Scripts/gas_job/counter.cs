using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class counter : MonoBehaviour
{
    [SerializeField]List<Transform> sort_At;

    public void placeitem(GameObject item_giver)
    {
        npc npc = item_giver.GetComponent<npc>();
        List<GameObject> itemlist = item_giver.GetComponent<npc>().inventory;
        for(int i = 0; i < npc.inventory.Count; i++)
        {
            itemlist[i].transform.SetParent(null);
            itemlist[i].transform.position = sort_At[i].transform.position;
        }
    }
}

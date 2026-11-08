using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelf_action : MonoBehaviour
{
    public List<GameObject> itemlist;
    bool take;

    public bool takeitem(GameObject giveto)
    {
        take = Random.Range(1,11) > 2; 
        // take = true;
        npc giveto_npc = giveto.GetComponent<npc>();
        if (take)
        {
            GameObject item_given = Instantiate(itemlist[Random.Range(0, itemlist.Count)], giveto_npc.hold_At.transform.position, Quaternion.identity);
            giveto_npc.inventory.Add(item_given);
            item_given.transform.SetParent(giveto_npc.hold_At);
            return true;
        }

        return false;
    }
}

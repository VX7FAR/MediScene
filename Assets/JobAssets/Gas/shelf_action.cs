using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelf_action : MonoBehaviour
{
    public List<GameObject> itemlist;
    bool take;
    bool takemore = true;
    bool firstfilled;

    public bool takeitem(GameObject giveto)
    {
        take = Random.Range(1,11) > 2; 
        // take = true;
        gasNPC giveto_npc = giveto.GetComponent<gasNPC>();
        if (take)
        {
            while(giveto_npc.inventory.Count < 4 && takemore)
            {
                GameObject item_given = Instantiate(itemlist[Random.Range(0, itemlist.Count)], giveto_npc.hold_At.transform.position, Quaternion.identity);
                giveto_npc.inventory.Add(item_given);
                if (firstfilled)
                {
                    item_given.transform.position = giveto.transform.position;
                    item_given.transform.SetParent(giveto.transform);
                }
                else
                {
                item_given.transform.SetParent(giveto_npc.hold_At);
                }
                takemore = Random.Range(0,2) == 0;
                firstfilled = true;
            }
            return true;
        }

        return false;
    }
}

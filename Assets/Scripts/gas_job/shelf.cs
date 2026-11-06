using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelf : MonoBehaviour
{
    public GameObject[] itemlist;
    public void take_item(GameObject giveto)
    {
        // NPC_AI npc = giveto.GetComponent<NPC_AI>();
        // GameObject given;
        // given = itemlist[Random.Range(0, itemlist.Length)];

        // if (npc.buylist.Count == 0)
        // {
        //     given = Instantiate(given, npc.hold_at.transform.position, Quaternion.identity);
        //     given.transform.SetParent(npc.hold_at.transform);
        //     given.transform.position = npc.hold_at.transform.position;
        //     npc.buylist.Add(given);
        //     Debug.Log("given: " + given.name);
        // }
        // else
        // {
        //     given = Instantiate(given, giveto.transform.position, Quaternion.identity);
        //     npc.buylist.Add(given);
        //     given.transform.SetParent(giveto.transform);
        // }
        // npc.itemobught = true;
    }
}

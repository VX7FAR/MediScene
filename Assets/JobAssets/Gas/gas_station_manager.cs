using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gas_station_manager : MonoBehaviour
{
    GameObject[] npc_spawned = new GameObject[2];
    public GameObject npc_prefab;
    public Transform spawn_At;
    bool allspawned;

    void Start()
    {
        npc_spawned[0] = Instantiate(npc_prefab, spawn_At.position, Quaternion.identity);
        npc_spawned[0].SetActive(true);
        StartCoroutine(init_npc());
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (allspawned)
            {
                GameObject npc = npc_spawned[i];
                if (npc.GetComponent<gasNPC>().kys)
                {
                    Destroy(npc_spawned[i]);
                    npc_spawned[i] = Instantiate(npc_prefab, spawn_At.position, Quaternion.identity);
                    npc_spawned[i].SetActive(true);

                }
                else if (npc == null)
                {
                    npc_spawned[i] = Instantiate(npc_prefab, spawn_At.position, Quaternion.identity);
                    npc_spawned[i].SetActive(true);

                }
            }
        }
    }

    IEnumerator init_npc()
    {
        yield return new WaitForSeconds(2);
        npc_spawned[1] = Instantiate(npc_prefab, spawn_At.position, Quaternion.identity);
        npc_spawned[1].SetActive(true);
        allspawned = true;
    }
}

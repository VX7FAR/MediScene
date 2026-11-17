using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
REQUIREMENTS:
    Gas_station_manager - gas_station_manager script, < npc_prefab, spawn_At>
    npc - attach gasnpc, tag = NPC, rigidbody, capsule collider, navmeshagent, < shelf_itemlist[], hold_At, shelf_location, leave_location, wait_At, place_At, counter >    //
    shelftrigger - box collider w trigger, tag = shopshelf      //
    npc spawn point - empty gameobject      //
    leave point - one or more, bx collider w trigger, tag = dnsnpc      //
    wait point - empty gameobject           //
    cash machine - mounter = cashier, tag = cashmachine     //
    spawn_At - empty gameobject     //
    counter - counter       //
*/

public class gas_station_manager : MonoBehaviour
{
    [Header("NPC")]
    GameObject[] npc_spawned = new GameObject[2];
    public GameObject npc_prefab;
    public Transform[] spawn_At;
    bool allnpcspawned;

    public GameObject[] car_prefab;
    public Transform[] car_spawnat = new Transform[2];
    GameObject[] car_spawned = new GameObject[2];
    bool allcarspawned;

    void Start()
    {
        // npc_spawned[0] = Instantiate(npc_prefab, decide_vector(spawn_At), Quaternion.identity);
        // npc_spawned[0].SetActive(true);
        // StartCoroutine(init_npc());

        // car_spawned[0] = Instantiate(car_prefab[0], car_spawnat[0].transform.position, Quaternion.identity);
        // car_spawned[1] = Instantiate(car_prefab[1], car_spawnat[1].transform.position, Quaternion.identity);
        StartCoroutine(init_Car());
    }

    void Update()
    {
        // for (int i = 0; i < 2; i++)
        // {
        //     if (allnpcspawned)
        //     {
        //         GameObject npc = npc_spawned[i];
        //         if (npc.GetComponent<gasNPC>().kys)
        //         {
        //             Destroy(npc_spawned[i]);
        //             npc_spawned[i] = Instantiate(npc_prefab, decide_vector(spawn_At), Quaternion.identity);
        //             npc_spawned[i].SetActive(true);

        //         }
        //         else if (npc == null)
        //         {
        //             npc_spawned[i] = Instantiate(npc_prefab, decide_vector(spawn_At), Quaternion.identity);
        //             npc_spawned[i].SetActive(true);

        //         }
        //     }
        // }


// Car shouldd not fill unless stopped
        if (allcarspawned)
        {
            for(int i = 0; i < 2; i++)
            {
                if(car_spawned[i].GetComponent<gasvehicle_ai>().kys)
                {
                    Destroy(car_spawned[i]);
                    GameObject carselect = car_prefab[Random.Range(0, car_prefab.Length)];
                    car_spawned[i] = Instantiate(carselect, car_spawnat[i].position, Quaternion.identity);
                    car_spawned[i].SetActive(true);
                }
            }
        }
    }

    Vector3 decide_vector(Transform[] spawn_pos)
    {
        int at = UnityEngine.Random.Range(0, spawn_pos.Length);
        return spawn_pos[at].position;
    }

    // IEnumerator init_npc()
    // {
    //     yield return new WaitForSeconds(2);
    //     npc_spawned[1] = Instantiate(npc_prefab, decide_vector(spawn_At), Quaternion.identity);
    //     npc_spawned[1].SetActive(true);
    //     allnpcspawned = true;
    // }

    IEnumerator init_Car()
    {
        int spawn = Random.Range(0, car_prefab.Length);
        car_spawned[0] = Instantiate(car_prefab[spawn], car_spawnat[0].transform.position, Quaternion.identity);
        car_spawned[0].SetActive(true);
        yield return new WaitForSeconds(2);
        spawn = Random.Range(0, car_prefab.Length);
        car_spawned[1] = Instantiate(car_prefab[spawn], car_spawnat[1].transform.position, Quaternion.identity);
        allcarspawned = true;
        car_spawned[1].SetActive(true);
    }
}

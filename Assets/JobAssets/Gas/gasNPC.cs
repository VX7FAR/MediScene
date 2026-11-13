using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*
*/

public class gasNPC : MonoBehaviour
{
    public enum Action
    {
        to_shelf, to_counter, waiting, taking_item, to_waiting, to_leaving
    }
    public Action action;
    NavMeshAgent agent;
    public bool toshelf;
    public bool kys;

    [Header("Inventory and Shelf")]
    public GameObject[] shelf_itemlist;
    public List<GameObject> inventory;
    public int needs;
    public Transform hold_At;
    public bool itemtaken;

    [Header("Waypoints")]
    public Transform[] shelf_location;
    public Transform[] leave_location; public bool leave;
    public Transform wait_At;
    bool isleaving;

    [Header("Counter n  Customer")]
    public Transform[] place_At;
    public GameObject counter;
    public bool customer_Waiting;
    GameObject customer;
    Counter counter_script;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "counter" && !customer_Waiting)
        {
            if (inventory.Count == 1)
            {
                inventory[0].transform.position = place_At[0].position;
                change_item_physics(inventory[0], false, false);
                inventory[0].transform.SetParent(null);
                inventory[0].GetComponent<Collider>().enabled = true;
                inventory.RemoveAt(0);
            }
            else
            {
                for (int i = inventory.Count - 1; i > -1; i--)
                {
                    inventory[i].transform.position = place_At[i].position;
                    change_item_physics(inventory[i], false, false);
                    inventory[i].transform.SetParent(null);
                    inventory[i].GetComponent<Collider>().enabled = true;
                    inventory.RemoveAt(i);
                }
            }
        }
        if (other.tag == "shopshelf")
        {
            action = Action.taking_item;
        }
        if(other.tag == "dnsnpc")
        {
            kys = true;
        }

    }

    void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        action = Action.to_shelf;
        pick_random_and_move(shelf_location);
    }

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        action = Action.to_shelf;
        counter_script = counter.GetComponent<Counter>();
        pick_random_and_move(shelf_location);
    }

    void Update()
    {
        customer_Waiting = counter_script.is_customer_wawiting;
        customer = counter_script.customer;

        if (action == Action.to_shelf && !itemtaken)
        {
            toshelf = true;
        }

        if (action == Action.taking_item && !itemtaken)
        {
            StartCoroutine(shelf_interaction());
        }

        if (action == Action.to_counter && itemtaken)
        {
            if (customer_Waiting && customer != null && customer != gameObject)
            {
                moveto(wait_At.position);
                action = Action.waiting;
            }
        }

        if (action == Action.waiting && customer == null && !customer_Waiting && itemtaken)
        {
            moveto(counter.transform.position);
        }

        if (action == Action.to_leaving && !isleaving)
        {
            pick_random_and_move(leave_location);
            isleaving = true;
        }
    }

    public void moveto(Vector3 to)
    {
        agent.SetDestination(to);
    }

    public void pick_random_and_move(Transform[] to)
    {
        int move_to = UnityEngine.Random.Range(0, to.Length);
        moveto(to[move_to].position);
    }

    public void change_item_physics(GameObject obj, bool state, bool rigidbody_change = true)
    {
        if (obj.GetComponent<Rigidbody>() != null && rigidbody_change)
        {
            obj.GetComponent<Rigidbody>().isKinematic = !state;
        }
        if (obj.GetComponent<BoxCollider>() != null)
        {
            obj.GetComponent<BoxCollider>().enabled = state;
        }
        if (obj.GetComponent<CapsuleCollider>() != null)
        {
            obj.GetComponent<CapsuleCollider>().enabled = state;
        }
    }

    IEnumerator shelf_interaction()
    {
        itemtaken = true;
            yield return new WaitForSeconds(2);
            // bool takeitem = UnityEngine.Random.Range(0, 10) < 8;
            bool takeitem = true;
            action = Action.taking_item;

            if (!takeitem)
            {
                pick_random_and_move(leave_location);
                action = Action.to_leaving;
                // doingaction = true;
                itemtaken = true;
                Debug.Log("Do not take item, leave");
            }
            else
            {
                int item_to_take = UnityEngine.Random.Range(0, 3);
                inventory.Clear();
                for (int i = 0; i <= item_to_take; i++)
                {
                    GameObject item_to_spawn = shelf_itemlist[UnityEngine.Random.Range(0, shelf_itemlist.Length)];
                    GameObject new_item = Instantiate(item_to_spawn, hold_At.transform.position, Quaternion.identity);
                    change_item_physics(new_item, false);
                    if (inventory.Count == 0)
                    {
                        new_item.transform.position = hold_At.transform.position;
                        new_item.transform.SetParent(hold_At.transform);
                    }
                    else
                    {
                        new_item.transform.position = gameObject.transform.position;
                        new_item.transform.SetParent(gameObject.transform);
                    }
                    inventory.Add(new_item);
                    action = Action.to_counter;
                    moveto(counter.transform.position);
                }
                needs = inventory.Count;
                itemtaken = true;
            }
    }
}

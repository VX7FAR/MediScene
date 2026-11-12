using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject customer;
    public bool is_customer_wawiting;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            // gasNPC npc = other.GetComponent<gasNPC>();
            customer = other.gameObject;
            is_customer_wawiting = true;
            Debug.Log("Customer here");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Destroy(customer);
            customer = null;
            is_customer_wawiting = false;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            customer.GetComponent<gasNPC>().action = gasNPC.Action.to_leaving;
            customer = null;
            is_customer_wawiting = false;
        }
    }
}

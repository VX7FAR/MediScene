using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject customer;
    public bool is_customer_wawiting;
    public int customer_need = 0;
    public int counted = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            // gasNPC npc = other.GetComponent<gasNPC>();
            customer = other.gameObject;
            is_customer_wawiting = true;
            customer_need = customer.GetComponent<gasNPC>().needs;
            customer_need = customer_need > 3? 3 : customer_need;
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
        if(customer_need == counted && customer != null)
        {
            customer.GetComponent<gasNPC>().action = gasNPC.Action.to_leaving;
            counted = 0;
            is_customer_wawiting = false;
            customer = null;
        }
    }
}

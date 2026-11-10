using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class gasNPC : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]Transform navigator;
    [SerializeField]Transform[] shelf;
    [SerializeField]GameObject counter;
    public Transform leave;

    public List<GameObject> inventory;
    public Transform hold_At;

    bool delaystarted;

    public bool isleaving;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "shelfnavigator" && !isleaving)
        {
            moveto(shelf[Random.Range(0, shelf.Length)].position);
        }
        else if(other.tag == "shopshelf" && !delaystarted && !isleaving)
        {
            //Call shelf picker here
            Debug.Log("Shelf pick");
            delaystarted = true;
            StartCoroutine(takeitem(other.gameObject));
        }
        else if(other.tag == "counter" && !isleaving)
        {
            Debug.Log("Checkout");
            other.GetComponent<counter>().placeitem(gameObject);
            moveto(transform.position);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveto(navigator.position);
    }


    IEnumerator takeitem(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        if (obj.GetComponent<shelf_action>().takeitem(gameObject))
        {
            moveto(counter.transform.position);
        }
        else
        {
            moveto(leave.position);
        }
        moveto(counter.transform.position);

        if(inventory.Count > 3)
        {
            inventory.RemoveRange(3, inventory.Count - 3);
        }
    }

    public void moveto(Vector3 position)
    {
        agent.SetDestination(position);
    }
}

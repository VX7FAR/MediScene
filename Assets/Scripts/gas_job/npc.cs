using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class npc : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]Transform navigator;
    [SerializeField]Transform[] shelf;
    [SerializeField]Transform counter;
    [SerializeField]Transform leave;

    public List<GameObject> inventory;
    public Transform hold_At;

    bool delaystarted;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "shelfnavigator")
        {
            moveto(shelf[Random.Range(0, shelf.Length)].position);
        }
        else if(other.tag == "shopshelf" && !delaystarted)
        {
            //Call shelf picker here
            Debug.Log("Shelf pick");
            delaystarted = true;
            StartCoroutine(takeitem(other.gameObject));
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveto(navigator.position);
    }

    void Update()
    {
        
    }

    IEnumerator takeitem(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        if (obj.GetComponent<shelf_action>().takeitem(gameObject))
        {
            moveto(counter.position);
        }
        else
        {
            moveto(leave.position);
        }
        moveto(counter.position);
    }

    public void moveto(Vector3 position)
    {
        agent.SetDestination(position);
    }
}

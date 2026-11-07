using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]Transform navigator;
    [SerializeField]Transform[] shelf;
    [SerializeField]Transform counter;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "shelfnavigator")
        {
            moveto(shelf[Random.Range(0, shelf.Length)].position);
        }
        else if(other.tag == "shopshelf")
        {
            //Call shelf picker here
            Debug.Log("Shelf pick");
            StartCoroutine(takeitem());
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

    IEnumerator takeitem()
    {
        yield return new WaitForSeconds(2);
        moveto(counter.position);
    }

    public void moveto(Vector3 position)
    {
        agent.SetDestination(position);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class gasNPC : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]Transform navigator;
    [SerializeField]Transform[] shelf;
    [SerializeField]GameObject counter;
    public Transform[] leave;

    public Transform[] spawn_At;

    public List<GameObject> inventory;
    public Transform hold_At;

    bool delaystarted;

    public bool isleaving;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "shelfnavigator" && !isleaving)
        {
            moveto(shelf[UnityEngine.Random.Range(0, shelf.Length)].position);
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
        else if(isleaving && other.tag == "dns_npc")
        {
            GameObject obj = Instantiate(gameObject, spawn_At[UnityEngine.Random.Range(0,2)].position, Quaternion.identity);
            obj.SetActive(false);
            obj.SetActive(true);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveto(navigator.position);
    }

    void OnEnable()
    {
        delaystarted = false;
        isleaving = false;
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
            moveto(leave[0].position);
        }
        moveto(counter.transform.position);

        if(inventory.Count > 3)
        {
            inventory.RemoveRange(3, inventory.Count - 3);
        }
        if(inventory.Count == 0)
        {
            moveto(leave[UnityEngine.Random.Range(0, leave.Count())].position);
            isleaving = true;
        }
    }

    public void moveto(Vector3 position)
    {
        agent.SetDestination(position);
    }
}

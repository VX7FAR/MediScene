using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasvehicle_ai : MonoBehaviour
{
    public Transform[] start_At = new Transform[2];

    [SerializeField] float speed;
    float speedhold;
    GameObject at;

    bool move;

    Transform move_to;
    GameObject next;
    Vector3 direction;

    bool takedecision = false;

    Ray frontray;
    RaycastHit hit;

    public bool waitforfuel;
    public bool is_fueling;
    public Transform fuel_tank_position;
    public float fuel;
    public float fuel_needed;
    public float fueling_speed;

    public bool kys;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Waypoint>() != null)
        {
            at = other.gameObject;
            takedecision = true;
            if (other.tag == "gas_fuelwait")
            {
                waitforfuel = true;
            }
            if(other.tag == "dns_car" && fuel >= fuel_needed)
            {
                kys = true;   
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Waypoint>() != null && at == other.gameObject)
        {
            takedecision = false;
        }
    }

    void OnEnable()
    {
        fuel_needed = Random.Range(3, 8);
        fuel = Random.Range(1, fuel_needed);
    }

    void Start()
    {
        speedhold = speed;
    }

    void Update()
    {
        if (waitforfuel && fuel < fuel_needed)
        {
            // speed = 0;
            
        }
        else
        {
            if (takedecision)
            {
                List<GameObject> change_to = at.GetComponent<Waypoint>().next_waypoint;
                if (change_to.Count != 0 && change_to != null)
                {
                    next = change_to[UnityEngine.Random.Range(0, change_to.Count)];
                    move_to = next.transform;
                    // Debug.Log("Move to: " + next.name);
                    takedecision = false;
                    move = true;
                }
                else
                {
                    move = false;
                }
            }


            frontray = new Ray(gameObject.transform.position, gameObject.transform.forward);
            if (Physics.Raycast(frontray, out hit, 8f))
            {
                move = hit.collider.tag == "car" && hit.distance < 15f ? false : true;

            }
            else
            {
                move = true;
            }


            if (move && move_to != null)
            {
                Vector3 direction = move_to.transform.position - transform.position;
                Quaternion target_rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, 5f * Time.deltaTime);

                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

        }

        if (is_fueling && fuel < fuel_needed && waitforfuel) 
        {
            fuel += fueling_speed * Time.deltaTime;
        }
    }

    IEnumerator waititngforfuel()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Waiting");
        speed = speedhold;
    }
}

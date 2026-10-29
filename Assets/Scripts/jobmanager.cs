using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class jobmanager : MonoBehaviour
{
    [SerializeField]GameObject player;
    
    [SerializeField]GameObject pizzabox;
    [SerializeField]Transform spawn_pizza_At;
    public bool spawn_pizza_box = false;
    

    void Update()
    {
        if (spawn_pizza_box)
        {
            int amount = Random.Range(1, 9);
            for(int i=0;i <= amount; i++)
            {
                GameObject new_box = Instantiate(pizzabox, spawn_pizza_At.position, Quaternion.identity);
            }
            spawn_pizza_box = false;
        }
    }
}

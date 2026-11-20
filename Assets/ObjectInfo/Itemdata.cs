using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Itemdata", menuName ="Itemdata/baseinfo")]
public class Itemdata : ScriptableObject
{
    public enum categories
    {
        regular, capsule, rollerskates, jobtrigger, PizzaDelivery, GasStation, house_door
    }
    public string obj_name;
    public bool can_pick;
    public categories obj_type;
}

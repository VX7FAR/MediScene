using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Itemdata", menuName ="Itemdata/baseinfo")]
public class Itemdata : ScriptableObject
{
    public enum categories
    {
        regular, capsule, jobtrigger, PizzaDelivery, Mart, GasStation, Cook, Warehouse
    }
    public string obj_name;
    public bool can_pick;
    public categories obj_type;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PizzaDeliveryItem", menuName ="Itemdata/PizzaItem")]
public class PizzaItemData : Itemdata
{
    public enum pizza_category
    {
        pizzabox, scooter, finishjob, deliverat, takeitem
    }
    public pizza_category Pizza_Item_Category;
}

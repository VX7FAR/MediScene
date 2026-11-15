using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GasJobItem", menuName ="Itemdata/GasItem")]
public class gas_itemdata : Itemdata
{
    public enum gasitem_category
    {
        product, cashier, bell, nozzle, fuel_tank
    }

    public gasitem_category gasitem;
}

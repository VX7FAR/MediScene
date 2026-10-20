using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Itemdata", menuName ="Itemdata/baseinfo")]
public class Itemdata : ScriptableObject
{
    public enum categories
    {
        regular, capsule 
    }
    public string obj_name;
    public bool can_pick;
    public categories obj_type;
    public void use()
    {
        switch (obj_type)
        {
            case categories.capsule:
                Debug.Log("Effect given");
                break;

            case categories.regular:
                Debug.Log("Used");
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicData", menuName = "Item Data/Basic")]
public class BaseItem : ScriptableObject
{
    public string obj_name;
    public bool canpick;
    public Vector3 pickup_size;
    public Vector3 original_size;
    public GameObject prefab;

    void OEnable()
    {
        original_size = prefab.transform.localScale;
    }
}

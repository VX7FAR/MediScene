using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynigh : MonoBehaviour
{
    [SerializeField]float speed;

    void Start()
    {   
        transform.eulerAngles = new Vector3(22.3f,200.3f,211.8f);
    }
    void Update()
    {
        gameObject.transform.Rotate(speed * Time.deltaTime, 0f,0f);
    }
}

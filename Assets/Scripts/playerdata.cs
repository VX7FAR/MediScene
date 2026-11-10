using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playerdata : MonoBehaviour
{
    public int money;
    [SerializeField]TextMeshProUGUI moneytext;

    void Update()
    {
        moneytext.text = money.ToString();
    }
}

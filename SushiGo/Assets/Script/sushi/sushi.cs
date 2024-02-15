using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class sushi : MonoBehaviour
{
    public int coinCount;
    public TMP_Text coinText;

    private void Update()
    {
        coinText.text = ": " +  coinCount.ToString();
    }
}

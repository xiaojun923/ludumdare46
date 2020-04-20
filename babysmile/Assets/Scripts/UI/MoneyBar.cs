using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour
{
    public Text data;
    public float temMoney;
    public float smooth = 5f;
    public void Start()
    {
        temMoney = 0;
    }

    public void Update()
    {
        float money = BabySmileManager.GetMoney();

        temMoney = Mathf.Lerp(temMoney, money, smooth * Time.deltaTime);
        data.text = Mathf.Round(temMoney).ToString();
    }
}

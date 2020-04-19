using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoActor : MonoBehaviour
{
    private int itemid;
    private int price;

    void Start()
    {
        
    }

    void Update()
    {
        if(price <= BabySmileManager.GetMoney())
        {
            BabySmileManager.SetItemState(itemid, 1);
        }
        else
        {
            BabySmileManager.SetItemState(itemid, 2);
        }
    }
}

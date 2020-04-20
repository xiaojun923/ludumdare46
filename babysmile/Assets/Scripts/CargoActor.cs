using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoActor : MonoBehaviour
{
    public int price;

    void Start()
    {
        
    }

    void Update()
    {
        int itemid = GetComponent<SceneItem>().id;
        if (price > BabySmileManager.GetMoney())
        {
            BabySmileManager.SetItemState(itemid, 1);
            //表现层改状态为缺货
        }
        else
        {
            BabySmileManager.SetItemState(itemid, 2);
            //表现层改状态为有货
        }
    }
}

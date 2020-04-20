using System.Collections;
using System.Collections.Generic;
using LD46;
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
            GetComponent<SceneItem>().Status = 1;
        }
        else
        {
            BabySmileManager.SetItemState(itemid, 2);
            //表现层改状态为有货
            GetComponent<SceneItem>().Status = 2;
        }
    }
}

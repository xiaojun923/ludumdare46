using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerActor : MonoBehaviour
{    
    private int itemid;
    private float acumTime = 0;
    private float waterTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //花盆状态 生苗1枯苗2生花3枯花4成花5
        int flowerStat = BabySmileManager.GetItemState(itemid);
        if ( flowerStat == 1 || flowerStat == 3)
        {
            acumTime += Time.deltaTime;
            if(acumTime > waterTime)
            {
                BabySmileManager.SetItemState(itemid, flowerStat + 1);
            }
        }
    }
}

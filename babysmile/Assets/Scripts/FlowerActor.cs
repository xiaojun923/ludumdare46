using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerActor : MonoBehaviour
{
    public float acumTime = 0;
    public float waterTime = 40;
    void Start()
    {
        
    }

    void Update()
    {
        //花盆状态 生苗1枯苗2生花3枯花4成花5
        int itemid = GetComponent<SceneItem>().id;
        int flowerStat = BabySmileManager.GetItemState(itemid);
        int placeid = BabySmileManager.GetTableType(BabySmileManager.GetItemTableID(itemid));

        /*
        if (flowerStat == 2 || flowerStat == 4 || flowerStat == 5)
        {
            //枯苗和枯花和成花都是锁定在阳台上不可拾取的，只能对其进行浇水或者采摘操作
            //BabySmileManager.SetPickable(itemid, false);
        }*/
        if (placeid == 3 && (flowerStat == 1 || flowerStat == 3))
        {
            //生苗和生花可以拾取，但只有在阳台才会生长
            acumTime += Time.deltaTime;
            if (acumTime > waterTime)
            {
                acumTime = 0;
                //BabySmileManager.SetPickable(itemid, true);
                BabySmileManager.SetItemState(itemid, flowerStat + 1);
                //表现层改状态为枯苗/枯花
            }
        }
    }
}

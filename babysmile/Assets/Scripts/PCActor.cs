using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCActor : MonoBehaviour
{
    public static float maxTime = 10;
    public static float minTime = 5;
    public float acumTime = 0;
    public float waitTime = 0;
    public float jobTh = 8;
    public float waitTh = 10;
    
    void Start()
    {
        //游戏开始就要有个工作出现
        int itemid = GetComponent<SceneItem>().id;
        jobTh = Random.Range(minTime, maxTime);
        BabySmileManager.SetItemState(itemid, 2);
        //表现层改状态为邮件
    }

    void Update()
    {
        int itemid = GetComponent<SceneItem>().id;
        int pcStat = BabySmileManager.GetItemState(itemid);
        if(pcStat == 1)
        {
            //随机产生邮件
            acumTime += Time.deltaTime;
            if(acumTime > jobTh)
            {
                acumTime = 0;
                jobTh = Random.Range(minTime, maxTime);
                BabySmileManager.SetItemState(itemid, 2);
                //表现层改状态为邮件
            }
        }
        else if(pcStat == 2)
        {
            //长时间没人理会变成关机
            waitTime += Time.deltaTime;
            if(waitTime > waitTh)
            {
                waitTime = 0;
                BabySmileManager.SetItemState(itemid, 1);
                //表现层改状态为关机
            }
        }
    }
}

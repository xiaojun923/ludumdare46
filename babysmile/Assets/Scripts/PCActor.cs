using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCActor : MonoBehaviour
{
    public static float maxTime = 10;
    public static float minTime = 5;
    private int itemid;
    //关机状态积累时间
    private float acumTime = 0;
    //邮件状态积累时间
    private float waitTime = 0;
    private float jobTh = 0.0f;
    private float waitTh = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //电脑状态 关机1有邮件2开机3
        int pcStat = BabySmileManager.GetItemState(itemid);
        if(pcStat==1)
        {
            acumTime += Time.deltaTime;
            if(acumTime > jobTh)
            {
                acumTime = 0;
                jobTh = Random.Range(minTime, maxTime);
                BabySmileManager.SetItemState(itemid, 2);
            }
        }
        else if(pcStat == 2)
        {
            waitTime += Time.deltaTime;
            if(waitTime > waitTh)
            {
                waitTime = 0;
                BabySmileManager.SetItemState(itemid, 1);
            }
        }
    }
}

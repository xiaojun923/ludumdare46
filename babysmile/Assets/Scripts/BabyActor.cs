using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyActor : MonoBehaviour
{
    public static float maxTime = 10;
    public static float minTime = 5;
    public static int taskNum = 2;
    public float acumTime = 0;
    public float cryTime = 0;
    public float hurtTh = 3.0f;
    public float cryTh = 0.0f;

    void Start()
    {
        
    
        
    }

    void Update()
    {
        int itemid = GetComponent<SceneItem>().id;
        int babyStat = BabySmileManager.GetItemState(itemid);
        if (babyStat == 1)
        {
            //平静
            acumTime += Time.deltaTime;
            if(acumTime > cryTh)
            {
                //提需求，开始哭
                acumTime = 0;
                cryTh = Random.Range(minTime, maxTime);
                BabySmileManager.AddTask(Random.Range(1, taskNum + 1));
                BabySmileManager.SetItemState(itemid, 2);
                //表现层改状态为有需求，婴儿哭
            }
        }
        else
        {
            //婴儿在哭
            cryTime += Time.deltaTime;
            if(cryTime > hurtTh)
            {
                cryTime = 0;
                BabySmileManager.ChangeHealth(-1);
                //表现层健康条减小
            }
        }
    }
}

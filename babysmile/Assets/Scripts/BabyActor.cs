using System.Collections;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class BabyActor : MonoBehaviour
{
    public static int taskNum = 2;
    public static float maxTime = 10;
    public static float minTime = 5;
    public static float hurtTh = 3;
    public static float cryTh = 5;

    public float acumTime = 0;
    public float cryTime = 0;

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
                SceneControl.Instance.InteractItems[itemid].GetComponent<SceneItem>().Status = 2;
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

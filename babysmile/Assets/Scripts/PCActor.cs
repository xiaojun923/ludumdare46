using System.Collections;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class PCActor : MonoBehaviour
{
    public static float maxTime = 10;
    public static float minTime = 5;
    public float acumTime = 0;
    public float waitTime = 0;
    public float jobTh = 5;
    public float waitTh = 10;

    public UITips WorkTips;


    public void OnEnable()
    {
        MessageSystem.AddListener(MessageType.SuccessfulInteract, OnSuccessfulInteract);
    }

    public void OnDisable()
    {
        MessageSystem.RemoveListener(MessageType.SuccessfulInteract, OnSuccessfulInteract);

    }

    public void OnSuccessfulInteract(object msg)
    {
        int id = (int) msg;

        if (id == 5)
        {
            Debug.Log("End Job");
            OnEndJob();
        }
    }

    void Start()
    {
        //游戏开始就要有个工作出现
        int itemid = GetComponent<SceneItem>().id;
        jobTh = Random.Range(minTime, maxTime);
        BabySmileManager.SetItemState(itemid, 2);
        //表现层改状态为邮件
        SceneControl.Instance.InteractItems[itemid].GetComponent<SceneItem>().Status = 2;
    }

    void Update()
    {
        int itemid = GetComponent<SceneItem>().id;
        int pcStat = BabySmileManager.GetItemState(itemid);
        if(pcStat == 1)
        {
            //随机产生邮件
            acumTime += Time.deltaTime;
            waitTime = 0;
            if (acumTime > jobTh)
            {
                acumTime = 0;
                jobTh = Random.Range(minTime, maxTime);
                BabySmileManager.SetItemState(itemid, 2);
                //表现层改状态为邮件
                SceneControl.Instance.InteractItems[itemid].GetComponent<SceneItem>().Status = 2;
                OnShowJob();
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

                SceneControl.Instance.InteractItems[itemid].GetComponent<SceneItem>().Status = 1;
                OnEndJob();
            }
        }
    }

    public void OnShowJob()
    {
        WorkTips.OnPlay();
    }

    public void OnEndJob()
    {
        WorkTips.OnStop();
    }
}

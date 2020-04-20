using System.Collections;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class BabyActor : MonoBehaviour
{
    public int taskNum = 2;
    public float maxTime = 10;
    public float minTime = 5;
    public float hurtTh = 3;
    public float cryTh = 5;

    public float acumTime = 0;
    public float cryTime = 0;

    [Header("Visual")] 
    public float ShowDurOnFinishTask = 5f;

    public UITips HealthUI;
    public UITips ToyUI;
    public UITips MilkUI;

    public void OnEnable()
    {
        MessageSystem.AddListener(MessageType.FinishTask,OnFinishTask);
    }
    public void OnDisable()
    {
        MessageSystem.RemoveListener(MessageType.FinishTask, OnFinishTask);
    }

    public void OnFinishTask(object msg)
    {
        int taskType = (int)msg;

        Debug.Log($"Finish Task{taskType}!!");

        HealthUI.ShowFor(ShowDurOnFinishTask);

        if (taskType == 1)
        {
            MilkUI.OnStop();
        }else if (taskType == 2)
        {
            ToyUI.OnStop();
        }
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

                int taskType = Random.Range(1, taskNum + 1);
                BabySmileManager.AddTask(taskType);
                BabySmileManager.SetItemState(itemid, 2);
                
                OnStartTask(taskType);
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

    public void OnStartTask(int taskType)
    {
        if (taskType == 1)
        {
            MilkUI.OnPlay();
        }
        else if (taskType == 2)
        {
            ToyUI.OnPlay();
        }
    }
}

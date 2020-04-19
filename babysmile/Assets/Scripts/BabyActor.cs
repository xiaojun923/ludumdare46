using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyActor : MonoBehaviour
{
    public static float maxTime = 10;
    public static float minTime = 5;
    public static int taskNum = 2;
    //婴儿状态 正常1哭2
    private int babyStat;
    //喂奶1 玩玩具2
    private List<int> taskList = new List<int>();
    //正常状态积累时间
    private float acumTime = 0;
    private float cryTime = 0;
    private float hurtTh = 3.0f;
    private float cryTh = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(babyStat == 1)
        {
            //婴儿正常
            acumTime += Time.deltaTime;
            if(acumTime > cryTh)
            {
                //提需求，开始哭
                acumTime = 0;
                cryTh = Random.Range(minTime, maxTime);
                int rtask = Random.Range(1, taskNum + 1);
                taskList.Add(rtask);
                babyStat = 2;
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
            }
            if(taskList.Count == 0)
            {
                babyStat = 1;
            }
        }
    }

    public void finish(int finid)
    {
        for(int i=0;i<taskList.Count;i++)
        {
            if(taskList[i]== finid)
            {
                taskList.RemoveAt(i);
                break;
            }
        }
    }
}

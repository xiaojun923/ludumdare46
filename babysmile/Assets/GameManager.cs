using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //元指令数组：每一项都是Tuple<int,int>类型，第一个int代表指令类型，第二个是指令参数

    //指令类型：指令参数
    //1：拾取物品：物品ID 
    //2：拾取新产生的物品：物品Type
    //3：将手里物品放到桌子上：桌子ID
    //4：将手里物品放置在地上 
    //5：手里物品消失 
    //6：物品切换到下一状态 物品ID
    //7：开始计时条时间 计时条Type
    //8：婴儿健康值变化 新值
    //9：婴儿快乐值变化 新值
    //10：钱包值变化 新值

    // 当前table上的物品序号，没有物品为0
    public List<int> inTableList = new List<int>();
    public List<Tuple<int, int>> deepOptList = new List<Tuple<int, int>>();
    public Dictionary<Tuple<int, int>, int> deepEftDic = new Dictionary<Tuple<int, int>, int>();

    List<Tuple<int, int>> getDeepOpt(int inTableID, int inHandID)
    {
        Tuple<int, int> key = new Tuple<int, int>(inHandID, inTableID);
        if (deepEftDic.ContainsKey(key))
        {
            
        }
    }

    List<Tuple<int,int>> ShortOpt(int tableID, int inHandID)
    {
        List<Tuple<int, int>> orderSet = new List<Tuple<int, int>>();
        int inTableID = inTableList[tableID];
        if(inTableID == 0 && inHandID == 0)
        {
            //桌子空，手里空 没操作
        }
        else if(inTableID == 0 && inHandID > 0)
        {
            //桌子空，手里有 将东西放到桌子上
            orderSet.Add(new Tuple<int, int>(3, tableID));
        }
        else if(inTableID > 0 && inHandID == 0)
        {
            //桌子有，手里空：拾取东西到手里
            orderSet.Add(new Tuple<int, int>(1, inTableID));
        }
        else if(inTableID > 0 && inHandID > 0)
        {
            //桌子有，手里有：将手里东西施加到桌子上东西上，返回施加结果
            orderSet.AddRange(getDeepOpt(inTableID, inHandID));
        }
        return orderSet;
    }

    void LongOpt(int inHandID, int tarID)
    {





    }

}

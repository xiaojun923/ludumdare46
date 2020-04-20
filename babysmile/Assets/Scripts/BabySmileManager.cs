using System;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class BabySmileManager : MonoBehaviour
{

    void Start()
    {
        itemEffect = new Dictionary<Tuple<int, int, int, int>, EffectData>();
        addEffect(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        addEffect(new List<int> { 0, 0, 2, 2, 0, 1, 3, 0, 0, 0, -5, 0 });
        addEffect(new List<int> { 3, 1, 4, 1, 0, 2, 0, 5, 2, 0, 0, 0 });
        addEffect(new List<int> { 4, 2, 1, 1, 1, 1, 0, 5, 0, 0, 0, 5 });
        addEffect(new List<int> { 4, 2, 1, 2, 1, 2, 0, 5, 0, 1, 0, 2 });
        addEffect(new List<int> { 0, 0, 5, 2, 0, 1, 6, 0, 0, 0, -40, 0 });
        addEffect(new List<int> { 6, 1, 1, 1, 0, 1, 0, 10, 0, 0, 0, 10 });
        addEffect(new List<int> { 6, 1, 1, 2, 0, 2, 0, 10, 0, 2, 0, 5 });
        addEffect(new List<int> { 0, 0, 8, 2, 0, 1, 9, 0, 0, 0, -25, 0 });
        addEffect(new List<int> { 10, 2, 9, 2, 1, 3, 0, 5, 3, 0, 0, 0 });
        addEffect(new List<int> { 10, 2, 9, 4, 1, 5, 0, 5, 3, 0, 0, 0 });
        addEffect(new List<int> { 0, 0, 9, 5, 0, 1, 0, 6, 0, 0, 30, 0 });
        addEffect(new List<int> { 0, 0, 7, 2, 0, 3, 0, 0, 0, 0, 0, 0 });
        addEffect(new List<int> { 0, 0, 7, 3, 0, 1, 0, 15, 0, 0, 30, 0 });

        typeInit = new Dictionary<int, Tuple<int, bool>>();
        addTypeInit(1, 1, false);
        addTypeInit(2, 1, false);
        addTypeInit(3, 1, true);
        addTypeInit(4, 1, true);
        addTypeInit(5, 1, false);
        addTypeInit(6, 1, true);
        addTypeInit(7, 1, false);
        addTypeInit(8, 1, false);
        addTypeInit(9, 1, true);
        addTypeInit(10,1, true);

        roleList = new List<RoleData>();
        roleList.Add(new RoleData(0));
        roleList.Add(new RoleData(1));
        roleList.Add(new RoleData(2));

        itemList = new List<ItemData>();
        addItem(0);
        addItem(1);
        addItem(2);
        addItem(5);
        addItem(8);
        addItem(7);
        addItem(4);
        addItem(10);

        tableList = new List<TableData>();
        addTable(0);
        addTable(2);
        addTable(3);
        addTable(1);
        addTable(1);
        addTable(1);

        putItemOnTable(6, 1);

        taskList = new List<int>();

    }

    void Update()
    {
        //检测婴儿数值，为空时结束游戏
        if (healthVal == 0)
        {
            // Game Over
        }
    }
    
    // 只读信息:
    private static Dictionary<Tuple<int, int, int, int>, EffectData> itemEffect = new Dictionary<Tuple<int, int, int, int>, EffectData>();
    private static Dictionary<int, Tuple<int, bool>> typeInit = new Dictionary<int, Tuple<int, bool>>();

    // 实时更新信息
    private static int moneyVal = 0;
    private static int healthVal = 0;
    private static List<RoleData> roleList = new List<RoleData>();
    private static List<ItemData> itemList = new List<ItemData>();
    private static List<TableData> tableList = new List<TableData>();
    private static List<int> taskList = new List<int>();

    class EffectData
    {
        //基本的
        public EffectData(List<int> param)
        {
            postStat = new Tuple<int, int>(param[0], param[1]);
            newType = param[2];
            costTime = param[3];
            holdType = param[4];
            taskType = param[5];
            moneyAdd = param[6];
            healthAdd = param[7];
        }
        public Tuple<int, int> postStat;
        public int newType;
        public int costTime;
        public int holdType;
        public int taskType;
        public int moneyAdd;
        public int healthAdd;
    }

    class RoleData
    {
        //从1开始编号
        public RoleData(int id)
        {
            roleID = id;
            handItemID = 0;
        }
        public int roleID { get; set; } = 0;
        public int handItemID { get; set; } = 0;
    }

    class ItemData
    {
        //从1开始编号
        public ItemData(int id,int type)
        {
            itemID = id;
            typeID = type;
            if (typeInit.ContainsKey(typeID))
            {
                crtStat = typeInit[typeID].Item1;
                pickFlag = typeInit[typeID].Item2;
            }
            else
            {
                crtStat = 0;
                pickFlag = false;
            }
        }
        public int itemID { get; set; } = 0;
        public int typeID { get; set; } = 0;
        public bool pickFlag { get; set; } = false;
        public int crtStat { get; set; } = 0;
    }

    class TableData
    {
        //从1开始编号
        public TableData(int tid,int pid)
        {
            tableID = tid;
            typeID = pid;
            holdItemID = 0;
        }
        public int tableID { get; set; } = 0;
        public int typeID { get; set; } = 0;
        public int holdItemID { get; set; } = 0;
    }

    private void addEffect(List<int> param)
    {
        Tuple<int, int, int, int> ekey = new Tuple<int, int, int, int>(param[0], param[1], param[2], param[3]);
        EffectData evalue = new EffectData(param.GetRange(4, 8));
        itemEffect.Add(ekey, evalue);
    }

    private void addTypeInit(int tyid,int stid,bool pic)
    {
        typeInit.Add(tyid, new Tuple<int, bool>(stid,pic));
    }

    private void addItem(int tyid)
    {
        itemList.Add(new ItemData(itemList.Count, tyid));
    }

    private void addTable(int tyid)
    {
        tableList.Add(new TableData(tableList.Count, tyid));
    }

    private static void putItemOnTable(int itemid,int tableid)
    {
        tableList[tableid].holdItemID = itemid;
    }

    private static List<Tuple<int, int>> runEffect(EffectData effect,RoleData role,ItemData src,ItemData tar)
    {
        List<Tuple<int, int>> orderSet = new List<Tuple<int, int>>();
        if(src != null)
        {
            src.crtStat = effect.postStat.Item1;
            if (src.crtStat > 0)
            {
                orderSet.Add(new Tuple<int, int>(4, src.itemID));
            }
            else
            { 
                orderSet.Add(new Tuple<int, int>(3, src.itemID));
            }
        }
        if(tar!=null)
        {
            tar.crtStat = effect.postStat.Item2;
            if (tar.crtStat > 0)
            {
                orderSet.Add(new Tuple<int, int>(4, tar.itemID));
            }
            else
            {
                orderSet.Add(new Tuple<int, int>(3, tar.itemID));
            }
        }
        if (effect.taskType > 0)
        {
            bool flag = false;
            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i] == effect.taskType)
                {
                    taskList.RemoveAt(i);
                    if(taskList.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            if(flag)
            {
                //婴儿止哭
                itemList[1].crtStat = 1;
                orderSet.Add(new Tuple<int, int>(4, 1));
            }
        }
        if (effect.newType > 0)
        {
            int new_id = itemList.Count;
            itemList.Add(new ItemData(new_id, effect.newType));
            role.handItemID = new_id;
            orderSet.Add(new Tuple<int, int>(2, effect.newType));
        }
        healthVal += effect.healthAdd;
        moneyVal += effect.moneyAdd;
        return orderSet;
    }

    private static List<Tuple<int, int>> itemOrder(bool finish, RoleData role, ItemData tar, ItemData src = null)
    {
        List<Tuple<int, int>> resultSet = new List<Tuple<int, int>>();

        if (tar.pickFlag && src == null)
        {
            //拾取目标物体
            role.handItemID = tar.itemID;
            resultSet.Add(new Tuple<int, int>(1, 0));
            return resultSet;
        }
        int t1 = (src == null) ? 0 : src.typeID;
        int s1 = (src == null) ? 0 : src.crtStat;
        int t2 = tar.typeID;
        int s2 = tar.crtStat;
        if (itemEffect.ContainsKey(new Tuple<int, int ,int, int>(t1,s1,t2,s2)))
        {
            EffectData effect = itemEffect[new Tuple<int, int, int, int>(t1, s1, t2, s2)];
            if ((effect.holdType == 0) || ((effect.holdType > 0) && effect.holdType == tableList[GetItemTableID(tar.itemID)].typeID))
            {
                if (effect.costTime > 0 && !finish)
                {
                    //开启目标物体时间条
                    resultSet.Add(new Tuple<int, int>(7, effect.costTime));
                }
                else
                {
                    //操作目标物体
                    resultSet.AddRange(runEffect(effect, role, null, tar));
                }
            }
        }

        return resultSet;
    }

    public static int GetTableItemID(int tableid)
    {
        return tableList[tableid].holdItemID;
    }

    public static int GetItemTableID(int itemid)
    {
        foreach (TableData tb in tableList)
        {
            if(tb.holdItemID == itemid)
            {
                return tb.tableID;
            }
        }
        return 0;
    }

    public static int GetItemState(int itemid)
    {
        return itemList[itemid].crtStat;
    }

    public static int GetTableType(int tableid)
    {
        return tableList[tableid].typeID;
    }

    public static void SetItemState(int itemid,int postStat)
    {
        itemList[itemid].crtStat = postStat;
    }

    public static void SetRoleHand(int roleid,int skillid)
    {
        roleList[roleid].handItemID = skillid;
    }

    public static void SetPickable(int itemid, bool flag)
    {
        itemList[itemid].pickFlag = flag;
    }

    public static void ChangeHealth(int addVal)
    {
        healthVal += addVal;
    }

    public static void ChangeGold(int addVal)
    {
        moneyVal += addVal;
    }

    public static int GetHealth()
    {
        return healthVal;
    }

    public static int GetMoney()
    {
        return moneyVal;
    }

    public static void AddTask(int rtask)
    {
        taskList.Add(rtask);
    }
    
    public static List<Tuple<int, int>> InteractItem(int roleid, int itemid)
    {
        int src_id = roleList[roleid].handItemID;
        int tar_id = itemid;
        if(src_id > 0)
        {
            return itemOrder(false, roleList[roleid], itemList[src_id],itemList[tar_id]);
        }
        else
        {
            return itemOrder(false, roleList[roleid], itemList[tar_id]);
        }
    }

    public static List<Tuple<int, int>> MissionComplete(int roleid, int itemid)
    {
        int src_id = roleList[roleid].handItemID;
        int tar_id = itemid;
        if (src_id > 0)
        {
            return itemOrder(true, roleList[roleid], itemList[src_id], itemList[tar_id]);
        }
        else
        {
            return itemOrder(true, roleList[roleid], itemList[tar_id]);
        }
    }

    public static List<Tuple<int, int>> InteractTable(int roleid, int tableid)
    {
        List<Tuple<int, int>> finalSet = new List<Tuple<int, int>>();
        if (tableList[tableid].holdItemID == 0 && roleList[roleid].handItemID > 0)
        {
            tableList[tableid].holdItemID = roleList[roleid].handItemID;
            roleList[roleid].handItemID = 0;
            finalSet.Add(new Tuple<int, int>(5, 0));
        }
        return finalSet;
    }

}


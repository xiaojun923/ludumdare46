using System;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class BabySmileManager : MonoBehaviour
{
    void Start()
    {
        //初始化所有的静态类型
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
    private static Dictionary<int, int> typeStat = new Dictionary<int, int>();
    private static Dictionary<int, bool> typePick = new Dictionary<int, bool>();
    private static List<Tuple<int, int, int, int>> Cargo = new List<Tuple<int, int, int, int>>();

    // 实时更新信息
    private static int moneyVal = 0;
    private static int healthVal = 0;
    private static List<RoleData> roleList = new List<RoleData>();
    private static List<ItemData> itemList = new List<ItemData>();
    private static List<TableData> tableList = new List<TableData>();

    class EffectData
    {
        public int holdID;
        public int costtime;
        public int newType;
        public int taskid;
        public Tuple<int, int> poststat;
        public int moneyAdd;
        public int healthAdd;
    }

    class RoleData
    {
        //手里所持物品的itemID
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
            if (typeStat.ContainsKey(typeID)) crtStat = typeStat[typeID];
            else crtStat = 1;
            if (typePick.ContainsKey(typeID)) pickflag = typePick[typeID];
            else pickflag = false;
        }
        public int itemID { get; set; } = 0;
        public int typeID { get; set; } = 0;
        public bool pickflag { get; set; } = false;
        public int crtStat { get; set; } = 0;
    }

    class TableData
    {
        //从1开始编号
        public int tableID { get; set; } = 0;
        public int holdItemID { get; set; } = 0;
    }

    private static List<Tuple<int, int>> runEffect(EffectData effect,RoleData role,ItemData src,ItemData tar)
    {
        List<Tuple<int, int>> orderSet = new List<Tuple<int, int>>();
        if(src!=null)
        {
            src.crtStat = effect.poststat.Item1;
            if (src.crtStat > 0) orderSet.Add(new Tuple<int, int>(4, src.itemID));
            else orderSet.Add(new Tuple<int, int>(3, src.itemID));
        }
        if(tar!=null)
        {
            tar.crtStat = effect.poststat.Item2;
            if (tar.crtStat > 0) orderSet.Add(new Tuple<int, int>(4, tar.itemID));
            else orderSet.Add(new Tuple<int, int>(3, tar.itemID));
        }
        if (effect.taskid > 0) orderSet.Add(new Tuple<int, int>(8, effect.taskid));
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

        if (tar.pickflag && src == null)
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
            if ((effect.holdID == 0) || ((effect.holdID > 0) && tableList[effect.holdID].holdItemID == tar.itemID))
            {
                if (effect.costtime > 0 && !finish)
                {
                    //开启目标物体时间条
                    resultSet.Add(new Tuple<int, int>(7, effect.costtime));
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

    public static int GetItemState(int itemid)
    {
        return itemList[itemid].crtStat;
    }

    public static void SetItemState(int itemid,int postStat)
    {
        itemList[itemid].crtStat = postStat;
    }

    public static void SetRoleHand(int roleid,int skillid)
    {
        roleList[roleid].handItemID = skillid;
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


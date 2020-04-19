using System;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class BabySmileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private static List<RoleData> roleList = new List<RoleData>();
    private static List<ItemData> itemList = new List<ItemData>();
    private static List<TableData> tableList = new List<TableData>();
    private static Dictionary<Tuple<int, int, int, int>, EffectData> item2itemEffect = new Dictionary<Tuple<int, int, int, int>, EffectData>();
    private static Dictionary<Tuple<int, int>, EffectData> empty2itemEffect = new Dictionary<Tuple<int, int>, EffectData>();


    class EffectData
    {
        public Tuple<int, int> postStat;
        public bool holdFlag;
        public int holdTableID;
    }

    class RoleData : IComparable
    {
        //手里所持物品的itemID
        public int roleID { get; set; } = 0;
        public int handItemID { get; set; } = 0;

        public int CompareTo(object obj)
        {
            return roleID.CompareTo(((RoleData)obj).roleID);
        }
    }

    class ItemData : IComparable
    {
        //从1开始编号
        public int itemID { get; set; } = 0;
        public int typeID { get; set; } = 0;
        public int crtStat { get; set; } = 0;
        public bool pickflag { get; set; }=false;
        public int CompareTo(object obj)
        {
            return itemID.CompareTo(((ItemData)obj).itemID);
        }
    }

    class TableData : IComparable
    {
        //从1开始编号
        public int tableID { get; set; } = 0;
        public int holdItemID { get; set; } = 0;

        public int CompareTo(object obj)
        {
            return tableID.CompareTo(((TableData)obj).tableID);
        }
    }

    private static List<Tuple<int, int>> item2item(ItemData item_1, ItemData item_2)
    {
        List<Tuple<int, int>> finalList = new List<Tuple<int, int>>();
        Tuple<int, int, int, int> check = new Tuple<int, int, int, int>(item_1.typeID, item_1.crtStat, item_2.typeID, item_2.crtStat);
        if(item2itemEffect.ContainsKey(check))
        {
            EffectData effect = item2itemEffect[check];
            if ((!effect.holdFlag) || (effect.holdFlag && tableList[effect.holdTableID].holdItemID == item_2.itemID))
            {
                item_1.crtStat = effect.postStat.Item1;
                if (item_1.crtStat > 0) finalList.Add(new Tuple<int, int>(4, item_1.itemID));
                else finalList.Add(new Tuple<int, int>(3, item_1.itemID));
                item_2.crtStat = effect.postStat.Item2;
                if (item_2.crtStat > 0) finalList.Add(new Tuple<int, int>(4, item_2.itemID));
                else finalList.Add(new Tuple<int, int>(3, item_2.itemID));
            }
        }
        return finalList;
    }

    private static List<Tuple<int, int>> empty2item(ItemData target)
    {
        List<Tuple<int, int>> finalList = new List<Tuple<int, int>>();
        if(target.pickflag)
        {

        }
        else
        {
            Tuple<int, int> check = new Tuple<int, int>(target.typeID, target.crtStat);
            if (empty2itemEffect.ContainsKey(check))
            {
                EffectData effect = empty2itemEffect[check];
            }
        }
        return finalList;
    }


    public static int GetTableItemID(int tableid)
    {
        return tableList[tableid].holdItemID;
    }

    public static int GetItemState(int itemid)
    {
        return itemList[itemid].crtStat;
    }

    public static List<Tuple<int, int>> InteractItem(int roleid, int itemid)
    {
        List <Tuple<int, int>> finalSet = new List<Tuple<int, int>>();

        int id_1 = roleList[roleid].handItemID;
        int id_2 = itemid;
        if(id_1 > 0 && id_2 > 0)
        {
            // 手里物体1和目标物体2交互
            finalSet.AddRange(item2item(itemList[id_1],itemList[id_2]));
        }
        else if (id_1 == 0 && id_2 > 0)
        {
            // 空手对物体2操作
            finalSet.AddRange(empty2item(itemList[id_2]));
        }
        return finalSet;
    }

    public static List<Tuple<int, int>> InteractTable(int roleid, int tableid)
    {
        List<Tuple<int, int>> finalSet = new List<Tuple<int, int>>();



        return finalSet;
    }

    public static List<Tuple<int, int>> MissionComplete(int itemid)
    {
        List<Tuple<int, int>> finalSet = new List<Tuple<int, int>>();



        return finalSet;
    }

}


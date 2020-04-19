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

    private static List<int> inTableList = new List<int>();

    private static Dictionary<Tuple<int, int>, OrderSet> itemOrderMapping = new Dictionary<Tuple<int, int>, OrderSet>();

    class OrderSet
    {
        List<Tuple<int, int>> orderList;

        public List<Tuple<int, int>> Order
        {
            get => orderList;
        }

    }



    private void executeOrder(Tuple<int, int> item)
    {

    }

    public static int GetTableItemID(int tableid)
    {
        return 0;
    }

    public static int GetItemState(int itemid)
    {
        return 0;
    }

    public static List<Tuple<int, int>> InteractItem(int itemid)
    {
        return new List<Tuple<int, int>>();
    }

    public static List<Tuple<int, int>> InteractTable(int tableid)
    {
        return new List<Tuple<int, int>>();
    }


}


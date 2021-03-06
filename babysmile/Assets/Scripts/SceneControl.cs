﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LD46
{
    public class SceneControl
    {
        private static SceneControl _instance;
        public static SceneControl Instance => _instance = _instance ?? new SceneControl();

        public Dictionary<int, GameObject> InteractItems = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> InteractTables = new Dictionary<int, GameObject>();
        
        private Dictionary<int, string> SpawnItemPath = new Dictionary<int, string>
        {
            {3, "MilkBucket"},
            {6, "BearToy"},
            {9, "Flower"},
        };

        private int _nextSpawnItemId = 12;

        public Tuple<int, SceneItemType> UpdateInteractTarget(GameObject player)
        {
            var component = player.transform.Find("InteractTrigger").GetComponent<CharacterTriggerInteract>();
            
            var item = component.GetNearestItem(player)?.GetComponent<SceneItem>();
            var table = component.GetNearestTable(player)?.GetComponent<SceneItem>();

            SceneItem target = null;
            if (table != null && item != null)
            {
                int itemOnTable = BabySmileManager.GetTableItemID(table.id);
                target = itemOnTable == item.id ? item : table;
            }
            else if (table != null)
            {
                target = table;
            }
            else if (item != null)
            {
                target = item;
            }

            var character = player.GetComponent<CharacterControl>();
            int prevId = character.InteractTarget;
            var prevType = character.InteractType;
            var dict = prevType == SceneItemType.Item ? InteractItems : InteractTables;
            var prevObj = dict.ContainsKey(prevId) ? dict[prevId] : null;
            var prevItem = prevObj != null ? prevObj.GetComponent<SceneItem>() : null;

            if (target != prevItem)
            {
                if (target != null)
                {
                    target.SetInteract(true);
                }

                if (prevItem != null)
                {
                    prevItem.SetInteract(false);
                }

                return target == null
                    ? new Tuple<int, SceneItemType>(0, SceneItemType.Item)
                    : new Tuple<int, SceneItemType>(target.id, target.type);
            }

            return new Tuple<int, SceneItemType>(prevId, prevType);
        }

        public void PlayerInteractTap(GameObject player, int target, SceneItemType type)
        {
            Debug.Log($"CharacterTap - {player.name}, {target}");
            int role = player.GetComponent<CharacterControl>().roleId;
            
            var commands = type == SceneItemType.Table
                ? BabySmileManager.InteractTable(role, target)
                : BabySmileManager.InteractItem(role, target);

            foreach (var (cmd, data) in commands)
            {
                if (cmd != (int) DataCommand.StartCast)
                {
                    ProcessCmd(cmd, player, target, data);
                }
            }
        }

        public void PlayerInteractHold(GameObject player, int target, bool enable)
        {
            Debug.Log($"CharacterHold:{enable} - {player.name}");
            
            int role = player.GetComponent<CharacterControl>().roleId;
            if (enable)
            {
                var commands = BabySmileManager.InteractItem(role, target);
                foreach (var (cmd, data) in commands)
                {
                    if (cmd == (int) DataCommand.StartCast)
                    {
                        PlayerStartCast(player, target, data);
                    }
                }
            }
            else
            {
                var item = InteractItems[target].GetComponent<SceneItem>();
                if (!item.HasTimerRunning)
                {
                    var commands = BabySmileManager.MissionComplete(role, target);
                    foreach (var (cmd, data) in commands)
                    {
                        ProcessCmd(cmd, player, target, data);
                    }
                }
                item.ClearTimer();
            }
        }

        private void ProcessCmd(int cmd, GameObject player, int target, int data)
        {
            switch ((DataCommand)cmd)
            {
                case DataCommand.PickItem:
                    PlayerPickUpItem(player, target);
                    break;
                case DataCommand.SpawnItem:
                    PlayerHandSpawnItem(player, data);
                    break;
                case DataCommand.DestroyItem:
                    SceneDestroyItem(data);
                    break;
                case DataCommand.UpdateItemStatus:
                    SceneUpdateItemStatus(data);
                    break;
                case DataCommand.PutOnTable:
                    PlayerPutItemOnTable(player, data, target);
                    break;
                case DataCommand.PutOnFloor:
                    PlayerPutItemOnFloor(player, data);
                    break;
                case DataCommand.StartCast:
                    PlayerStartCast(player, target, data);
                    break;
            }

            MessageSystem.SendMessage(MessageType.SuccessfulInteract, target);
        }

        private void PlayerPickUpItem(GameObject player, int itemId)
        {
            if (!InteractItems.ContainsKey(itemId))
            {
                Debug.LogError($"场景GameObject不存在：{itemId}");
                return;
            }
            var itemGo = InteractItems[itemId];

            var t = player.transform.Find("ItemAnchor");

            itemGo.transform.parent = t;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localRotation = Quaternion.identity;
            itemGo.GetComponent<SceneItem>().ShowHint(false);

            player.GetComponent<CharacterControl>().InHandId = itemId;
        }

        private void PlayerHandSpawnItem(GameObject player, int configId)
        {
            string prefabName = SpawnItemPath[configId];
            var obj = Resources.Load<GameObject>($"RuntimeSpawn/{prefabName}");
            if (obj == null)
            {
                Debug.LogError("Prefab路径不存在，实例化失败" + $"RuntimeSpawn/{prefabName}");
                return;
            }


            
            var t = player.transform.Find("ItemAnchor");
            
            var itemGo = Object.Instantiate(obj, t, false);
            itemGo.tag = "InteractItem";
            itemGo.transform.parent = t;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localRotation = Quaternion.identity;
            itemGo.GetComponent<SceneItem>().ShowHint(false);
            itemGo.GetComponent<SceneItem>().id = _nextSpawnItemId;
            player.GetComponent<CharacterControl>().InHandId = _nextSpawnItemId;

            _nextSpawnItemId++;
        }

        private void SceneDestroyItem(int itemId)
        {
            if (InteractItems.ContainsKey(itemId))
            {
                var item = InteractItems[itemId];
                InteractItems.Remove(itemId);
                Object.Destroy(item);
            }

            if (InteractTables.ContainsKey(itemId))
            {
                var item = InteractTables[itemId];
                InteractTables.Remove(itemId);
                Object.Destroy(item);
            }
        }

        private void SceneUpdateItemStatus(int itemId)
        {
            int status = BabySmileManager.GetItemState(itemId);
            var go = SceneControl.Instance.InteractItems[itemId];
            var item = go.GetComponent<SceneItem>();
            item.Status = status;
        }

        private void PlayerPutItemOnTable(GameObject player, int itemId, int tableId)
        {
            if (!InteractItems.ContainsKey(itemId))
            {
                Debug.LogError($"场景GameObject不存在Item：{itemId}");
                return;
            }
            if (!InteractTables.ContainsKey(tableId))
            {
                Debug.LogError($"场景GameObject不存在Table：{tableId}");
                return;
            }
            
            var itemGo = InteractItems[itemId];
            var tableGo = InteractTables[tableId];
            
            var t = tableGo.transform.Find("ItemAnchor");
            if (t == null)
            {
                Debug.LogError($"桌子对象缺少挂点：{tableGo.name}");
                return;
            }
            
            itemGo.transform.parent = t;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localRotation = Quaternion.identity;
            itemGo.GetComponent<SceneItem>().ShowHint(false);
            
            player.GetComponent<CharacterControl>().InHandId = 0;
        }
        
        private void PlayerPutItemOnFloor(GameObject player, int itemId)
        {
            if (!InteractItems.ContainsKey(itemId))
            {
                Debug.LogError($"场景GameObject不存在：{itemId}");
                return;
            }
            var itemGo = InteractItems[itemId];
            
            itemGo.transform.parent = null;

            player.GetComponent<CharacterControl>().InHandId = 0;
        }

        private void PlayerStartCast(GameObject player, int itemId, float duration)
        {
            if (!InteractItems.ContainsKey(itemId))
            {
                Debug.LogError($"场景GameObject不存在：{itemId}");
                return;
            }
            var itemGo = InteractItems[itemId];
            var item = itemGo.GetComponent<SceneItem>();
            item.SetTimer(duration);
        }
    }
}

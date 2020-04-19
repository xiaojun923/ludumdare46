using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class SceneControl
    {
        private static SceneControl _instance;
        public static SceneControl Instance => _instance = _instance ?? new SceneControl();

        public Dictionary<int, GameObject> InteractItems = new Dictionary<int, GameObject>();

        public int UpdateInteractTarget(GameObject player)
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
            
            int prevId = player.GetComponent<CharacterControl>().InteractTarget;
            var prevObj = InteractItems.ContainsKey(prevId) ? InteractItems[prevId] : null;
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

                return target == null ? 0 : target.id;
            }

            return prevId;
        }

        public void PlayerInteractTap(GameObject player, int target)
        {
            Debug.Log($"CharacterTap - {player.name}, {target}");
            
            var commands = IsTable(target)
                ? BabySmileManager.InteractTable(target)
                : BabySmileManager.InteractItem(target);

            foreach (var (cmd, data) in commands)
            {
                ProcessCmd(cmd, player, target, data);
            }
        }

        public void PlayerInteractHold(GameObject player, int target, bool enable)
        {
            Debug.Log($"CharacterHold:{enable} - {player.name}");

            if (enable)
            {
                var commands = BabySmileManager.InteractItem(target);
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
                // var commands = BabySmileManager.MissionComplete(target);
                var commands = new List<Tuple<int, int>>();
                foreach (var (cmd, data) in commands)
                {
                    ProcessCmd(cmd, player, target, data);
                }
            }
        }

        private bool IsTable(int id)
        {
            if (!InteractItems.ContainsKey(id))
            {
                return false;
            }
            return InteractItems[id].GetComponent<SceneItem>().type == SceneItemType.Table;
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
                    PlayerPutItemOnFloor(player, target);
                    break;
                case DataCommand.StartCast:
                    PlayerStartCast(player, target, data);
                    break;
            }
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
        }

        private void PlayerHandSpawnItem(GameObject player, int configId)
        {
            
        }

        private void SceneDestroyItem(int itemId)
        {
            
        }

        private void SceneUpdateItemStatus(int itemId)
        {
            
        }

        private void PlayerPutItemOnTable(GameObject player, int itemId, int tableId)
        {
            
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
        }

        private void PlayerStartCast(GameObject player, int itemId, float duration)
        {
            
        }
    }
}

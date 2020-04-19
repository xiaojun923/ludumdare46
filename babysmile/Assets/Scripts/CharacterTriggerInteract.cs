using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class CharacterTriggerInteract : MonoBehaviour
    {
        private List<GameObject> _items = new List<GameObject>();
        private List<GameObject> _tables = new List<GameObject>();

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                var si = other.gameObject.GetComponent<SceneItem>();
                if (si.type == SceneItemType.Item)
                {
                    if (!_items.Contains(other.gameObject))
                    {
                        _items.Add(other.gameObject);
                    }
                }
                else if (si.type == SceneItemType.Table)
                {
                    if (!_tables.Contains(other.gameObject))
                    {
                        _tables.Add(other.gameObject);                        
                    }
                }
                else
                {
                    Debug.LogError("InteractItem对象类型错误");
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                var si = other.gameObject.GetComponent<SceneItem>();
                if (si.type == SceneItemType.Item)
                {
                    _items.Remove(other.gameObject);
                }
                else if (si.type == SceneItemType.Table)
                {
                    _tables.Remove(other.gameObject);
                }
                else
                {
                    Debug.LogError("InteractItem对象类型错误");
                }
            }
        }

        private GameObject GetNearestElement(List<GameObject> list, GameObject player, int excludeId = 0)
        {
            float dMin = Single.MaxValue;
            int iMin = -1;
            bool needRefresh = false;
            
            for (var i = 0; i < list.Count; i++)
            {
                var go = list[i];
                if (go == null)
                {
                    needRefresh = true;
                    continue;
                }
                int id = go.GetComponent<SceneItem>().id;
                if (excludeId > 0 && id == excludeId)
                {
                    continue;
                }
                float d = Vector3.Distance(player.transform.position, go.transform.position);
                if (d < dMin)
                {
                    dMin = d;
                    iMin = i;
                }
            }

            if (needRefresh)
            {
                RefreshItemList();
            }

            return iMin >= 0 ? list[iMin] : null;
        }

        public GameObject GetNearestItem(GameObject player)
        {
            int exclude = player.GetComponent<CharacterControl>().InHandId;
            return GetNearestElement(_items, player, exclude);
        }

        public GameObject GetNearestTable(GameObject player)
        {
            return GetNearestElement(_tables, player);
        }

        public void RefreshItemList()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    _items.RemoveAt(i);
                }
            }
        }
    }
}

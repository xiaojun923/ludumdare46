using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class CharacterTriggerInteract : MonoBehaviour
    {
        private List<GameObject> _items = new List<GameObject>();
        private GameObject _activeItem;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                _items.Add(other.gameObject);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                _items.Remove(other.gameObject);
            }
        }

        private GameObject GetNearestItem(GameObject player)
        {
            float dMin = Single.MaxValue;
            int iMin = -1;
        
            for (var i = 0; i < _items.Count; i++)
            {
                var go = _items[i];
                float d = Vector3.Distance(player.transform.position, go.transform.position);
                if (d < dMin)
                {
                    dMin = d;
                    iMin = i;
                }
            }

            return iMin >= 0 ? _items[iMin] : null;
        }

        public void UpdateActiveItem(GameObject player)
        {
            var item = GetNearestItem(player);
            if (item != _activeItem)
            {
                if (_activeItem != null)
                {
                    _activeItem.GetComponent<SceneItem>().SetInteract(false);
                }
            
                if (item != null)
                {
                    item.GetComponent<SceneItem>().SetInteract(true);
                }
            
                _activeItem = item;
            }
        }

        public void FireActiveItemInteraction()
        {
            if (_activeItem != null)
            {
                _activeItem.GetComponent<SceneItem>().Interact();
            }
        }
    }
}

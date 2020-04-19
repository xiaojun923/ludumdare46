using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class SceneControl
    {
        private static SceneControl _instance;
        public static SceneControl Instance => _instance = _instance ?? new SceneControl();

        public Dictionary<int, GameObject> InteractItems = new Dictionary<int, GameObject>();

        public static int UpdateInteractTarget(GameObject player)
        {
            var component = player.transform.Find("InteractTrigger").GetComponent<CharacterTriggerInteract>();
            
            var item = component.GetNearestItem(player).GetComponent<SceneItem>();
            var table = component.GetNearestTable(player).GetComponent<SceneItem>();
            
            int itemOnTable = BabySmileManager.GetTableItemID(table.id);
            bool on = itemOnTable == item.id;
            item.SetInteract(on);
            table.SetInteract(!on);

            return on ? item.id : table.id;
        }
    }
}

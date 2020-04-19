using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class SceneControl
    {
        private static SceneControl _instance;
        public static SceneControl Instance => _instance = _instance ?? new SceneControl();

        public Dictionary<int, GameObject> InteractItems = new Dictionary<int, GameObject>();
    }
}

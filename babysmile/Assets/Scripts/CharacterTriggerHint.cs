using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public class CharacterTriggerHint : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                var item = other.gameObject.GetComponent<SceneItem>();
                item.ShowHint = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("InteractItem"))
            {
                var item = other.gameObject.GetComponent<SceneItem>();
                item.ShowHint = false;
            }
        }
    }
}
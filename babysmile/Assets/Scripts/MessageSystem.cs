using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD46
{
    public delegate void MessageCallback(object obj);
    
    public static class MessageSystem
    {
        private static Dictionary<string, HashSet<MessageCallback>> _messages =
            new Dictionary<string, HashSet<MessageCallback>>();

        public static void AddListener(string msgName, MessageCallback callback)
        {
            if (!_messages.ContainsKey(msgName))
            {
                _messages.Add(msgName, new HashSet<MessageCallback>());
            }
            var msgSet = _messages[msgName];

            if (msgSet.Contains(callback))
            {
                Debug.LogError($"重复添加消息回调：{msgName}");
            }
            else
            {
                msgSet.Add(callback);
            }
        }

        public static void RemoveListener(string msgName, MessageCallback callback)
        {
            if (_messages.ContainsKey(msgName))
            {
                var msgSet = _messages[msgName];
                if (msgSet.Contains(callback))
                {
                    msgSet.Remove(callback);
                }
            }
        }

        public static void SendMessage(string msgName, object data = null, object sender = null)
        {
            if (_messages.ContainsKey(msgName))
            {
                var msgSet = _messages[msgName];
                foreach (var callback in msgSet)
                {
                    callback(data);
                }
            }
        }
    }
}

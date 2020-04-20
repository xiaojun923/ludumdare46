using UnityEngine;

namespace LD46
{
    public static class MessageType
    {
        public static string InteractTap = "InteractTap";
        public static string InteractHold = "InteractHold";
        public static string CharacterMove = "CharacterMove";

        public static string SuccessfulInteract = "SuccessfulInteract";
        public static string FinishTask = "FinishTask";
    }
    
    public class MessageDataHold
    {
        public bool Holding;
        public GameObject Player;
    }
}

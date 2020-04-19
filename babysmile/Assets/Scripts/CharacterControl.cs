using LD46;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public void Awake()
    {
        MessageSystem.AddListener(MessageType.CharacterMove, OnCharacterMove);
        MessageSystem.AddListener(MessageType.InteractTap, OnInteractTap);
        MessageSystem.AddListener(MessageType.InteractHold, OnInteractHold);
    }

    private void OnCharacterMove(object msg)
    {
        var player = msg as GameObject;
        if (player == null || player.name != name)
        {
            return;
        }
        // Debug.Log($"CharacterMove - {name}");
    }

    private void OnInteractTap(object msg)
    {
        var player = msg as GameObject;
        if (player == null || player.name != name)
        {
            return;
        }
        Debug.Log($"CharacterTap - {name}");
    }

    private void OnInteractHold(object msg)
    {
        var data = msg as MessageDataHold;
         if (data == null || data.Player.name != name)
        {
            return;
        }
        Debug.Log($"CharacterHold:{data.Holding} - {name}");
    }
}

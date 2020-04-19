using LD46;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DataCommand
{
    PickItem = 1,
    SpawnItem = 2,
    DestroyItem = 3,
    UpdateItemStatus = 4,
    PutOnTable = 5,
    PutOnFloor = 6,
    StartCast = 7,
}

public class CharacterControl : MonoBehaviour
{
    public void Awake()
    {
        MessageSystem.AddListener(MessageType.CharacterMove, OnCharacterMove);
        MessageSystem.AddListener(MessageType.InteractTap, OnInteractTap);
        MessageSystem.AddListener(MessageType.InteractHold, OnInteractHold);
    }

    private int _interactTarget;
    public int InteractTarget => _interactTarget;

    private void OnCharacterMove(object msg)
    {
        var player = msg as GameObject;
        if (player == null || player.name != name)
        {
            return;
        }

        int target = SceneControl.Instance.UpdateInteractTarget(player);
        _interactTarget = target;
    }

    private void OnInteractTap(object msg)
    {
        var player = msg as GameObject;
        if (player == null || player.name != name)
        {
            return;
        }
        
        if (_interactTarget > 0)
        {
            SceneControl.Instance.PlayerInteractTap(gameObject, _interactTarget);
        }
    }

    private void OnInteractHold(object msg)
    {
        var data = msg as MessageDataHold;
         if (data == null || data.Player.name != name)
        {
            return;
        }
        SceneControl.Instance.PlayerInteractHold(gameObject, _interactTarget, data.Holding);
    }
}

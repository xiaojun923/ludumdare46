using System;
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

    private SceneItemType _interactType;
    public SceneItemType InteractType => _interactType;

    private int _inHandId;
    public int InHandId
    {
        get => _inHandId;
        set => _inHandId = value;
    }

    public int roleId;

    private void OnCharacterMove(object msg)
    {
        var player = msg as GameObject;
        if (player == null || player.name != name)
        {
            return;
        }

        (_interactTarget, _interactType) = SceneControl.Instance.UpdateInteractTarget(player);
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
            SceneControl.Instance.PlayerInteractTap(gameObject, _interactTarget, _interactType);
        }
        else if (_inHandId > 0)
        {
            SceneControl.Instance.PlayerInteractTap(gameObject, _inHandId, _interactType);
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

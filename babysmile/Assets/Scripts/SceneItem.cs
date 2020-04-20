using System.Collections.Generic;
using DG.Tweening;
using LD46;
using UnityEngine;

public enum SceneItemType
{
    Item,
    Table,
}

public class SceneItem : MonoBehaviour
{
    public SceneItemType type = SceneItemType.Item;
    public int id;

    private int _status;
    public int Status
    {
        get => _status;
        set { _status = value; UpdateStatus(); }
    }

    public List<GameObject> activeStatusMap;

    private bool _showHint;
    private bool _showInteract;
    public GameObject hintObj;
    public UITips tips;
    public bool feedBackOnInteact = false;

    public void OnEnable()
    {
        MessageSystem.AddListener(MessageType.SuccessfulInteract, OnSuccessfulInteract);

    }

    public void OnDisable()
    {
        MessageSystem.RemoveListener(MessageType.SuccessfulInteract, OnSuccessfulInteract);

    }
    public void OnSuccessfulInteract(object msg)
    {
        int msgID = (int) msg;

        if (id == msgID && feedBackOnInteact)
        {
            transform.DOKill();
            transform.DOShakeScale(0.4f, 1f);
        }
    }

    public void Start()
    {
        if (id > 0)
        {
            if (type == SceneItemType.Item)
            {
                SceneControl.Instance.InteractItems.Add(id, gameObject);
            }
            else if (type == SceneItemType.Table)
            {
                SceneControl.Instance.InteractTables.Add(id, gameObject);
            }
            gameObject.tag = "InteractItem";
        }
        else
        {
            Debug.LogError($"未指定场景物体ID：{name}");
        }
        
        _status = 1;
        UpdateStatus();
    }

    public void SetInteract(bool interact)
    {
        _showInteract = interact;
        Debug.Log($"GameObject [{name}]], Interact=[{interact}]");
    }

    public void ShowHint(bool show)
    {
        Debug.Log($"Show Hint [{name}]], Interact=[{show}]");
        _showHint = show;

        if (hintObj != null)
        {
            hintObj.SetActive(show);
        }
        else
        {
            transform.localScale = show ? Vector3.one * 1.05f : Vector3.one * 1.0f;
        }

        if (tips != null)
        {
            tips.ShowTips(show);
        }
    }



    public void UpdateStatus()
    {
        if (activeStatusMap.Count > 0)
        {
            foreach (var go in activeStatusMap)
            {
                if (go != null)
                {
                    go.SetActive(false);
                }
            }

            if (activeStatusMap[_status - 1] != null)
            {
                activeStatusMap[_status - 1].SetActive(true);
            }
        }
    }
}

using System.Collections.Generic;
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
        get { return _status; }
        set { _status = value; UpdateStatus(); }
    }

    public List<GameObject> activeStatusMap;

    private bool _showHint;
    private bool _showInteract;

    public void Awake()
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
    }

    public void Start()
    {
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
        _showHint = show;
        transform.localScale = show ? Vector3.one * 1.05f : Vector3.one * 1.0f;
    }

    public void UpdateStatus()
    {
        if (activeStatusMap.Count > 0)
        {
            foreach (var go in activeStatusMap)
            {
                go.SetActive(false);
            }
            activeStatusMap[_status - 1].SetActive(true);
        }
    }
}

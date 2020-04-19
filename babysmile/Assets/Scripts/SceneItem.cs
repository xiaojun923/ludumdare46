using System;
using System.Collections;
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
    public int id;
    public SceneItemType type = SceneItemType.Item;

    private bool _showHint;
    private bool _showInteract;

    public void Awake()
    {
        if (id > 0)
        {
            SceneControl.Instance.InteractItems.Add(id, gameObject);
            gameObject.tag = "InteractItem";
        }
        else
        {
            Debug.LogError($"未指定场景物体ID：{name}");
        }
    }

    public void Interact()
    {
        Debug.Log($"Interacted: {name}");
    }

    public void SetInteract(bool interact)
    {
        _showInteract = interact;
        Debug.Log($"GameObject [{name}]], Interact=[{interact}]");
    }

    public void ShowHint(bool show)
    {
        _showHint = show;
        if (show)
        {
            transform.localScale = Vector3.one * 1.1f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}

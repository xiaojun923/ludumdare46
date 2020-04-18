using System;
using System.Collections;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class SceneItem : MonoBehaviour
{
    public int id;

    private bool _showHint;
    public bool ShowHint
    {
        get => _showHint;
        set => _showHint = value;
    }

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
}

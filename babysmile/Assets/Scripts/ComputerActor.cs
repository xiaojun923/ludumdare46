using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComputerActor : MonoBehaviour
{
    public SceneItem item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<SceneItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

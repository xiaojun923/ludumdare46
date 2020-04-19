using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyActor : MonoBehaviour
{
    public SceneItem item;
    public static List<int> taskList = new List<int>();
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

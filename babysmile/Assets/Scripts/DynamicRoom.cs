using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRoom : MonoBehaviour
{
    private  List<Material> matList = new List<Material>();

    public List<GameObject> ReferenceObj;

    public float LimitHeight;

    public float HeightTop = 3f;
    public float HeightBot = 0.5f;
    public float SmoothRate = 2f;

    public string senseTag = "Player";

    private int ThresholdID = Shader.PropertyToID("_ThresholdY");
    public void GetMat( List<GameObject> objList )
    {
        matList = new List<Material>();
        foreach (var obj in objList)
        {
            matList.AddRange(obj.GetComponent<Renderer>().materials);

        }
    }

    public void UpdateHeight()
    {
        LimitHeight = Mathf.Lerp(LimitHeight, (CharacterCounter <= 0)? HeightTop : HeightBot, SmoothRate * Time.deltaTime);
        
    }

    public void SynHeight()
    { 
        foreach (var mat in matList)
        {
            mat.SetFloat(ThresholdID,LimitHeight);
        }

    }

    public void Start()
    {
        GetMat(ReferenceObj);
        LimitHeight = HeightTop;
    }

    public void Update()
    {
        UpdateHeight();
        SynHeight();
    }

    public int CharacterCounter = 0 ;

    public void OnTriggerEnter(Collider col)
    {

        if (col.tag.Equals(senseTag))
        {
            CharacterCounter++;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals(senseTag))
        {
            CharacterCounter--;
        }
    }
}

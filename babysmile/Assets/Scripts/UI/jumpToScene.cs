using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpToScene : MonoBehaviour
{
    public void JumpToScene()
    {
        SceneManager.LoadScene(1);
    }
}

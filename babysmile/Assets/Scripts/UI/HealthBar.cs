using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;
    public float temHealth;
    public float smooth = 5f;
    public void Start()
    {
        temHealth = 1.0f;
    }

    public void Update()
    {
        float health = BabySmileManager.GetHealthRate();

        temHealth = Mathf.Lerp(temHealth, health, smooth * Time.deltaTime);
        image.fillAmount = temHealth;
    }
}

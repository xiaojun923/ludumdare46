using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITips : MonoBehaviour
{
    public float aniShowDuration = 0.5f;
    public float aniFadeDuration = 0.3f;
    public Ease easeType;
    public bool ShowAtStart = false;
    public void Start()
    {
        if (!ShowAtStart)
            transform.DOScale(0, 0);
    }

    public void ShowTips(bool isShow )
    {
        if (isShow)
            OnPlay();
        else
            OnStop();
    }
    public void OnPlay()
    {
        transform.DOKill();
        transform.DOScale(1f, aniShowDuration).SetEase(easeType);
    }

    public void OnStop()
    {
        transform.DOKill();

        transform.DOScale(0, aniFadeDuration).SetEase(easeType);
    }
}

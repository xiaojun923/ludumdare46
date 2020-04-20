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

    public bool SwingOnShow = false;
    public float swingDuration = 1f;
    public float swingIntensity = 10f;
    public Ease swingEase;


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
        transform.localRotation = Quaternion.identity;
        
        transform.DOScale(1f, aniShowDuration).SetEase(easeType);

        if (SwingOnShow)
        {
            transform.localEulerAngles = new Vector3(0,0,-swingIntensity*0.5f);
            transform.DORotate(new Vector3(0,0,swingIntensity) , swingDuration).SetLoops(-1, LoopType.Yoyo).SetRelative(true).SetEase(swingEase);
        }
    }

    public void OnStop()
    {
        transform.DOKill();

        transform.DOScale(0, aniFadeDuration).SetEase(easeType);
    }

    public void ShowFor(float duration)
    {
        transform.DOScale(1f, aniShowDuration).SetEase(easeType);

        transform.DOScale(0, aniFadeDuration).SetEase(easeType).SetDelay(duration);
    }
}

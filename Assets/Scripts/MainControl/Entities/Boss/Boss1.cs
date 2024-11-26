using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : Boss
{
    public Image timeBar;
    public float _time = 100f;
    protected override void LoadBoss()
    {
        _animationTime = 2f;
        _time = totalTime;
        timeBar.transform.parent.gameObject.SetActive(true);
        
    }
    protected override void LoadUpdate()
    {
        timeBar.fillAmount = totalTime/_time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouderCtrl : BaseMovement
{
    private static BouderCtrl instance;
    public static BouderCtrl Instance => instance;
    public TriggerActionCtrl triggerActionCtrl;
    public GameObject player;
    int hitNumber = 0;
    protected override void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Trap Ctrl allowed"); }
        instance = this;
        LoadComponents();
    }
    protected override void LoadComponents()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        _anim = player.GetComponent<Animator>();
        triggerActionCtrl = transform.GetComponentInChildren<TriggerActionCtrl>();
    }
    public void OutBouder()
    {
        if (triggerActionCtrl == null) return;
        hitNumber--;
        if (hitNumber < 0)
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        player.transform.position = checkPos;
    }
}

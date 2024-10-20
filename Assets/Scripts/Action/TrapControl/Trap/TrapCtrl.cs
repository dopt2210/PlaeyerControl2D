using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrapCtrl : BaseMovement
{
    private static TrapCtrl instance;
    public static TrapCtrl Instance => instance;
    public TriggerActionCtrl triggerActionCtrl;
    public GameObject player;
    int hitNumber = 2;
    protected override void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Trap Ctrl allowed"); }
        instance = this;
        LoadComponent();
    }
    protected override void LoadComponent()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = player.GetComponent<Animator>();
        triggerActionCtrl = transform.GetComponentInChildren<TriggerActionCtrl>();
    }

    public void HitTrap(int dmg)
    {
        if (triggerActionCtrl == null) return;
        hitNumber -= dmg;
        if(hitNumber > 0)
        {
            _anim.Play("Damged");
            Debug.Log("Hit");
        }
        else Die();
        
    }
    public void HitTrap(int dmg, List<Trigger> listTrap)
    {
        foreach (Trigger go in listTrap)
        {
            if (triggerActionCtrl == null) return;
            hitNumber -= dmg;
            if (hitNumber > 0)
            {
                _anim.Play("Damged");
                Debug.Log("Hit");
            }
            else Die();
        }

    }

    private void Die()
    {
        Respawn();
    }

    private void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        player.transform.position = checkPos;
        hitNumber = 2;
    }

}

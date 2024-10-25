using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrapCtrl : MonoBehaviour, IGameData
{
    private static TrapCtrl instance;
    public static TrapCtrl Instance => instance;
    public TriggerActionCtrl triggerActionCtrl;
    public GameObject player;
    int hitNumber = 2;
    int deadCount = 0;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Trap Ctrl allowed"); }
        instance = this;
        LoadComponent();
    }
    protected void LoadComponent()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        triggerActionCtrl = GetComponent<TriggerActionCtrl>();
    }

    public void HitTrap(int dmg)
    {
        if (triggerActionCtrl == null) return;
        hitNumber -= dmg;
        if(hitNumber > 0)
        {
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
                Debug.Log("Hit");
            }
            else
            {
                Die();
                return;
            }
        }

    }

    private void Die()
    {
        deadCount++;
        Debug.Log("Dead: " + deadCount);
        Respawn();
    }

    private void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        player.transform.position = checkPos;
        hitNumber = 2;
    }

    public void LoadData(GameData gameData)
    {
        this.deadCount = gameData.deathCount;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.deathCount = deadCount;
    }
}

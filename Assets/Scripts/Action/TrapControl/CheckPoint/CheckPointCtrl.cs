using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckPointCtrl : MonoBehaviour
{
    private static CheckPointCtrl instance;
    public static CheckPointCtrl Instance => instance;
    public GameObject player;
    public TriggerActionCtrl triggerActionCtrl;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Checkpoint Ctrl allowed"); }
        instance = this;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        CheckReload();
    }

    private void CheckReload()
    {
        if(!PlayerCtrl.instance.LoadDown) return;
        if(this.triggerActionCtrl == null) return;

        Vector2 checkPos = this.triggerActionCtrl.transform.position;
        

        player.transform.position = checkPos;
    }
    public void SetCheckPoint(TriggerActionCtrl ctrl)
    {
        if (IsOldCheckPoint(ctrl)) return;
        this.triggerActionCtrl = ctrl;
    }

    private bool IsOldCheckPoint(TriggerActionCtrl ctrl)
    {
        if(ctrl == null) return false;
        Regex rg = new Regex(@"\d+");
        Match match;

        string newCpName = ctrl.name;
        match = rg.Match(newCpName);
        int newCpNumber = Int32.Parse(match.Value);

        string oldCpName = this.triggerActionCtrl.name;
        match = rg.Match(oldCpName);
        int oldCpNumber = Int32.Parse(match.Value);

        if(oldCpNumber >= newCpNumber) return true;

        return false;
    }

}

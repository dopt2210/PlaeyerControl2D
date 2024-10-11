using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public static CheckPointManager instance;
    public GameObject player;
    public StateCtrl state;

    private void Awake()
    {
        CheckPointManager.instance = this;
    }
    private void Start()
    {
        player = GameObject.Find("PLAYER");
    }
    private void Update()
    {
        CheckReload();
    }

    private void CheckReload()
    {
        if(!PlayerCtrl.instance.LoadDown) return;
        if(this.state == null) return;

        Vector2 checkPos = this.state.transform.position;
        

        player.transform.position = checkPos;
    }
    public void SetCheckPoint(StateCtrl state)
    {
        if (IsOldCheckPoint(state)) return;
        this.state = state;
    }

    private bool IsOldCheckPoint(StateCtrl state)
    {
        if(state == null) return false;
        Regex rg = new Regex(@"\d+");
        Match match;

        string newCpName = state.name;
        match = rg.Match(newCpName);
        int newCpNumber = Int32.Parse(match.Value);

        string oldCpName = this.state.name;
        match = rg.Match(oldCpName);
        int oldCpNumber = Int32.Parse(match.Value);

        if(oldCpNumber >= newCpNumber) return true;

        return false;
    }

}

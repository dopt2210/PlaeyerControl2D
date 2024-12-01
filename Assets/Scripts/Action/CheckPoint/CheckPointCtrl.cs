using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckPointCtrl : MonoBehaviour, IGameData
{
    private static CheckPointCtrl instance;
    public static CheckPointCtrl Instance => instance;
    private GameObject player;
    public TriggerActionCtrl triggerActionCtrl;
    private Vector3 currentSavePoint;
    private bool _isSave;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Checkpoint Ctrl allowed"); }
        instance = this;
        triggerActionCtrl = GetComponentInChildren<TriggerActionCtrl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        CheckReload();
        if (!_isSave) currentSavePoint = triggerActionCtrl.transform.position;
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
        _isSave = false;
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

    public void LoadData(GameData gameData)
    {
        if (gameData == null) return;
        player.transform.position = gameData.playerPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = currentSavePoint;
    }
}

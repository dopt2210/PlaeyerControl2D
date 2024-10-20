using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameDataCtrl : MonoBehaviour
{
    private static GameDataCtrl instance;
    public static GameDataCtrl Instance => instance;
    private GameData gameData;
    [SerializeField] private string fileName;
    [SerializeField] private bool useEnDe;
    private FileDataHandler fileHandler;
    private List<IGameData> gameDatas;
    private void Awake()
    {
        if (instance != null) { Destroy(this.gameObject); Debug.LogError("Ctrl existed!"); }
        instance = this;
    }
    private void Start()
    {
        this.fileHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEnDe); 
        this.gameDatas = FindAllGameData();
        LoadGame();
    }

    private List<IGameData> FindAllGameData()
    {
        IEnumerable<IGameData> gameDatas = FindObjectsOfType<MonoBehaviour>().OfType<IGameData>();
        return new List<IGameData>(gameDatas);
    }

    public void NewGame(    )
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        foreach (IGameData data in gameDatas)
        {
            data.SaveData(ref gameData);
        }
        Debug.Log("loaded dead: " +  gameData.deathCount);
        fileHandler.Save(gameData);
    }

    public void LoadGame()
    {
        this.gameData = fileHandler.Load();
        if (gameData == null)
        {
            Debug.Log("Creat another");
            NewGame();
        }
        foreach (IGameData data in gameDatas)
        {
            data.LoadData(gameData);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

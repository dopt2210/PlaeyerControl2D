using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataCtrl : MonoBehaviour
{
    public static GameDataCtrl Instance { get; private set; }

    [SerializeField] private string fileName;
    [SerializeField] private bool useEnDe;

    private FileDataHandler fileHandler;

    private GameData gameData;
    private List<IGameData> gameDatas;
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); Debug.LogError("Ctrl existed!"); }
        Instance = this;
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
        fileHandler.Save(gameData);
    }

    public void LoadGame()
    {
        this.gameData = fileHandler.Load();
        if (gameData == null)
        {
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

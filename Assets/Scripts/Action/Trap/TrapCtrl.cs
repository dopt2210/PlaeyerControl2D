using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCtrl : MonoBehaviour, IGameData, IDamageAble<Trigger>
{
    private static TrapCtrl instance;
    public static TrapCtrl Instance => instance;
    #region Vars
    public float _maxHP {  get; set; }
    public float _currentHP {  get; set; }
    public int _deadCount {  get; set; }

    private GameObject player;
    #endregion
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Trap Ctrl allowed"); }
        instance = this;
        LoadComponent();
    }
    private void Start()
    {
        _currentHP = 0;
    }
    protected void LoadComponent()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #region Data
    public void LoadData(GameData gameData)
    {
        _deadCount = gameData.deathCount;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.deathCount = _deadCount;
    }
    #endregion
    #region Interface Function
    public void Die()
    {
        _deadCount++;
        Debug.Log("Dead: " + _deadCount);
        Respawn();
    }

    public void Damage(float dmg, IEnumerable<Trigger> objects)
    {
        foreach (Trigger tg in objects)
        {
            _currentHP -= dmg;
            if (_currentHP > 0)
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

    public void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        player.transform.position = checkPos;
        _currentHP = 2;
    }


    #endregion
}

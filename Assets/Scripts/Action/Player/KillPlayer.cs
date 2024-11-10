using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour, IDamageAble<Trigger>, IGameData
{
    public static KillPlayer Instance {  get; private set; }
    public float _maxHP { get; set; }
    public float _currentHP { get; set; }
    public int _deadCount { get; set; }
    private GameObject _player;
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        _maxHP = 0;
        _currentHP = _maxHP;
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
    public void Damage(float dmg, IEnumerable<object> objects = null)
    {
        _currentHP -= dmg;
        if (_currentHP < 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        _deadCount++;
        _player.SetActive(false);
        PlayerCtrl.DeactivatePlayerCtrl();
        Invoke("Respawn", 5f);
    }

    public void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        PlayerCtrl.ActivatePlayerCtrl();
        _player.SetActive(true);
        _player.transform.position = checkPos;
        _currentHP = 0;
    }


    public void LoadData(GameData gameData)
    {
        _deadCount = gameData.deathCount;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.deathCount = _deadCount;
    }
}

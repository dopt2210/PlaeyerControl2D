using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouderCtrl : BaseMovement, IDamageAble<object>
{
    private static BouderCtrl instance;
    public static BouderCtrl Instance => instance;

    public float _maxHP {  get; set; }
    public float _currentHP {  get; set; }
    public int _deadCount {  get; set; }

    public TriggerActionCtrl triggerActionCtrl;
    public GameObject player;

    protected override void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Trap Ctrl allowed"); }
        instance = this;
        LoadComponents();
    }
    private void Start()
    {
        _currentHP = 0;
    }
    protected override void LoadComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = player.GetComponent<Animator>();
        triggerActionCtrl = transform.GetComponentInChildren<TriggerActionCtrl>();
    }
    #region Interface Function
    public void Damage(float dmg, IEnumerable<object> objects = null)
    {
        if (triggerActionCtrl == null) return;
        _currentHP -= dmg;
        if (_currentHP < 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        Respawn();
    }

    public void Respawn()
    {
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        player.transform.position = checkPos;
    }
    #endregion
}

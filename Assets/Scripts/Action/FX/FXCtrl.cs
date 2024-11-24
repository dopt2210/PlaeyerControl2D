using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FXCtrl : MonoBehaviour
{
    public GameObject _shieldFX;
    private float _shieldTime = 1f;
    private float _shieldRange = 10f;
    [SerializeField] private GameObject[] listDangerProjectiles;
    private bool _isShieldActive = false;
    private void Start()
    {
        _shieldFX = GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).gameObject;
        _shieldFX.SetActive(false);
        listDangerProjectiles = GameObject.FindGameObjectsWithTag("Enemy");
        
    }
    void Update()
    {
        if (PlayerCtrl.instance.InteractDown)
        {
            StartCoroutine(WaitShieldActive());
            Invincible();
        }
    }
    private void FixedUpdate()
    {
        
    }
    private IEnumerator WaitShieldActive()
    {
        ShieldOn();
        yield return new WaitForSeconds(_shieldTime);
        ShieldOff();
    }
    private void ShieldOn()
    {
        _shieldFX.SetActive(true);
        _isShieldActive = true;
    }
    private void ShieldOff()
    {
        _shieldFX.SetActive(false);
        _isShieldActive = false;
    }
    private void Invincible()
    {
        if (_isShieldActive)
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_shieldFX.transform.position, _shieldRange);
            foreach (var item in collider2D)
            {
                if (listDangerProjectiles.Contains(item.gameObject)) item.gameObject.SetActive(false);
            }    
        }
    }
}

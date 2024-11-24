using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FXCtrl : MonoBehaviour
{
    [SerializeField] private GameObject _shieldFX;
    [SerializeField] private Material _material;
    [SerializeField] private GameObject[] listDangerProjectiles;

    private static int _distance = Shader.PropertyToID("_ShockWaveDistance");

    private float shockWaveTime = 0.75f;
    private float waitTime = 5f;
    private float shieldTime = 0.75f;

    private float _shieldRange = 5f;
    private float _shockWaveStartPos = -0.1f;
    private float _shockWaveEndPos = 1f;

    [SerializeField] private bool _isShieldActive = false;
    private bool _isWaiting = false;
    private void Start()
    {
        _shieldFX = GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).gameObject;
        _material = GameObject.FindGameObjectWithTag("Player").transform.GetChild(7).GetComponent<SpriteRenderer>().material;
        ShieldOff();
        listDangerProjectiles = GameObject.FindGameObjectsWithTag("Enemy");
        
    }
    void Update()
    {
        if (PlayerCtrl.instance.SkillDown)
        {
            CallShockWave();
        }
    }
    private void CallShockWave()
    {
        if (!_isShieldActive)
        {
            StartCoroutine(ShockWaveAciton(_shockWaveStartPos, _shockWaveEndPos));
            StartCoroutine(ShieldAction());
            StartCoroutine(WaitTillCanUseAgain());
        }
    }
    private IEnumerator ShockWaveAciton(float startPos, float endPos)
    {
        _material.SetFloat(_distance, shockWaveTime);
        float lerpAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / shockWaveTime));
            _material.SetFloat(_distance, lerpAmount);
            yield return null;
        }
    }
    private IEnumerator ShieldAction()
    {
        float lerpAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < shieldTime)
        {
            elapsedTime += Time.deltaTime;
            lerpAmount = Mathf.Lerp(0, 2f, (elapsedTime / shieldTime));
            ShieldOn();
            yield return null;
        }
        ShieldOff();

    }
    

    private IEnumerator WaitTillCanUseAgain()
    {
        if (_isWaiting) yield break;

        _isWaiting = true;
        _isShieldActive = true;
        Invincible();

        yield return new WaitForSeconds(waitTime);

        _isShieldActive = false;
        _isWaiting = false;
    }

    private void ShieldOn()
    {
        _shieldFX.SetActive(true);
    }
    private void ShieldOff()
    {
        _shieldFX.SetActive(false);
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

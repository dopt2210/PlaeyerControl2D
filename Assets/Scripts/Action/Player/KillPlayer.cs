using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour, IDamageAble<Trigger>, IGameData
{
    public static KillPlayer Instance {  get; private set; }
    public static bool IsDie { get; private set; }
    [SerializeField] private GameObject DieFX;
    private GameObject _dieFx;
    public float _maxHP { get; set; }
    public float _currentHP { get; set; }
    public float deadTime = 3f;
    public int _deadCount { get; set; }
    
    private GameObject _player;
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        _player = GameObject.FindGameObjectWithTag("Player");
        InitFX();
    }
    private void Start()
    {
        _maxHP = 0;
        _currentHP = _maxHP;
    }
    public void Damage(float dmg, IEnumerable<Trigger> objects)
    {
        if (!FXCtrl.isInvicible)
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
    public void KillByBoss(CinemachineVirtualCamera camera)
    {
        if (!FXCtrl.isInvicible)
        {
            _currentHP--;
            Die();
            CameraCtrl.Instance.SwapCamera(CameraCtrl.Instance._currentCamera, camera);
        }
        
    }

    public void Die()
    {
        IsDie = true;
        _deadCount++;
        JumpCtrl._isJumping = false;
        DashCtl._isCanDash = false;
        _player.SetActive(false);
        PlayFX();
        StartCoroutine(DeactivateAfterTime(_dieFx, 1f));
        PlayerCtrl.DeactivatePlayerCtrl();
        
        Invoke("Respawn", 2f);
    }

    public void Respawn()
    {
        IsDie = false;
        Vector2 checkPos = CheckPointCtrl.Instance.triggerActionCtrl.transform.position;
        PlayerCtrl.ActivatePlayerCtrl();

        _player.SetActive(true);
        
        _player.transform.position = checkPos;
        _currentHP = 0;
        
    }
    private void InitFX()
    {
        _dieFx =  GameObject.Instantiate(DieFX, this.transform);
        _dieFx.SetActive(false);
    }
    private void PlayFX()
    {
        _dieFx.SetActive(true);
        _dieFx.transform.position = _player.transform.position;
    }
    private IEnumerator DeactivateAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoBoss : MonoBehaviour
{
    public static TempoBoss Instance {  get; private set; }
    private GameObject bossInstance;

    [SerializeField] private GameObject boss;

    private GameObject doorClose;
    private GameObject bossTrigger;
    
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        doorClose = transform.GetChild(0).gameObject;
        doorClose.SetActive(false);
        bossTrigger = transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if (KillPlayer.IsDie)
        {
            doorClose.SetActive(false);
            bossTrigger.SetActive(true);
            //MusicManager.Instance.PlayMusic("Theme");
            RemoveBoss();

        }
    }
    public void InitBoss()
    {
        bossTrigger.SetActive(false);
        //MusicManager.Instance.PlayMusic("Boss");
        bossInstance = GameObject.Instantiate(boss, new Vector3(250, 49, 0), Quaternion.Euler(0,180,0));
    }
    public void CloseBossDoor()
    {
        doorClose.SetActive(true);
    }
    void RemoveBoss()
    {
        Destroy(bossInstance);
    }
}

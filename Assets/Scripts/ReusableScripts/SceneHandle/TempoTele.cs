using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoTele : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetActive(true);
            //MusicManager.Instance.PlayMusic("Boss");
            this.gameObject.SetActive(false);
            boss.SetActive(true);
        }
    }
}

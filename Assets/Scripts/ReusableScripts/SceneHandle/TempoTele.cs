using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoTele : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            boss.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{

    [SerializeField] private GameObject _leftBackGround;
    [SerializeField] private GameObject _rightBackGround;
    private Collider2D _coll;
    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _coll.bounds.center).normalized;
            if (_leftBackGround != null && _rightBackGround != null)
            {
                BackCtrl.instance.SwapBackground(_leftBackGround, _rightBackGround, exitDirection);
            }

        }
    }
    
}

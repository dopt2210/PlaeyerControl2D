using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> listBackground;
    [SerializeField] private GameObject backgroundCamera;
    [SerializeField] private GameObject _leftBackGround;
    [SerializeField] private GameObject _rightBackGround;
    private GameObject currentBackground;
    
    private Collider2D _coll;
    
    private void Awake()
    {
        foreach(Transform t in backgroundCamera.transform)
        {
            listBackground.Add(t.gameObject);
        }
        foreach (var item in listBackground)
        {
            if(item.activeSelf) currentBackground = item;
        }
    }
    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    void SwapBackground(GameObject _left, GameObject _right, Vector2 exitPos)
    {
        if (currentBackground == _leftBackGround && exitPos.x > 0)
        {
            _rightBackGround.SetActive(true);
            _leftBackGround.SetActive(false);

            currentBackground = _rightBackGround;
        }
        if (currentBackground == _rightBackGround && exitPos.x < 0)
        {
            _rightBackGround.SetActive(false);
            _leftBackGround.SetActive(true);
            currentBackground = _leftBackGround;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _coll.bounds.center).normalized;
            if (_leftBackGround != null && _rightBackGround != null)
            {
                SwapBackground(_leftBackGround, _rightBackGround, exitDirection);
            }

        }
    }
    
}

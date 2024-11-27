using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCtrl : MonoBehaviour
{
    public static BackCtrl instance {  get; private set; }

    [SerializeField] private List<GameObject> listBackground;
    [SerializeField] private GameObject backgroundCamera;
    [SerializeField] private GameObject currentBackground;

    

    private void Awake()
    {
        if(instance != null) { Destroy(gameObject); return; }
        instance = this;
        foreach (Transform t in backgroundCamera.transform)
        {
            listBackground.Add(t.gameObject);
        }
        foreach (var item in listBackground)
        {
            if (item.activeSelf) currentBackground = item;
        }
    }

    public void SwapBackground(GameObject _left, GameObject _right, Vector2 exitPos)
    {
        if (currentBackground == _left && exitPos.x > 0)
        {
            _right.SetActive(true);
            _left.SetActive(false);

            currentBackground = _right;
        }
        if (currentBackground == _right && exitPos.x < 0)
        {
            _right.SetActive(false);
            _left.SetActive(true);
            currentBackground = _left;
        }
    }
}

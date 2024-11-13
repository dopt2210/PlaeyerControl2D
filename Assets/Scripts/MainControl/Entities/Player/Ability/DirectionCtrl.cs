using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCtrl : MonoBehaviour
{
    private GameObject _player;
    private Coroutine _coroutine;
    public bool _isFacingRight;

    public float _flipRotationTime = 0.5f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _isFacingRight = true;
    }
    public void CallTurn()
    {
        _coroutine = StartCoroutine(FlipYLearp());
    }
    IEnumerator FlipYLearp()
    {
        float startRotation = transform.parent.localEulerAngles.y;
        float endRotation = DetectRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }
    float DetectRotation()
    {
        _isFacingRight = !_isFacingRight;
        if (_isFacingRight) return 180f;
        else return 0f;
    }
}

using System.Collections;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    private bool _isLoadFromDoor;
    [SerializeField] private Transform targetPortal; 
    private bool canTeleport = true;
    private bool isTeleporting = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            isTeleporting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            isTeleporting = false;
        }
    }

    private void Update()
    {
        if (PlayerCtrl.instance.InteractDown && isTeleporting)
        {
            isTeleporting = false;
            _player.transform.position = targetPortal.position;
            SpawnToThisDoor();

        }
    }


    #region Fade
    public void SpawnToThisDoor()
    {
        _isLoadFromDoor = true;
        StartCoroutine(FadeOutChange());
    }
    private IEnumerator FadeOutChange()
    {
        PlayerCtrl.DeactivatePlayerCtrl();
        SceneFadeCtrl.Instance.StartFadeOut();

        while (SceneFadeCtrl.Instance._isFadeOut)
        {
            yield return null;
        }
        SceneFadeCtrl.Instance.StartFadeIn();
        if (_isLoadFromDoor)
        {
            StartCoroutine(ActivatePlayerControlAfterFadeIn());
            _isLoadFromDoor = false;

        }
    }
    private IEnumerator ActivatePlayerControlAfterFadeIn()
    {
        while (SceneFadeCtrl.Instance._isFadeIn)
        {
            yield return null;
        }
        PlayerCtrl.ActivatePlayerCtrl();
    }
    #endregion

}
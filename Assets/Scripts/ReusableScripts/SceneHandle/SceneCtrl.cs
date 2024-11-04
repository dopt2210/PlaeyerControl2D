using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public static SceneCtrl Instance { get; private set; }
    public static bool _loadFromDoor;
    private GameObject _player;
    private Collider2D _doorCol;
    private Collider2D _playerCol;
    private Vector3 _playerSpawnPosition;
    private DoorCtrl.DoorToSpawn _doorToSpawn;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCol = _player.GetComponent<Collider2D>();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public static void SwapSceneFromDoorUse(SceneField sceneField, DoorCtrl.DoorToSpawn doorToSpawn)
    {
        _loadFromDoor = true;
        Instance.StartCoroutine(Instance.FadeOutChange(sceneField, doorToSpawn));
    }
    private IEnumerator FadeOutChange(SceneField sceneField, DoorCtrl.DoorToSpawn doorToSpawn = DoorCtrl.DoorToSpawn.None)
    {
        PlayerCtrl.DeactivatePlayerCtrl();
        SceneFadeCtrl.Instance.StartFadeOut();

        while (SceneFadeCtrl.Instance._isFadeOut)
        {

            yield return null;
        }
        _doorToSpawn = doorToSpawn;
        SceneManager.LoadScene(sceneField);
    }
    private IEnumerator ActivatePlayerContrlAfterFadeIn()
    {
        while (SceneFadeCtrl.Instance._isFadeIn)
        {
            yield return null;
        }
        PlayerCtrl.ActivatePlayerCtrl();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneFadeCtrl.Instance.StartFadeIn();

        if (_loadFromDoor)
        {
            StartCoroutine(ActivatePlayerContrlAfterFadeIn());
            FindDoor(_doorToSpawn);
            _player.transform.position = _playerSpawnPosition;
            _loadFromDoor = false;
        }
    }
    private void FindDoor(DoorCtrl.DoorToSpawn doorToSpawn)
    {
        DoorOpen[] doors = FindObjectsOfType<DoorOpen>();

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].currentDoorPosition == doorToSpawn)
            {
                _doorCol = doors[i].gameObject.GetComponentInParent<Collider2D>();

                CalculatePlayerPosition();
                return;
            }
        }
    }

    private void CalculatePlayerPosition()
    {
        float colHeight = _playerCol.bounds.extents.y;
        _playerSpawnPosition = _doorCol.transform.position - new Vector3 (0f, colHeight, 0f);
    }
}

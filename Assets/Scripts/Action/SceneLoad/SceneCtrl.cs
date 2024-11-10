using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public static SceneCtrl Instance { get; private set; }

    public static bool _isLoadFromDoor;
    private GameObject _player;
    [SerializeField] private Collider2D _doorCol;
    [SerializeField] private Collider2D _playerCol;
    private Vector3 _playerSpawnPosition;
    private DoorCtrl.DoorToSpawn _doorToSpawn;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCol = _player.GetComponent<Collider2D>();
    }
    #region LoadScene
    public void LoadScene(SceneField[] _sceneLoad)
    {
        for (int i = 0; i < _sceneLoad.Length; i++)
        {
            bool isLoadScene = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _sceneLoad[i].SceneName)
                {
                    isLoadScene = true;
                    break;
                }
            }
            if (!isLoadScene)
            {
                SceneManager.LoadSceneAsync(_sceneLoad[i], LoadSceneMode.Additive);
            }
        }
    }
    public void UnloadScene(SceneField[] _sceneUnload)
    {
        for (int i = 0; i < _sceneUnload.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _sceneUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_sceneUnload[i]);
                }
            }
        }
    }
    #endregion

    public static void SwapFromDoorUse(DoorCtrl.DoorToSpawn doorToSpawn)
    {
        _isLoadFromDoor = true;
        Instance.StartCoroutine(Instance.FadeOutChange(doorToSpawn));
    }
    private IEnumerator FadeOutChange(DoorCtrl.DoorToSpawn doorToSpawn = DoorCtrl.DoorToSpawn.None)
    {
        PlayerCtrl.DeactivatePlayerCtrl();
        SceneFadeCtrl.Instance.StartFadeOut();

        while (SceneFadeCtrl.Instance._isFadeOut)
        {
            yield return null;
        }
        _doorToSpawn = doorToSpawn;
        SceneFadeCtrl.Instance.StartFadeIn();
        if (_isLoadFromDoor)
        {
            StartCoroutine(ActivatePlayerControlAfterFadeIn());
            FindDoor(_doorToSpawn);
            _player.transform.position = _playerSpawnPosition;
            _isLoadFromDoor = false;
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
        _playerSpawnPosition = _doorCol.transform.position - new Vector3(0f, colHeight, 0f);
    }

    private IEnumerator ActivatePlayerControlAfterFadeIn()
    {
        while (SceneFadeCtrl.Instance._isFadeIn)
        {
            yield return null;
        }
        PlayerCtrl.ActivatePlayerCtrl();
    }
}

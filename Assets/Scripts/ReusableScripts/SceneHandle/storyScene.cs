using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class storyScene : MonoBehaviour
{
    public PlayableDirector timelineDirector; // Gán PlayableDirector từ Inspector

    [SerializeField] private Animator flashText;

    private bool isFlash = true;
    private bool _isEndTimeLine;

    [Header("Scene to load")]
    [SerializeField] private SceneField playerScene;
    [SerializeField] private SceneField mapScene;

    private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();

    private void Awake()
    {
        _isEndTimeLine = false;
        isFlash = true;
        flashText.gameObject.SetActive(false);
    }

    void Start()
    {
        if (timelineDirector != null)
        {
            timelineDirector.stopped += OnTimelineFinished; // Đăng ký sự kiện
        }
    }
    private void Update()
    {
        if (_isEndTimeLine) flashText.gameObject.SetActive(isFlash); 
        OpenMenuByPressKey();
    }
    void OnTimelineFinished(PlayableDirector director)
    {
        if (director == timelineDirector) // Kiểm tra đúng timeline
        {
            _isEndTimeLine = true;
        }
    }
    void OnDestroy()
    {
        if (timelineDirector != null)
        {
            timelineDirector.stopped -= OnTimelineFinished; // Hủy đăng ký sự kiện
        }
    }
    public void Play()
    {
        if (string.IsNullOrEmpty(playerScene) || string.IsNullOrEmpty(mapScene))
        {
            Debug.LogError("SceneField is not properly assigned!");
            return;
        }
        sceneToLoad.Add(SceneManager.LoadSceneAsync(playerScene));
        sceneToLoad.Add(SceneManager.LoadSceneAsync(mapScene, LoadSceneMode.Additive));
        
        MenuManager.instance.PlayNewGame();

    }
    public void OpenMenuByPressKey()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && isFlash && _isEndTimeLine)
        {
            isFlash = false;
            Play();
        }

    }
}

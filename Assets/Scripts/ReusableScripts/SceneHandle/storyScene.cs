using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class storyScene : MonoBehaviour
{
    public static storyScene instance { get; private set; }
    public PlayableDirector timelineDirector; // Gán PlayableDirector từ Inspector

    public GameObject pressKeyText;
    private TextMeshProUGUI flashText;

    private bool isFlash = true;
    private bool _isEndTimeLine;

    [SerializeField] float _flashTime = 0.5f;

    [Header("Scene to load")]
    [SerializeField] private SceneField playerScene;
    [SerializeField] private SceneField mapScene;

    private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        flashText = pressKeyText.GetComponentInChildren<TextMeshProUGUI>();
        flashText.color = Color.green;
        flashText.alpha = 0f;
        _isEndTimeLine = false;
    }
    void Start()
    {
        if (timelineDirector != null)
        {
            timelineDirector.stopped += OnTimelineFinished; // Đăng ký sự kiện
        }
        StartCoroutine(FlashText());
    }
    private void Update()
    {
        OpenMenuByPressKey();
        OnPLayClicked();
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
    public void OnPLayClicked()
    {
        if (!_isEndTimeLine) return;
        flashText.alpha = 1f;
    }
    private IEnumerator FlashText()
    {
        while (isFlash)
        {
            pressKeyText.SetActive(!pressKeyText.activeSelf);
            
            yield return new WaitForSeconds(_flashTime);
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
        MusicManager.Instance.PlayMusic("Theme");
        MenuManager.instance.IsPlaying = true;
        MenuManager.instance.SetButtonEvent();

    }
    public void OpenMenuByPressKey()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && isFlash && _isEndTimeLine)
        {
            isFlash = false;
            Play();

        }

    }
}

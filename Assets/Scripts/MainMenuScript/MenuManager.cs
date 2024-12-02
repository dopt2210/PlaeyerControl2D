using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IGameData
{  
    public static MenuManager instance {  get; private set; }

    #region UI Vars
    public GameObject CurrentUI { get; private set; }
    public GameObject PauseUI {  get; private set; }
    public GameObject PlayUI {  get; private set; }
    public GameObject OptionUI {  get; private set; }
    public GameObject GraphicUI {  get; private set; }
    public GameObject SoundUI {  get; private set; }
    public GameObject ModeUI { get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Graphic graphic;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Slider[] sliders;
    #endregion

    public static bool IsPaused { get; private set; }
    public static bool IsPlaying { get; private set; }

    private bool _isSwitchButtonDone;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;

        LoadComponent();
    }
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
        CurrentUI = PlayUI;

        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(false);
        }

        SetButtonEvent();
        if(!GameDataCtrl.Instance.HasGameData()) buttons[15].interactable = false;
    }
    private void Update()
    {
        SetButtonBack();
        if (IsPaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
    private void LoadComponent()
    {
        PlayUI = transform.GetChild(0).gameObject;
        OptionUI = transform.GetChild(1).gameObject;
        SoundUI = transform.GetChild(2).gameObject;
        GraphicUI = transform.GetChild(3).gameObject;
        PauseUI = transform.GetChild(4).gameObject;
        ModeUI = transform.GetChild(5).gameObject;

        buttons = GetComponentsInChildren<Button>();
    }
    public void SetButtonEvent()
    {
        RemoveButtonEvent();

        //PlayUI
        buttons[0].onClick.AddListener(() => SwitchUI(ModeUI));         //play
        buttons[1].onClick.AddListener(() => SwitchUI(OptionUI));       //option
        buttons[2].onClick.AddListener(() => QuitGame());               //exit
        //OptionUI
        buttons[3].onClick.AddListener(() => SwitchUI(GraphicUI));      //graphic
        buttons[4].onClick.AddListener(() => SwitchUI(SoundUI));        //volume
        buttons[5].onClick.AddListener(() => SwitchUI(PlayUI));
        //GraphicUI
        buttons[7].onClick.AddListener(graphic.OnHighButtonClick);      //high
        buttons[8].onClick.AddListener(graphic.OnMediumButtonClick);    //medium
        buttons[9].onClick.AddListener(graphic.OnLowButtonClick);       //low
        buttons[10].onClick.AddListener(() => BackToUI(OptionUI));      //backOfGraphic - Option
        //PauseUI
        buttons[11].onClick.AddListener(() => PauseMenu.instance.Resume());     //resume
        buttons[12].onClick.AddListener(() => SwitchUI(OptionUI));              //option
        buttons[13].onClick.AddListener(() => BackToMainMenu());       //backOfPause - MainMenu
        //ModeUI
        buttons[14].onClick.AddListener(() => NewGame());               //new game
        buttons[15].onClick.AddListener(() => ContinueGame());          //continue
        buttons[16].onClick.AddListener(() => BackToUI(PlayUI));        //backOfMode - Play
        //SoundUI
        sliders[0].onValueChanged.AddListener(UpdateMusicVolume);       //slider music
        sliders[1].onValueChanged.AddListener(UpdateSFXVolume);         //slider sound
        buttons[6].onClick.AddListener(() => BackToUI(OptionUI));       //backOfSound - Option

        foreach (var button in buttons)
        {
            AddEventTrigger(button.gameObject, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(button.gameObject, EventTriggerType.PointerClick, OnPointerClick);
        }
    }
    public void RemoveButtonEvent()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        foreach (var slider in sliders)
        {
            RemoveAllEventTriggers(slider);
        }
    }

    #region Event 
    #region Sound
    private void AddEventTrigger(GameObject target, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = target.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = eventType
        };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
    private void RemoveAllEventTriggers(Slider slider)
    {
        EventTrigger trigger = slider.GetComponent<EventTrigger>();

        if (trigger != null)
        {
            trigger.triggers.Clear();
        }
    }
    private void OnPointerEnter(BaseEventData data)
    {
        SoundManager.Instance.PlaySound2D("Hover");
    }
    private void OnPointerClick(BaseEventData data)
    {
        SoundManager.Instance.PlaySound2D("Click");
    }
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
    #endregion
    #region UI
    private void SwitchUI(GameObject newUI)
    {
        if (CurrentUI != null)
        {
            CurrentUI.SetActive(false); 
        }
        CurrentUI = newUI; 
        CurrentUI.SetActive(true); 
    }

    private void BackToUI(GameObject previousUI)
    {
        SwitchUI(previousUI);
    }

    #endregion
    #endregion

    #region Internal Function
    public static void SetPaused(bool value) => IsPaused = value;
    private void SetButtonBack()
    {
        if (_isSwitchButtonDone) return;
        if (!IsPlaying)
        {
            buttons[5].onClick.RemoveAllListeners();
            buttons[5].onClick.AddListener(() => BackToUI(PlayUI));  //backOfOption - Play
        }
        else
        {
            buttons[5].onClick.RemoveAllListeners();
            buttons[5].onClick.AddListener(() => BackToUI(PauseUI));  //backOfOption - Pause
        }
    }
    private void ResetMenuOnLoad()
    {
        IsPlaying = false;
        IsPaused = false;

        _isSwitchButtonDone = false;
    }
    #endregion

    #region MenuAction
    public void PauseGame()
    {
        SwitchUI(PauseUI);
        IsPaused = true;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        CurrentUI.SetActive(false);
        IsPaused = false;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("Player");
    }
    private void NewGame()
    {
        CurrentUI.SetActive(false);
        PlayStoryScene();
    }
    private void ContinueGame()
    {
        IsPlaying = true;
        _isSwitchButtonDone = false;

        CurrentUI.SetActive(false);
        PlayGameScene();
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SwitchToPlay()
    {
        SwitchUI(PlayUI);
    }
    public void BackToMainMenu()
    {
        CurrentUI.SetActive(false);

        ResetMenuOnLoad();

        SceneLoadingCtrl.instance.EnableLoading();
        SceneManager.LoadScene("MainMenu");
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    public void PlayStoryScene()
    {
        SceneManager.LoadSceneAsync("StoryScene");
        MusicManager.Instance.PlayMusic("Story");

        GameDataCtrl.Instance.NewGame();
    }
    public void PlayGameScene()
    {
        SceneLoadingCtrl.instance.EnableLoading();
        SceneManager.LoadScene("GameScene");
        MusicManager.Instance.PlayMusic("Theme");

    }
    public void PlayNewGame()
    {
        SceneLoadingCtrl.instance.EnableLoading();
        MusicManager.Instance.PlayMusic("Theme");
        IsPlaying = true;

    }
    #endregion
    #region Save

    public void LoadData(GameData gameData)
    {
        sliders[0].value = gameData.musicVolume;
        sliders[1].value = gameData.sfxVolume;
        UpdateMusicVolume(gameData.musicVolume);
        UpdateSFXVolume(gameData.sfxVolume);
        graphic.LoadGraphicsState(gameData.graphicsQuality); // Load graphics state
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.musicVolume = sliders[0].value;
        gameData.sfxVolume = sliders[1].value;
        gameData.graphicsQuality = graphic.GetCurrentGraphicsQuality(); // Save graphics state
    }
    #endregion
}

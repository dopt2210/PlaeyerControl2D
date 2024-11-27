using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IGameData
{

    //    playButton = buttons[0];
    //    optionButton = buttons[1];
    //    exitButton = buttons[2];
    //    graphicButton = buttons[4];
    //    volumeButton = buttons[3];
    //    backOfOptionButton = buttons[5];
    //    backOfSoundButton = buttons[6];
    //    graphicHigh = buttons[7];
    //    graphicMedium = buttons[8];
    //    graphicLow = buttons[9];
    //    backOfGraphicButton = buttons[10];

    //    musicSlider = sliders[0];
    //    sfxSlider = sliders[1];

    public static MenuManager instance {  get; private set; }
    public Graphic graphic;
    public AudioMixer audioMixer;
    public GameObject CurrentUI { get; private set; }
    public GameObject PauseUI {  get; private set; }
    public GameObject PlayUI {  get; private set; }
    public GameObject OptionUI {  get; private set; }
    public GameObject GraphicUI {  get; private set; }
    public GameObject SoundUI {  get; private set; }

    [SerializeField] private Button[] buttons;
    [SerializeField] private Slider[] sliders;
    public bool IsPaused { get; private set; } = false;
    public bool IsPlaying { get; set; }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        IsPlaying = false;
        LoadComponent();
    }
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
        graphic = FindObjectOfType<Graphic>();
        CurrentUI = PlayUI;

        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(false);
        }

        SetButtonEvent();
    }
    private void LoadComponent()
    {
        PauseUI = transform.GetChild(4).gameObject;
        PlayUI = transform.GetChild(0).gameObject;
        OptionUI = transform.GetChild(1).gameObject;
        GraphicUI = transform.GetChild(2).gameObject;
        SoundUI = transform.GetChild(3).gameObject;

        buttons = transform.GetComponentsInChildren<Button>();
        sliders = transform.GetComponentsInChildren<Slider>();
    }
    public void SetButtonEvent()
    {
        RemoveButtonEvent();

        foreach (var slider in sliders)
        {
            RemoveAllEventTriggers(slider);
        }

        buttons[0].onClick.AddListener(OnPlayButtonClicked);
        buttons[1].onClick.AddListener(() => SwitchUI(OptionUI));
        buttons[2].onClick.AddListener(() => OnExitButtonClicked());

        buttons[3].onClick.AddListener(() => SwitchUI(SoundUI));
        buttons[4].onClick.AddListener(() => SwitchUI(GraphicUI));

        if(!IsPlaying) buttons[5].onClick.AddListener(() => BackToUI(PlayUI));
        else buttons[5].onClick.AddListener(() => BackToUI(PauseUI));

        buttons[6].onClick.AddListener(() => BackToUI(OptionUI));
        buttons[10].onClick.AddListener(() => BackToUI(OptionUI));

        buttons[7].onClick.AddListener(graphic.OnHighButtonClick);
        buttons[8].onClick.AddListener(graphic.OnMediumButtonClick);
        buttons[9].onClick.AddListener(graphic.OnLowButtonClick);

        buttons[11].onClick.AddListener(() => PauseMenu.instance.Resume());
        buttons[12].onClick.AddListener(() => SwitchUI(OptionUI));
        buttons[13].onClick.AddListener(() => PauseMenu.instance.Quit());

        sliders[0].onValueChanged.AddListener(UpdateMusicVolume);
        sliders[1].onValueChanged.AddListener(UpdateSFXVolume);

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
    private void OnPlayButtonClicked()
    {
        CurrentUI.SetActive(false);
        MainMenu.instance.PlayStoryScene();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
    #endregion
    #endregion

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void PauseGame()
    {
        SwitchUI(PauseUI);
        IsPaused = true;
        Time.timeScale = 0f;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("Player");
    }
    public void BackToMenu()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SwitchUI(PlayUI);
    }
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    private TextMeshProUGUI flashText;
    private GameObject pressKeyText;
    private bool isFlash = true;
    [Header("Scene to load")]
    [SerializeField] private SceneField playerScene;
    [SerializeField] private SceneField mapScene;
    [SerializeField] private SceneField storyScene;

	private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
    }
    private void Start()
    {
        pressKeyText = transform.GetChild(1).gameObject;
        flashText = pressKeyText.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(FlashText());
    }
    private void Update()
    {
        OpenMenuByPressKey();
    }
    public void Play()
    {
  //      if (Keyboard.current.eKey.wasPressedThisFrame)
  //      {
		//	sceneToLoad.Add(SceneManager.LoadSceneAsync(playerScene));
		//	sceneToLoad.Add(SceneManager.LoadSceneAsync(mapScene, LoadSceneMode.Additive));
		//	MusicManager.Instance.PlayMusic("Boss");
		//	MenuManager.instance.IsPlaying = true;
		//	MenuManager.instance.SetButtonEvent();
		//}
		sceneToLoad.Add(SceneManager.LoadSceneAsync(playerScene));
		sceneToLoad.Add(SceneManager.LoadSceneAsync(mapScene, LoadSceneMode.Additive));
		MusicManager.Instance.PlayMusic("Boss");
		MenuManager.instance.IsPlaying = true;
		MenuManager.instance.SetButtonEvent();

	}
    private IEnumerator FlashText()
    {
        while (isFlash)
        {
            pressKeyText.SetActive(!pressKeyText.activeSelf);
            flashText.color = Color.green;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void OpenMenuByPressKey()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame && isFlash)
        {
            isFlash = false;
            pressKeyText.gameObject.SetActive(false);
            MenuManager.instance.PlayGame();
        }    
            
    }

    public void PlayStoryScene()
    {
		SceneManager.LoadSceneAsync(playerScene);
	}
}
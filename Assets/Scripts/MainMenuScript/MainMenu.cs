using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    [SerializeField] private SceneField storyScene;
    private TextMeshProUGUI flashText;
    private GameObject pressKeyText;
    private bool isFlash = true;
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
		SceneManager.LoadSceneAsync(storyScene);
        MusicManager.Instance.PlayMusic("Story");
	}
}
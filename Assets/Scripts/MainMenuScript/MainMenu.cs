using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator flashText;
    private bool isFlash = true;
    private void OnEnable()
    {
        isFlash = true;
    }
    private void Update()
    {
        OpenMenuByPressKey();
        flashText.gameObject.SetActive(isFlash);
    }

    private void OpenMenuByPressKey()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame && isFlash)
        {
            isFlash = false;
            flashText.gameObject.SetActive(false);
            MenuManager.instance.SwitchToPlay();
        }    
    }
}
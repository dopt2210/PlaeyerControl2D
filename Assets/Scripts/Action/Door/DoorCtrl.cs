using UnityEngine;

public class DoorCtrl : MonoBehaviour, IInteractable
{
    public enum DoorToSpawn
    {
        None,
        One,
        Two,
        Three
    }
    public static DoorCtrl Instance { get; private set; }
    public bool IsInteractable { get; set; }
    public GameObject PlayerGameObject {  get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Only allowed");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (PlayerCtrl.instance.InteractDown) IsInteractable = true;
    }

    public void Interact() { }
    public void DisableInteract() => IsInteractable = false;
}

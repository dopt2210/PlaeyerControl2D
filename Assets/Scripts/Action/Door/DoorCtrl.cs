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
    public bool IsInteractable { get => PlayerCtrl.instance.InteractDown; set { } }
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


    public void Interact()
    {
        if(PlayerCtrl.instance.InteractDown) 
        Debug.Log("in house");
    }
}

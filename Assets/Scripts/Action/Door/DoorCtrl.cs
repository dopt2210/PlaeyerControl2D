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
    public bool _isContactAble { get; private set; }
    #region Interface vars
    public bool IsInteractable { get => PlayerCtrl.instance.InteractDown; set { } }
    public GameObject PlayerGameObject {  get; set; }
    #endregion
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Only allowed");
            return;
        }
        Instance = this;
    }
    
    public void Interact() { }
    public void DisableInteract() { }
    public void SetRangeInteract(bool value) => _isContactAble = value;
}

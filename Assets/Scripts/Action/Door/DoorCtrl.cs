using System.Collections;
using UnityEngine;

public class DoorCtrl : MonoBehaviour, IInteractable
{
    public static DoorCtrl Instance { get; private set; }
    public enum DoorToSpawn
    {
        None,
        One,
        Two,
        Three
    }

    [SerializeField] private Collider2D[] doors; 
    [SerializeField] private Collider2D _playerCol;
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
    private void Start()
    {
        PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerCol = PlayerGameObject.GetComponent<Collider2D>();
    }
    public void TeleportPlayer(Collider2D fromDoor, Collider2D toDoor)
    {
        float colHeight = _playerCol.bounds.extents.y;
        Vector3 targetPosition = toDoor.transform.position - new Vector3(0f, colHeight, 0f);

        Debug.Log($"Teleporting player to {toDoor.name}. Target position: {targetPosition}");

        PlayerGameObject.transform.position = targetPosition;

        Debug.Log($"Player teleported to {PlayerGameObject.transform.position}");
    }

    public void Interact() { }
    public void DisableInteract() { }
    public void SetRangeInteract(bool value) => _isContactAble = value;
}

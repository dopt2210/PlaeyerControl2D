using UnityEngine;

public class NpcCtrl : MonoBehaviour, IInteractable
{
    public static NpcCtrl Instance { get; private set; }
    public GameObject PlayerGameObject { get; set; }
    public bool IsInteractable { get => PlayerCtrl.instance.InteractDown; set { } }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }
    private void Start()
    {
        //PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void OutRangeInteract() { }

    public virtual void InRangeInteract() { }

    public virtual void CheckInteractable() { }

    public virtual void Interact() { }
    public virtual void DisableInteract() { }
}

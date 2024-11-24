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

    public virtual void OutRangeInteract() { }

    public virtual void InRangeInteract() { }

    public virtual void CheckInteractable() { }

    public void Interact() { }

    public void DisableInteract() { }

    public void SetRangeInteract(bool value) { }
}

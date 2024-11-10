using UnityEngine;

public class DoorOpen : Action
{
    [Header("Current door")]
    public DoorCtrl.DoorToSpawn currentDoorPosition;
    [Header("Spawn to door")]
    [SerializeField] private DoorCtrl.DoorToSpawn _doorToSpawn;
    [Header("Spawn to scene")]
    [SerializeField] private SceneField[] _sceneLoad;
    [SerializeField] private SceneField[] _sceneUnload;

    public override void UpdateAct()
    {
        Interact();
    }
    
    public void Interact()
    {
        if(DoorCtrl.Instance.IsInteractable)
        {
            SceneCtrl.Instance.UnloadScene(_sceneUnload);
            SceneCtrl.Instance.LoadScene(_sceneLoad);
            DoorCtrl.Instance.DisableInteract();
            SceneCtrl.SwapFromDoorUse(_doorToSpawn);
        }
    }

}

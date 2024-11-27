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


    public override void Act()
    {
        DoorCtrl.Instance.SetRangeInteract(true);
    }
    public override void CancelAct()
    {
        DoorCtrl.Instance.SetRangeInteract(false);
    }

    public void OpenTheDoor()
    {
        SceneCtrl.Instance.LoadScene(_sceneLoad);
        SceneCtrl.Instance.UnloadScene(_sceneUnload);
        SceneCtrl.SwapFromDoorUse(_doorToSpawn);

    }

    private void Update()
    {
        if (DoorCtrl.Instance.IsInteractable && DoorCtrl.Instance._isContactAble)
        {
            OpenTheDoor();
        }
    }

}

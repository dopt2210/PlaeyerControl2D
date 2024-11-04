using UnityEngine;

public class DoorOpen : Action
{
    [Header("Spawn to")]
    [SerializeField] private DoorCtrl.DoorToSpawn _doorToSpawn;
    [SerializeField] private SceneField _sceneFieldToLoad;
    [Header("This door")]
    public DoorCtrl.DoorToSpawn currentDoorPosition;

    public override void UpdateAct()
    {
        Interact();
    }
    
    public void Interact()
    {
        if (PlayerCtrl.instance.InteractDown)
        {
            Debug.Log("Open");
            SceneCtrl.SwapSceneFromDoorUse(_sceneFieldToLoad, _doorToSpawn);
        }
    }

}

using UnityEngine;

public class SceneLoad : Action
{
    [SerializeField] private SceneField[] _sceneLoad;
    [SerializeField] private SceneField[] _sceneUnload;

    public override void Act()
    {
        SceneCtrl.Instance.LoadScene(_sceneLoad);
        SceneCtrl.Instance.UnloadScene(_sceneUnload);
    }

    
}

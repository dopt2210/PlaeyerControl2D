using UnityEngine;

public class TriggerActionCtrl : MonoBehaviour 
{
    private void Awake()
    {
        trigger = transform.GetComponentInChildren<Trigger>();
        action = transform.GetComponentInChildren<Action>();
    }

    public Trigger trigger;

    public Action action;

}

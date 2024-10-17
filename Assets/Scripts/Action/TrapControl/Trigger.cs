using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerActionCtrl triggerActionCtrl;

    private void Awake()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerActionCtrl.action.Act();
    }
}

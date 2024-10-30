using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public TriggerActionCtrl triggerActionCtrl;
    public virtual void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
    }
    public virtual void Act() { }
    public virtual void CancelAct() { }
}

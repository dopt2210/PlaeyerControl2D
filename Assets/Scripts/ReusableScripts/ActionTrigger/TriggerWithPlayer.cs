using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWithPlayer : Trigger
{
    public CapsuleCollider2D col;
    protected override void Awake()
    {
        base.Awake();
        col = transform.GetComponent<CapsuleCollider2D>();

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggerActionCtrl.triggerAndAction.TryGetValue(this, out Action action))
            {
                action.Act();
            }
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggerActionCtrl.triggerAndAction.TryGetValue(this, out Action action))
            {
                action.CancelAct();
            }
        }
    }
    public void SetRadiusTrigger(Vector2 size, Vector2 offset)
    {
        if(col == null) return;
        col.size = size;
        col.offset = offset;
    }
}

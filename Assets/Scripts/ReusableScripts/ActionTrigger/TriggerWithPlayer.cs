using UnityEngine;

//[RequireComponent (typeof(CapsuleCollider2D))]
public class TriggerWithPlayer : Trigger
{
    public CapsuleCollider2D col;
    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider2D>();
        if (col == null) return;
        col.isTrigger = true;

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
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggerActionCtrl.triggerAndAction.TryGetValue(this, out Action action))
            {
                action.UpdateAct();
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
        if (col == null)
        {
            Debug.Log("Not found coll");
            return;
        }
        col.size = size;
        col.offset = offset;
    }
}

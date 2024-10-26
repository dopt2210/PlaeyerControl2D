using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWithPlayer : Trigger
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var action in triggerActionCtrl.actions)
                action.Act();
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var action in triggerActionCtrl.actions)
                action.CancelAct();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcIdleState : NpcState
{
    private SpriteRenderer _targetSprite;
    public NpcIdleState(Npc npc): base(npc, "Idle", Npc.NpcStateEnum.Idle)
    {
        _targetSprite = GameObject.FindGameObjectWithTag("Npc")
            .transform.Find("Interact_Image").GetComponentInChildren<SpriteRenderer>();
    }

    public override void AnimationTriggerEvent(Npc.NpcStateEnum npcStateEnum) { }

    public override void EnterState()
    {
        npc.SetAnimation(anim, true);
    }

    public override void ExitState()
    {
        npc.SetAnimation(anim, false);
    }

    public override void LogicUpdate()
    {
        if (npc.IsInteractable && npc._isTalkable)
        {
            npc.Interact();
        }
        else if (!npc._isTalkable)
        {
            npc.DisableInteract();
        }
        if (_targetSprite.gameObject.activeSelf && !npc._isTalkable)
        {
            _targetSprite.gameObject.SetActive(false);
        }
        else if (!_targetSprite.gameObject.activeSelf && npc._isTalkable)
        {
            _targetSprite.gameObject.SetActive(true);
        }
    }

    public override void PhysicsUpdate() { }
}

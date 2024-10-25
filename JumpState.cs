using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbilityState
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Player Jump State Enter");
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("PJS Tick");
    }

    public override void Exit()
    {
        Debug.Log("pjs Exit");    
    }
}

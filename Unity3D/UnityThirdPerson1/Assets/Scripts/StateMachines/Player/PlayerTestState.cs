using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer = 5f;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        Debug.Log("Enter");
        stateMachine.InputReader.JumpEvent += OnJump;

    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick");
        timer -= deltaTime;
        Debug.Log(timer);
        // if (timer <= 0)
        // {
        //     stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        // }
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    public override void Exit()
    {
        Debug.Log("Exit");
        stateMachine.InputReader.JumpEvent -= OnJump;

    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Locomotion = Animator.StringToHash("Locomotion");
    
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;


    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    { 
        stateMachine.Animator.CrossFadeInFixedTime(Locomotion,CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        stateMachine.Animator.SetFloat(Speed,0,AnimatorDampTime,deltaTime);

    }

    public override void Exit()
    {
        
    }
}

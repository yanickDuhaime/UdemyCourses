using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Locomotion = Animator.StringToHash("Locomotion");
    
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(Locomotion,CrossFadeDuration);
        Debug.Log("Entered chase state");
    }

    public override void Tick(float deltaTime)
    {
        moveToPlayer(deltaTime);
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.NavMeshAgent.ResetPath();
        stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }

    private void moveToPlayer(float deltaTime)
    {
         stateMachine.NavMeshAgent.destination = stateMachine.Player.transform.position;
         Move(stateMachine.NavMeshAgent.desiredVelocity.normalized * stateMachine.MovementSpeed,deltaTime);
         stateMachine.NavMeshAgent.velocity = stateMachine.CharacterController.velocity;
    }
}

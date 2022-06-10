using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class PlayerFreeLookState : PlayerBaseState
{
    private static readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
    private static readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        stateMachine.Animator.Play(FreeLookBlendTree);
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        
        Vector3 movement = CalculateMovement();
        //stateMachine.transform.Translate(movement * deltaTime);
        Move(movement * stateMachine.FreeLookMovementSpeed , deltaTime);

        if (MovementAnimation(deltaTime)) return;
        
        FaceMovementDirection(movement,deltaTime);
    }



    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;

    }
    
    
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        
        forward.y = 0;
        right.y = 0;
        
        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    private bool MovementAnimation(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeed, 0, AnimatorDampTime, deltaTime);
            return true;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeed, 1, AnimatorDampTime, deltaTime);
        return false;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}

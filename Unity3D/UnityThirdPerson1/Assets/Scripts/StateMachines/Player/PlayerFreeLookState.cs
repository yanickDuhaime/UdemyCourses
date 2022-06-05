using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private static readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        //stateMachine.transform.Translate(movement * deltaTime);
        stateMachine.CharacterController.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (MovementAnimation(deltaTime)) return;
        
        FaceMovementDirection(movement,deltaTime);
    }



    public override void Exit()
    {

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;
    private bool alreadyApliedForce;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,attack.TransitionDuration);

    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime();
        Move(deltaTime);
        FaceTarget();
        
        //if the animation has finished
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            //Go back to locomotionState
            if (stateMachine.Targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        
        
       

        previousFrameTime = normalizedTime;
    }

   

    public override void Exit()
    {
        
    }

    private float GetNormalizedTime()
    {
      AnimatorStateInfo currentInfo =  stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
      AnimatorStateInfo nextInfo =  stateMachine.Animator.GetNextAnimatorStateInfo(0);
        //If we are transitioning to an attack
      if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
      {
          return nextInfo.normalizedTime;
      }
      else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
      {
          return currentInfo.normalizedTime;
      }
      else
      {
          return 0;
      }
    } 
    
    private void TryComboAttack(float normalizedTime)
    {
        //if the attack can't combo
        if (attack.ComboStateIndex == -1) return;
        //If we haven't reached the point in the animation to transition to the next attack
        if (normalizedTime < attack.ComboAttackTime) return;
        alreadyApliedForce = false;
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine,attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (alreadyApliedForce) return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyApliedForce = true;
    }
}

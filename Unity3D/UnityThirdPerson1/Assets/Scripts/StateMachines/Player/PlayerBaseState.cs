using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class PlayerBaseState : State
{
   protected PlayerStateMachine stateMachine;

   public PlayerBaseState(PlayerStateMachine stateMachine)
   {
      this.stateMachine = stateMachine;
   }

   protected void Move(Vector3 motion, float deltaTime)
   {
      stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
   }

   protected void FaceTarget(/*Vector3 target,float deltaTime*/)
   {
      if (stateMachine.Targeter.currentTarget == null) return;
      
      Vector3 lookPos = stateMachine.Targeter.currentTarget.transform.position - stateMachine.transform.position;
      lookPos.y = 0f;
      stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
      //stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,Quaternion.LookRotation(target), deltaTime * stateMachine.RotationDamping);
      
   }
}

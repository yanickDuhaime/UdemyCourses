using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public CharacterController CharacterController { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    [field:SerializeField] public Targeter Targeter { get; private set; }
    [field:SerializeField] public ForceReceiver ForceReceiver { get; private set; }


    [field:SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field:SerializeField] public float RotationDamping { get; private set; }
    
    public Transform MainCameraTransform { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
        //StartCoroutine(SwitchStateCoroutine());
    }

    IEnumerator SwitchStateCoroutine()
    {
        SwitchState(new PlayerFreeLookState(this));
        yield return new WaitForSeconds(1f);
        SwitchState(new PlayerJumpState(this));
    }
}

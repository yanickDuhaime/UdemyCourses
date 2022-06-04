using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [field:SerializeField] public InputReader InputReader { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerTestState(this));
        //StartCoroutine(SwitchStateCoroutine());
    }

    IEnumerator SwitchStateCoroutine()
    {
        SwitchState(new PlayerTestState(this));
        yield return new WaitForSeconds(1f);
        SwitchState(new PlayerJumpState(this));
    }
}

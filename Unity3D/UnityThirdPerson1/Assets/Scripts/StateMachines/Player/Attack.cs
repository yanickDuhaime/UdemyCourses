using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
    //How far along an animation has to be to go to the next one
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    //When the force is applied in the animation
    [field: SerializeField] public float ForceTime { get; private set; }
    //Intensity of the force
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public int Damage { get; private set; } = 10;

}

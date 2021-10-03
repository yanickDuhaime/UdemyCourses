using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hit")) return;
        score++;
        Debug.Log($"You've bumped into a thing {score} times");
    }
}

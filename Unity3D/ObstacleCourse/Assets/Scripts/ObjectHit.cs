using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         Debug.Log("Wall hit");
         gameObject.tag = "Hit";
         GetComponent<MeshRenderer>().material.color = Color.magenta;
      }

      
   }
}

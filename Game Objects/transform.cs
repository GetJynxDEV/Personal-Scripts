using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    [SerializeField] private GameObject currentTransform;
    [SerializeField] private RigidBody2D rb;
    public float force; //How fast the Object go

    private void newTransform()
    {
      //Will get the current transform of the Game Object,
      Vector3 direction = currentTransform.transform.position;
      
      //Will get the transform but can modify X or Y value of the currentTransform
      Vector3 direction = new Vector3(currentTransform.transform.position.x, 0.741016f) - transform.position;

      //Game Object Rigid Body will head towards the direction transform at a normalized phase using the Force Value as the speed
      rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;
    }    
}


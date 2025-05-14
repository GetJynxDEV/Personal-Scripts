using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    [SerializeField] private float distance;                 //Distance between Object and Layer Mask
    [SerializeField] private Transform raycastTransform;     //Reference an Object
    [SerializeField] private LayerMask mask;                 //Reference a Layermask

    private void collisionCheck()
    {
      if (Physics.Raycast(transform.position, Vector3.down, distance, mask))
      {
          Debug.Log("Raycast detected mask");
          //Logic here
      }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform target;
   public float smooth;
   private Vector3 offset;

   private void Awake()
   {
       offset = transform.position - target.position;
   }

   void Update()
    {
        transform.position = Vector3.Lerp(transform.position,  target.position + offset, smooth * Time.deltaTime);
    }
}

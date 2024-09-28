using System;
using UnityEngine;

public class SphericalCam : MonoBehaviour 
{
     public float MinDist, CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV, MouseXStrength, MouseYStrength;
     public Transform Target;

     public void Update()
     {
         AngleH += Input.GetAxis("Mouse X") * MouseXStrength;
         AngleV -= Input.GetAxis("Mouse Y") * MouseYStrength;
         CurrentDist += Input.GetAxis("Mouse ScrollWheel");
     }

     public void LateUpdate()
     {
         Vector3 tmp;
         tmp.x = Mathf.Cos(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + Target.position.x;
         tmp.z = Mathf.Sin(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + Target.position.z;
         tmp.y = Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + Target.position.y;
         transform.position = Vector3.Slerp(transform.position, tmp, TranslateSpeed * Time.deltaTime);
         transform.LookAt(Target);
     }
 }
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static List<Gravity> otherObj;
    Rigidbody rb;
    const float G = 0.006674f; //Gravitational Constant 6.674
   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObj == null)
        {
            otherObj = new List<Gravity>();
        }
        otherObj.Add(this);
    }

    
    void FixedUpdate()
    {
        foreach (Gravity obj in otherObj)
        {
            if (obj != this) //กันไม่ให้โดนแรงดึงดูดของตัวเอง
            {
                AttractForce(obj);
            }
        }
    }

    void AttractForce(Gravity other)
    {
        Rigidbody otherRb = other.rb; // ดึงค่ามวล m
        Vector3 direction = rb.position - otherRb.position; // ทิศทางจากวัตถุ M ไป m

        float distance = direction.magnitude; // หาระยะห่าง r
        if (distance == 0f) return; // กันไม่ให้มีแรงดึงดูดเมื่อวัตถุอยู่ตำแหน่งเดียวกัน

        //F = G(m1 * m2) /r^2
        float forceMagnitude = G * (rb.mass - otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationalForce = forceMagnitude * direction.normalized; //นำแรงที่ได้มาใส่ทิศทาง 1,-1
        otherRb.AddForce(gravitationalForce); //ใส่แรงดึงดูดพร้อมทิศทางให้กับวัตถุ

    }
}

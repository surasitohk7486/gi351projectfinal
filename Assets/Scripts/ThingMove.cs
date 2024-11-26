using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingMove : MonoBehaviour
{
    public Transform targetObject; // วัตถุที่ต้องการให้เคลื่อนที่
    public Vector3 moveDirection = Vector3.forward; // ทิศทางการเคลื่อนที่ (ค่าตั้งต้นไปข้างหน้า)
    public float moveDistance = 5f; // ระยะทางที่วัตถุจะเคลื่อนที่
    public float moveSpeed = 2f; // ความเร็วการเคลื่อนที่

    private Vector3 startPosition; // ตำแหน่งเริ่มต้นของวัตถุ
    private bool isMoving = false; // ตรวจสอบว่าวัตถุกำลังเคลื่อนที่หรือไม่

    void Start()
    {
        // บันทึกตำแหน่งเริ่มต้นของวัตถุ
        if (targetObject != null)
        {
            startPosition = targetObject.position;
        }
        else
        {
            Debug.LogError("Target Object is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่า Player เป็นผู้ชน
        if (other.CompareTag("Player"))
        {
            isMoving = true; // เริ่มการเคลื่อนที่
        }
    }

    void Update()
    {
        if (isMoving && targetObject != null)
        {
            // คำนวณตำแหน่งถัดไป
            targetObject.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

            // ตรวจสอบว่าถึงระยะทางที่กำหนดหรือยัง
            if (Vector3.Distance(startPosition, targetObject.position) >= moveDistance)
            {
                isMoving = false; // หยุดการเคลื่อนที่
            }
        }
    }
}

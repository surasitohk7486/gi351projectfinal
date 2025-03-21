﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI; // UI สำหรับการอินเทอร์แอค
    [SerializeField] private LayerMask doorLayer; // เลเยอร์ที่ใช้สำหรับตรวจจับประตู
    [SerializeField] private Camera playerCamera; // กล้องของผู้เล่น
    [SerializeField] private float interactionDistance = 5f; // ระยะการตรวจจับ
    [SerializeField] private bool doorIsOpen = false;
    [SerializeField] private AudioSource audio;

    private bool isLookingAtDoor = false; // เช็คว่ากำลังมองที่ประตูหรือไม่
    private GameObject currentDoor; // ประตูที่กำลังมองอยู่

    void Update()
    {
        // สร้าง Raycast จากกล้องของผู้เล่น
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, doorLayer))
        {
            if (hit.collider.CompareTag("Door") && !doorIsOpen) // ตรวจสอบว่าชนกับประตู
            {
                isLookingAtDoor = true; // กำลังมองที่ประตู
                currentDoor = hit.collider.gameObject; // เก็บ GameObject ของประตูที่มองอยู่
                interactionUI.SetActive(true); // แสดง UI

                if (Input.GetKeyDown(KeyCode.E)) // กด E เพื่อเปิดประตู
                {
                    OpenDoor(currentDoor); // เปิดประตูที่กำลังมองอยู่
                }
            }
        }

        else
        {
            isLookingAtDoor = false; // ไม่มองที่ประตู
            currentDoor = null; // รีเซ็ตตัวแปรประตู
            interactionUI.SetActive(false); // ซ่อน UI
        }
    }

    public void OpenDoor(GameObject door)
    {
        Animator doorAnimator = door.GetComponent<Animator>(); // ดึง Animator จากประตู

        if (doorAnimator != null)
        {
            audio.Play();
            doorIsOpen = true;
            doorAnimator.SetBool("Open", true); // เล่นแอนิเมชันที่ตั้งไว้ใน Animator
            Debug.Log("Door opened: " + door.name);
        }

        else
        {
            Debug.LogWarning("No Animator found on the door: " + door.name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteract : MonoBehaviour
{
    public Camera playerCamera; // กล้องของผู้เล่น
    public float interactionDistance = 5f; // ระยะที่สามารถตรวจจับได้
    public LayerMask noteLayer; // เลเยอร์สำหรับกระดาษโน้ต

    public GameObject interactionUI; // UI แสดงข้อความ "กด E เพื่ออ่าน"
    public GameObject noteUI; // UI กระดาษโน้ต
    public FirstPersonController playerController; // ตัวควบคุมผู้เล่น (เพื่อหยุดการเคลื่อนที่)
    public FlashLight flashlightController; // ตัวควบคุมไฟฉาย

    public AudioSource interactSound; // เสียงเมื่อกดปุ่ม

    private bool isLookingAtNote = false; // ตรวจสอบว่าผู้เล่นกำลังมองกระดาษอยู่หรือไม่
    private bool isReading = false; // ตรวจสอบว่าผู้เล่นกำลังอ่านโน้ตอยู่

    void Start()
    {
        interactionUI.SetActive(false);
        noteUI.SetActive(false);
    }

    void Update()
    {
        if (!isReading)
        {
            // Raycast จากกล้องของผู้เล่น
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, noteLayer))
            {
                isLookingAtNote = true;
                interactionUI.SetActive(true); // แสดง UI "กด E เพื่ออ่าน"

                if (Input.GetKeyDown(KeyCode.E)) // กด E เพื่ออ่าน
                {
                    PlayInteractSound(); // เล่นเสียง
                    StartReading();
                }
            }
            else
            {
                isLookingAtNote = false;
                interactionUI.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E)) // กด E อีกครั้งเพื่อปิด
            {
                PlayInteractSound(); // เล่นเสียง
                StopReading();
            }
        }
    }

    private void StartReading()
    {
        isReading = true;
        noteUI.SetActive(true); // แสดง UI กระดาษโน้ต
        interactionUI.SetActive(false); // ซ่อน UI "กด E เพื่ออ่าน"
        playerController.enabled = false; // ปิดการควบคุมผู้เล่น

        // ปิดการควบคุมไฟฉาย
        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(true);
        }
    }

    private void StopReading()
    {
        isReading = false;
        noteUI.SetActive(false); // ซ่อน UI กระดาษโน้ต
        playerController.enabled = true; // เปิดการควบคุมผู้เล่นอีกครั้ง

        // เปิดการควบคุมไฟฉาย
        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(false);
        }
    }

    private void PlayInteractSound()
    {
        if (interactSound != null)
        {
            interactSound.Play(); // เล่นเสียง
        }
    }
}

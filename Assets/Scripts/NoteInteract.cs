using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteract : MonoBehaviour
{
    public Camera playerCamera; // กล้องของผู้เล่น
    public float interactionDistance = 5f; // ระยะตรวจจับ
    public LayerMask noteLayer; // เลเยอร์ของโน้ต

    public GameObject interactionUI; // UI "กด E เพื่ออ่าน"
    public FirstPersonController playerController; // ตัวควบคุมผู้เล่น
    public FlashLight flashlightController; // ตัวควบคุมไฟฉาย
    public AudioSource interactSound; // เสียงตอนกดปุ่ม

    private bool isReading = false; // ตรวจสอบว่าผู้เล่นกำลังอ่านอยู่
    private GameObject currentNoteUI; // UI ของโน้ตที่แสดงผลอยู่

    void Start()
    {
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (!isReading)
        {
            CheckForNoteInteraction();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StopReading();
        }
    }

    private void CheckForNoteInteraction()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, noteLayer))
        {
            interactionUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayInteractSound();

                // รับข้อมูล NoteData จากโน้ตที่ตรวจจับได้
                Note note = hit.collider.GetComponent<Note>();
                if (note != null)
                {
                    StartReading(note.noteData);
                }
            }
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }

    private void StartReading(NoteData noteData)
    {
        isReading = true;
        interactionUI.SetActive(false);

        // แสดง UI ของโน้ต
        currentNoteUI = Instantiate(noteData.noteUIPrefab);

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            currentNoteUI.transform.SetParent(canvas.transform, false); // ใช้ false เพื่อรักษา scale เดิม
        }
        else
        {
            Debug.LogWarning("Canvas not found. Ensure there is a Canvas in the scene.");
        }

        playerController.enabled = false;

        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(true);
        }
    }

    private void StopReading()
    {
        PlayInteractSound();
        isReading = false;
        Destroy(currentNoteUI); // ทำลาย UI โน้ตปัจจุบัน
        playerController.enabled = true;

        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(false);
        }
    }

    private void PlayInteractSound()
    {
        if (interactSound != null)
        {
            interactSound.Play();
        }
    }
}

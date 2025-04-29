using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

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

    private int count1 = 0;
    private int count2 = 0;
    private int count3 = 0;
    private int count4 = 0;
    private int count5 = 0;
    private int count6 = 0;

    void Start()
    {
        interactionUI.SetActive(false);
        Initialize();
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

        Debug.Log(currentNoteUI);

        if(noteData.noteTitle == "1" && count1 == 0)
        {
            Debug.Log("Note1");
            NotesInGame("1",flashlightController.idPlayer,1);
            count1++;
        }

        if (noteData.noteTitle == "2" && count2 == 0)
        {
            Debug.Log("Note2");
            NotesInGame("2", flashlightController.idPlayer, 1);
            count2++;
        }

        if (noteData.noteTitle == "3" && count3 == 0)
        {
            Debug.Log("Note3");
            NotesInGame("3", flashlightController.idPlayer, 1);
            count3++;
        }

        if (noteData.noteTitle == "4" && count4 == 0)
        {
            Debug.Log("Note4");
            NotesInGame("4", flashlightController.idPlayer, 1);
            count4++;
        }

        if (noteData.noteTitle == "5" && count5 == 0)
        {
            Debug.Log("Note5");
            NotesInGame("5", flashlightController.idPlayer, 1);
            count5++;
        }

        if (noteData.noteTitle == "6" && count6 == 6)
        {
            Debug.Log("Note6");
            NotesInGame("6", flashlightController.idPlayer, 1);
            count6++;
        }

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

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void NotesInGame(string numNotes, int numPlayer,float seeNotes)
    {
        CustomEvent notesInGame = new CustomEvent("NotesInGame")
        {
            {"NumNotes",numNotes},
            {"Player",numPlayer},
            {"SeeNotes",seeNotes}
        };

        AnalyticsService.Instance.RecordEvent(notesInGame);
        Debug.Log($"Recording Event NotesInGame {numNotes}");
    }
}

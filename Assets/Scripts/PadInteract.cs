using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadInteract : MonoBehaviour
{
    public FlashLight flashlightController;

    public string correctCode = "1234"; // รหัสที่ถูกต้อง
    public GameObject codeUI; // UI สำหรับใส่รหัส
    public TextMeshProUGUI codeDisplay; // Text แสดงรหัสที่ผู้เล่นกด
    public TextMeshProUGUI feedbackText; // ข้อความแจ้งเตือน (รหัสผิด)

    public FirstPersonController playerController; // ตัวควบคุมผู้เล่น
    public GameObject door; // ประตูที่จะเปิด
    private string currentInput = ""; // รหัสที่ผู้เล่นกำลังกรอก

    private bool isActive = false; // ตรวจสอบว่า UI กำลังเปิดอยู่หรือไม่
    private bool isLookingAtPad = false; // ตรวจสอบว่าผู้เล่นกำลังมองแพด

    public Camera playerCamera; // กล้องของผู้เล่น
    public float interactionDistance = 5f; // ระยะที่สามารถมองเห็นได้
    public LayerMask padLayer; // เลเยอร์สำหรับตรวจจับแพด

    void Start()
    {
        codeUI.SetActive(false); // ซ่อน UI ตอนเริ่มเกม
        feedbackText.gameObject.SetActive(false); // ซ่อนข้อความแจ้งเตือน
    }

    void Update()
    {
        // Raycast จากกล้องของผู้เล่น
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, padLayer))
        {
            isLookingAtPad = true; // ผู้เล่นกำลังมองแพด
            if (!isActive)
            {
                // แสดงข้อความ "กด E เพื่ออินเทอร์แอค"
                feedbackText.text = "Press E to interact";
                feedbackText.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !isActive)
            {
                OpenCodeUI(); // เปิด UI สำหรับใส่รหัส
            }
        }
        else
        {
            isLookingAtPad = false;
            feedbackText.gameObject.SetActive(false); // ซ่อนข้อความ "กด E เพื่ออินเทอร์แอค"
        }

        if (isActive && Input.GetKeyDown(KeyCode.Escape)) // กด E เพื่อปิด UI
        {
            CloseCodeUI();
        }
    }

    public void OpenCodeUI()
    {
        isActive = true;
        codeUI.SetActive(true); // เปิด UI
        currentInput = ""; // รีเซ็ตรหัสที่ผู้เล่นกรอก
        codeDisplay.text = ""; // ล้างหน้าจอ
        feedbackText.gameObject.SetActive(false); // ซ่อนข้อความแจ้งเตือน
        playerController.enabled = false; // ปิดการควบคุมผู้เล่น

        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(true);
        }

        Cursor.lockState = CursorLockMode.None; // ปลดล็อก Cursor
        Cursor.visible = true; // ทำให้ Cursor มองเห็นได้
        Debug.Log("Code UI opened successfully.");
    }

    public void CloseCodeUI()
    {
        isActive = false;
        codeUI.SetActive(false); // ซ่อน UI
        playerController.enabled = true; // เปิดการควบคุมผู้เล่นอีกครั้ง

        if (flashlightController != null)
        {
            flashlightController.LockFlashlight(false);
        }

        Cursor.lockState = CursorLockMode.Locked; // ล็อก Cursor ให้อยู่ตรงกลางหน้าจอ
        Cursor.visible = false; // ทำให้ Cursor หายไป
        Debug.Log("Code UI closed.");
    }

    public void AddDigit(string digit)
    {
        if (currentInput.Length <= 4) // จำกัดให้กรอกได้ 4 ตัว
        {
            currentInput += digit;
            codeDisplay.text = currentInput;
        }

        if (currentInput.Length == 4) // ตรวจสอบเมื่อครบ 4 ตัว
        {
            StartCoroutine(CheckCode());
        }
    }

    IEnumerator CheckCode()
    {
        if (currentInput == correctCode) // รหัสถูกต้อง
        {
            codeDisplay.text = "Correct!";
            codeDisplay.color = Color.green;
            

            yield return new WaitForSeconds(1f); // รอ 1 วินาที
            OpenDoor(); // เปิดประตู
        }
        else // รหัสผิด
        {
            codeDisplay.text = "Incorrect!";
            codeDisplay.color = Color.red;
            

            yield return new WaitForSeconds(1f); // แสดงข้อความแจ้งเตือน 3 วินาที
            
            currentInput = ""; // รีเซ็ตรหัส
            codeDisplay.text = "";
            codeDisplay.color = Color.black;
        }
    }

    public void OpenDoor()
    {
        // ตัวอย่าง: ซ่อนประตูหรือเปลี่ยนสถานะ
        door.SetActive(false);
        CloseCodeUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWin : MonoBehaviour
{
    public Camera playerCamera; // กล้องของผู้เล่น
    public float interactionDistance = 5f; // ระยะที่สามารถตรวจจับได้
    public LayerMask keyLayer; // เลเยอร์ของกุญแจ

    public GameObject interactionUI; // UI แสดงข้อความ "กด E เพื่อเก็บกุญแจ
    public AudioSource pickupSound; // เสียงเมื่อเก็บกุญแจ

    [SerializeField] private FirstPersonController firstPersonController;

    private void Start()
    {
        interactionUI.SetActive(false); // ซ่อน UI ตอนเริ่มเกม
    }

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // ยิง Ray จากตำแหน่งเมาส์
        RaycastHit hit;

        // ตรวจจับ Raycast เฉพาะในเลเยอร์กุญแจ
        if (Physics.Raycast(ray, out hit, interactionDistance, keyLayer))
        {
            interactionUI.SetActive(true); // แสดง UI

            if (Input.GetKeyDown(KeyCode.E)) // เมื่อผู้เล่นกด E
            {
                PickupKey(hit.collider.gameObject); // เก็บกุญแจ
            }
        }
        else
        {
            interactionUI.SetActive(false); // ซ่อน UI เมื่อไม่ได้มองกุญแจ
        }
    }

    private void PickupKey(GameObject key)
    {
        firstPersonController.hasKey = true; // ตั้งค่าสถานะว่ามีกุญแจ
        Destroy(key); // ลบวัตถุกุญแจออกจากฉาก
        interactionUI.SetActive(false); // ซ่อน UI

        if (pickupSound != null)
        {
            pickupSound.Play(); // เล่นเสียงเก็บกุญแจ
        }

        Debug.Log("Key picked up!"); // ข้อความใน Console
    }
}

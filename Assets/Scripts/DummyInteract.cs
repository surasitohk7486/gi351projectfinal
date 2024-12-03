using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // กล้องของผู้เล่น
    [SerializeField] private float interactionDistance = 2f; // ระยะที่สามารถตรวจจับได้
    [SerializeField] private LayerMask interactableLayer; // เลเยอร์สำหรับตรวจจับ Object ที่สามารถ Interact ได้
    [SerializeField] private GameObject interactionUI; // UI แสดงข้อความ "Press E to Interact"
    [SerializeField] private GameObject objectToActivate; // Object ที่ต้องการทำให้ Active
    [SerializeField] private GameObject objectToActivate2;
    [SerializeField] private GameObject objectToDeactivate; // Object ที่ต้องการให้หายไป (ซ่อนหรือทำลาย)
    [SerializeField] private AudioSource audio;
    [SerializeField] private BlinkLight addBlinkLight;
    [SerializeField] private Light light;

    int count = 0;

    private GameObject currentInteractableObject; // Object ที่ผู้เล่นกำลังมอง

    private void Start()
    {
        interactionUI.SetActive(false); // ซ่อน UI ตอนเริ่ม
    }

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // สร้าง Raycast จากตำแหน่งของเมาส์
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Dummy") && count == 0) // ตรวจสอบว่า Object ที่มองมีแท็กว่า "Interactable"
            {
                interactionUI.SetActive(true); // แสดง UI "Press E to Interact"
                currentInteractableObject = hit.collider.gameObject;

                if (Input.GetKeyDown(KeyCode.E)) // หากผู้เล่นกด E
                {   
                    InteractWithObjectAction(); // เรียกฟังก์ชัน Interact
                }
            }
        }
        else
        {
            interactionUI.SetActive(false); // ซ่อน UI หากไม่ได้มอง Object ที่สามารถ Interact
            currentInteractableObject = null;
        }
    }

    private void InteractWithObjectAction()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // ทำให้ Object ที่ตั้งไว้ Active
            objectToActivate2.SetActive(true);
            audio.Play();
            addBlinkLight.AddLight(light);
        }

        if (objectToDeactivate != null)
        {
            Destroy(objectToDeactivate); // ทำให้ Object ที่ตั้งไว้หายไป (ซ่อน)
            // หรือถ้าต้องการทำลาย Object ใช้: Destroy(objectToDeactivate);
        }

        interactionUI.SetActive(false); // ซ่อน UI หลังจากทำการ Interact

        count++;
    }
}

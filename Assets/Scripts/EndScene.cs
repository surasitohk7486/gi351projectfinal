using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    public float raycastDistance = 5f; // ระยะทางของ Raycast
    public string endSceneName = "EndScene"; // ชื่อฉากปลายทาง
    public TextMeshProUGUI interactText; // UI Text สำหรับแสดงข้อความ
    [SerializeField] private FirstPersonController firstPersonController;

    private bool canInteract = false; // ใช้ตรวจสอบว่าผู้เล่นอยู่ในระยะหรือไม่

    void Start()
    {
        // ซ่อนข้อความตอนเริ่มเกม
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;

        // ยิง Raycast เพื่อตรวจจับวัตถุ
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance))
        {
            // ตรวจสอบ Tag ของวัตถุ
            if (hit.collider.CompareTag("DoorEnd"))
            {
                canInteract = true;

                // แสดงข้อความบนหน้าจอ
                if (interactText != null)
                {
                    interactText.gameObject.SetActive(true);
                    
                }

                // ตรวจสอบการกดปุ่ม E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interacted with " + hit.collider.name);

                    if (firstPersonController.hasKey == true)
                    {
                        SceneManager.LoadScene(endSceneName);
                    }
                    else
                    {
                        interactText.text = "You don't have key"; // แสดงข้อความ "ไม่มีคีย์"
                        StartCoroutine(ResetInteractText()); // เรียก Coroutine เพื่อคืนค่าข้อความ
                    }
                }

                return;
            }
        }

        // หากไม่มีวัตถุในระยะหรือไม่สามารถโต้ตอบได้
        canInteract = false;
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    // ใช้สำหรับการแสดง Debug Ray ใน Scene View
    private void OnDrawGizmos()
    {
        if (Camera.main != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * raycastDistance);
        }
    }

    private IEnumerator ResetInteractText()
    {
        yield return new WaitForSeconds(2f);
        interactText.text = "Press E to Open"; // คืนค่าข้อความ
    }
}

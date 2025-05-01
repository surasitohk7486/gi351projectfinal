using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class EndScene : MonoBehaviour
{
    public float gameTime = 0f;
    public float raycastDistance = 5f; // ระยะทางของ Raycast
    public string endSceneName = "EndScene"; // ชื่อฉากปลายทาง
    public TextMeshProUGUI interactText; // UI Text สำหรับแสดงข้อความ
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private FlashLight flashlight;

    private bool canInteract = false; // ใช้ตรวจสอบว่าผู้เล่นอยู่ในระยะหรือไม่

    void Start()
    {
        Initialize();
        // ซ่อนข้อความตอนเริ่มเกม
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        gameTime += Time.deltaTime;

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
                    TimeSpent(flashlight.idPlayer, gameTime);
                    UseBattery(flashlight.idPlayer, flashlight.fTimeSpent);
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

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void TimeSpent(int numPlayer, float timeSpent)
    {
        CustomEvent time = new CustomEvent("TimeSpent")
        {
            {"Player",numPlayer},
            {"Time", timeSpent}
        };

        AnalyticsService.Instance.RecordEvent(time);
        Debug.Log($"Recording Event BuyPromotion {timeSpent}");
    }

    private void UseBattery(int numPlayer, float useBattery)
    {
        CustomEvent batteryUse = new CustomEvent("BatteryUse")
        {
            {"Player",numPlayer},
            {"UsageRateBattery",useBattery}
        };

        AnalyticsService.Instance.RecordEvent(batteryUse);
        Debug.Log($"Recording Event NotesInGame {useBattery}");
    }
}

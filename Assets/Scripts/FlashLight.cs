using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;

    [SerializeField] private bool on;
    [SerializeField] private bool off;

    [SerializeField] private bool isLocked = false; // ตัวแปรควบคุมการล็อกไฟฉาย

    public float batteryLevel = 100f; // แบตเตอรี่เริ่มต้น (100%)
    [SerializeField] private float batteryConsumptionRate = 1f; // อัตราการลดแบตเตอรี่ต่อวินาที
    [SerializeField] private float maxBatteryLevel = 100f;

    // ตัวแปรสำหรับเสียง
    [SerializeField] private AudioClip flashlightOnSound;
    [SerializeField] private AudioClip flashlightOffSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject pickupText;
    [SerializeField] private TextMeshProUGUI batteryPercentageText;
    [SerializeField] private AudioSource sound;

    [SerializeField] private LayerMask batteryLayer;

    private void Start()
    {
        off = true;
        flashlight.SetActive(false);

        // ตรวจสอบว่า AudioSource ได้ตั้งค่าไว้หรือยัง ถ้ายังไม่มีจะเพิ่มให้อัตโนมัติ
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        pickupText.SetActive(false);
    }

    private void Update()
    {
        // ตรวจสอบว่าถูกล็อกหรือไม่
        if (!isLocked)
        {
            if (off && Input.GetMouseButtonDown(0))
            {
                flashlight.SetActive(true);
                PlaySound(flashlightOnSound); // เล่นเสียงเปิดไฟฉาย
                off = false;
                on = true;
            }
            else if (on && Input.GetMouseButtonDown(0))
            {
                flashlight.SetActive(false);
                PlaySound(flashlightOffSound); // เล่นเสียงปิดไฟฉาย
                off = true;
                on = false;
            }
        }

        if (on)
        {
            batteryLevel -= batteryConsumptionRate * Time.deltaTime;
            batteryLevel = Mathf.Max(batteryLevel, 0); // ไม่ให้แบตเตอรี่ติดลบ

            if (batteryLevel == 0)
            {
                flashlight.SetActive(false); // ปิดไฟฉายเมื่อแบตหมด
                PlaySound(flashlightOffSound);
                off = true;
                on = false;
            }
        }

        UpdateBatteryUI();

        RaycastPickup();
    }

    private void RaycastPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, batteryLayer)) // ตรวจจับไอเท็มในระยะ 3 เมตร
        {
            BatteryItem batteryItem = hit.collider.GetComponent<BatteryItem>(); // ตรวจสอบว่า Object ที่ชนคือไอเท็มแบตเตอรี่หรือไม่
            if (batteryItem != null)
            {
                // แสดงข้อความ UI หรืออินเตอร์เฟซว่าไอเท็มนี้สามารถเก็บได้

                pickupText.SetActive(true); // เปลี่ยนข้อความ UI
                // เมื่อกดปุ่ม E เก็บไอเท็ม

                if (Input.GetKeyDown(KeyCode.E))
                {
                    IncreaseBattery(batteryItem.batteryAmount); // เพิ่มแบตเตอรี่ให้กับไฟฉาย
                    Destroy(hit.collider.gameObject); // ทำลายไอเท็มหลังจากเก็บ
                }
            }
            else
            {
                pickupText.SetActive(false);
            }
        }
        else
        {
            pickupText.SetActive(false);
        }
    }


    public void IncreaseBattery(float amount)
    {
        sound.Play();
        batteryLevel += amount;
        batteryLevel = Mathf.Min(batteryLevel, maxBatteryLevel); // ไม่ให้เกินค่าสูงสุด
    }

    // ฟังก์ชันล็อกไฟฉาย
    public void LockFlashlight(bool lockState)
    {
        isLocked = lockState;

        // ปิดไฟฉายทันทีเมื่อถูกล็อก
        if (isLocked && flashlight.activeSelf)
        {
            flashlight.SetActive(false);
            PlaySound(flashlightOffSound); // เล่นเสียงปิดไฟฉายเมื่อถูกล็อก
            off = true;
            on = false;
        }
    }

    private void UpdateBatteryUI()
    {
        // แสดงเปอร์เซ็นต์แบตเตอรี่
        float batteryPercentage = (batteryLevel / maxBatteryLevel) * 100f;
        batteryPercentageText.text = Mathf.RoundToInt(batteryPercentage) + "%"; // อัปเดตข้อความ UI
    }

    // ฟังก์ชันสำหรับเล่นเสียง
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

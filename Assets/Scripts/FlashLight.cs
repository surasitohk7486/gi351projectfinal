using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject flashlight;

    public bool on;
    public bool off;

    private bool isLocked = false; // ตัวแปรควบคุมการล็อกไฟฉาย

    public float batteryLevel = 100f; // แบตเตอรี่เริ่มต้น (100%)
    public float batteryConsumptionRate = 1f; // อัตราการลดแบตเตอรี่ต่อวินาที
    public float maxBatteryLevel = 100f;

    // ตัวแปรสำหรับเสียง
    public AudioClip flashlightOnSound;
    public AudioClip flashlightOffSound;
    public AudioSource audioSource;

    private void Start()
    {
        off = true;
        flashlight.SetActive(false);

        // ตรวจสอบว่า AudioSource ได้ตั้งค่าไว้หรือยัง ถ้ายังไม่มีจะเพิ่มให้อัตโนมัติ
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
    }

    public void IncreaseBattery(float amount)
    {
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

    // ฟังก์ชันสำหรับเล่นเสียง
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

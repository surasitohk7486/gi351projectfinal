using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    //public AudioSource turnOn;
    //public AudioSource turnOff;

    public GameObject flashlight;

    public bool on;
    public bool off;

    private bool isLocked = false; // ตัวแปรควบคุมการล็อกไฟฉาย

    private void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }

    private void Update()
    {
        // ตรวจสอบว่าถูกล็อกหรือไม่
        if (!isLocked)
        {
            if (off && Input.GetMouseButtonDown(0))
            {
                flashlight.SetActive(true);
                off = false;
                on = true;
            }
            else if (on && Input.GetMouseButtonDown(0))
            {
                flashlight.SetActive(false);
                off = true;
                on = false;
            }
        }
    }

    // ฟังก์ชันล็อกไฟฉาย
    public void LockFlashlight(bool lockState)
    {
        isLocked = lockState;

        // ปิดไฟฉายทันทีเมื่อถูกล็อก
        if (isLocked && flashlight.activeSelf)
        {
            flashlight.SetActive(false);
            off = true;
            on = false;
        }
    }
}

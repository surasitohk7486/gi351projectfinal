using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject flashlight;

    public bool on;
    public bool off;

    private bool isLocked = false; // ตัวแปรควบคุมการล็อกไฟฉาย

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

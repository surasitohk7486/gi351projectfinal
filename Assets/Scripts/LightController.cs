using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public List<Light> lights; // รายการของไฟที่ต้องควบคุม
    public float lightOffDelay = 0.5f; // ดีเลย์ระหว่างการปิดไฟทีละดวง
    public float resetDelay = 5f; // เวลารอสำหรับเปิดไฟใหม่ทั้งหมด
    public Transform player; // ตัวผู้เล่น
    public float lockDuration = 3f; // ระยะเวลาที่ผู้เล่นถูกล็อก

    private bool isProcessing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isProcessing)
        {
            StartCoroutine(HandleLights());
        }
    }

    private IEnumerator HandleLights()
    {
        isProcessing = true;

        // ล็อกการเคลื่อนไหวและมุมมองของผู้เล่น
        FirstPersonController playerControl = player.GetComponent<FirstPersonController>();
        if (playerControl != null)
        {
            playerControl.LockPlayer(lockDuration);
        }

        FlashLight flashLight = player.GetComponent<FlashLight>();
        if (flashLight != null)
        {
            flashLight.LockFlashlight(true); // ล็อกไฟฉายทันที
        }
        yield return new WaitForSeconds(3f);

        // ปิดไฟทีละดวง
        foreach (Light light in lights)
        {
            light.enabled = false;
            yield return new WaitForSeconds(lightOffDelay);
        }

        // รอเวลาที่กำหนดก่อนเปิดไฟใหม่ทั้งหมด
        yield return new WaitForSeconds(resetDelay);

        // เปิดไฟทั้งหมด
        foreach (Light light in lights)
        {
            light.enabled = true;
        }

        if (flashLight != null)
        {
            flashLight.LockFlashlight(false);
        }

        isProcessing = false;
    }
}

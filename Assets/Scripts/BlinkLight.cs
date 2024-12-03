using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    public List<Light> lights = new List<Light>(); // รายการไฟที่ต้องการกระพริบ
    public float minBlinkInterval = 0.2f; // เวลาต่ำสุดที่ไฟจะกระพริบ
    public float maxBlinkInterval = 1.0f; // เวลาสูงสุดที่ไฟจะกระพริบ

    [SerializeField] private List<Coroutine> blinkCoroutines = new List<Coroutine>(); // จัดเก็บ Coroutine แต่ละดวง

    private void Start()
    {
        // เริ่มกระพริบไฟในรายการเริ่มต้น
        foreach (Light light in lights)
        {
            Coroutine newCoroutine = StartCoroutine(BlinkingLight(light));
        }
        
    }

    public void AddLight(Light newLight)
    {
        if (!lights.Contains(newLight))
        {
            lights.Add(newLight); // เพิ่มไฟในรายการ
            Coroutine newCoroutine = StartCoroutine(BlinkingLight(newLight)); // เริ่มกระพริบไฟใหม่
            blinkCoroutines.Add(newCoroutine); // เก็บ Coroutine เพื่อจัดการ
        }
    }

    private IEnumerator BlinkingLight(Light light)
    {
        while (true)
        {
            light.enabled = !light.enabled; // สลับสถานะเปิด-ปิด
            float randomInterval = Random.Range(minBlinkInterval, maxBlinkInterval); // เลือกเวลาสุ่ม
            yield return new WaitForSeconds(randomInterval); // รอเวลาสุ่ม
        }
    }
}

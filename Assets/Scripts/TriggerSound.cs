using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource triggerSound; // เสียงที่ต้องการเล่น
    public string targetTag = "Player"; // Tag ของวัตถุที่ทำให้เกิด Trigger (เช่น Player)

    public int Sound = 0;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าผู้ชนมี Tag ที่กำหนดหรือไม่
        if (other.CompareTag(targetTag) && Sound == 0)
        {
            // เล่นเสียงเมื่อ Trigger ถูกชน
            if (triggerSound != null)
            {
                triggerSound.Play();
                Sound = 1;
            }
        }
    }
}

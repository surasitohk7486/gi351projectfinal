using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // อ้างถึง Animator ของประตู
    public string openParameter = "IsOpen"; // ชื่อ Parameter ใน Animator

    private bool isOpen = false; // สถานะของประตู

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ตรวจสอบว่า Player เข้าใกล้ประตูหรือไม่
        {
            isOpen = true;
            doorAnimator.SetBool(openParameter, isOpen); // สั่งให้ประตูเปิด
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ตรวจสอบว่า Player ออกจากพื้นที่
        {
            isOpen = false;
            doorAnimator.SetBool(openParameter, isOpen); // สั่งให้ประตูปิด
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // ฟังก์ชันสำหรับเปลี่ยนฉาก
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // โหลดฉากที่กำหนดโดยชื่อ
    }
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // แสดงข้อความใน Console (สำหรับ Debugging)
        Application.Quit(); // ออกจากเกม
    }
}
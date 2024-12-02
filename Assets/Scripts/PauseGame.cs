using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // อ้างอิง UI Panel

    private bool isPaused = false;

    

    void Update()
    {
        // ตรวจสอบการกดปุ่ม Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGameEsc();
            }
        }
    }

    public void PauseGameEsc()
    {
        // แสดง UI และหยุดเกม
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // หยุดเวลาในเกม
        isPaused = true;
    }

    public void ResumeGame()
    {
        // ซ่อน UI และเริ่มเกมต่อ
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // เริ่มเวลาในเกมใหม่
        isPaused = false;
    }

    public void LoadScene(string sceneName)
    {
        // รีเซ็ตเวลาและโหลด Scene ใหม่
        Time.timeScale = 1f; // เริ่มเวลาใหม่ก่อนเปลี่ยน Scene
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // อ้างอิง UI Panel
    [SerializeField] private GameObject player; // ตัวละครผู้เล่น
    private bool isPaused = false;
    [SerializeField] PadInteract padInteract;

    public int numPlayer = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && padInteract.isActive == false)
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

        // ตรวจสอบสถานะ UI และจัดการเคอร์เซอร์
        if (pauseMenuUI.activeSelf)
        {
            EnableCursor();
        }
        else if (padInteract.isActive == false)
        {
            DisableCursor();
        }
    }

    public void PauseGameEsc()
    {
        // แสดง UI และหยุดเกม
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // หยุดเวลาในเกม
        AudioListener.pause = true; // หยุดเสียงทั้งหมดในเกม
        isPaused = true;


        // ปิดการควบคุมตัวละครหรือระบบอื่นๆ
        DisablePlayerControls();
    }

    public void ResumeGame()
    {
        // ซ่อน UI และเริ่มเกมต่อ
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // เริ่มเวลาในเกมใหม่
        AudioListener.pause = false; // เริ่มเสียงใหม่
        isPaused = false;

        // เปิดการควบคุมตัวละครหรือระบบอื่นๆ
        EnablePlayerControls();
    }

    public void LoadScene(string sceneName)
    {
        // รีเซ็ตเวลาและโหลด Scene ใหม่
        Time.timeScale = 1f; // เริ่มเวลาใหม่ก่อนเปลี่ยน Scene
        AudioListener.pause = false; // เริ่มเสียงใหม่
        SceneManager.LoadScene(sceneName);
    }

    private void DisablePlayerControls()
    {
        // ตัวอย่างการปิดระบบ Input ของผู้เล่น
        if (player != null)
        {
            FirstPersonController playerController = player.GetComponent<FirstPersonController>(); // สมมติว่าคุณใช้ PlayerController
            if (playerController != null)
            {
                playerController.enabled = false; // ปิดการทำงานของ PlayerController
            }

            FirstPersonController cameraController = player.GetComponentInChildren<FirstPersonController>(); // สมมติว่าคุณใช้ CameraController
            if (cameraController != null)
            {
                cameraController.enabled = false; // ปิดการทำงานของกล้อง
            }

            FlashLight flashLight = player.GetComponentInChildren<FlashLight>();
            if (flashLight != null)
            {
                flashLight.enabled = false;
            }
        }
    }

    private void EnablePlayerControls()
    {
        // ตัวอย่างการเปิดระบบ Input ของผู้เล่น
        if (player != null)
        {
            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            if (playerController != null)
            {
                playerController.enabled = true; // เปิดการทำงานของ PlayerController
            }

            FirstPersonController cameraController = player.GetComponentInChildren<FirstPersonController>();
            if (cameraController != null)
            {
                cameraController.enabled = true; // เปิดการทำงานของกล้อง
            }

            FlashLight flashLight = player.GetComponentInChildren<FlashLight>();
            if (flashLight != null)
            {
                flashLight.enabled = true;
            }
        }
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None; // ปลดล็อกเมาส์
        Cursor.visible = true; // แสดงเคอร์เซอร์
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ไว้กลางหน้าจอ
        Cursor.visible = false; // ซ่อนเคอร์เซอร์
    }
}

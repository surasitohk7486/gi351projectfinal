using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursorMouse : MonoBehaviour
{
    void Start()
    {
        // แสดง Cursor
        Cursor.visible = true;

        // ปลดล็อก Cursor จากหน้าจอ
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDisable()
    {
        // ซ่อน Cursor
        Cursor.visible = false;

        // ล็อก Cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
}

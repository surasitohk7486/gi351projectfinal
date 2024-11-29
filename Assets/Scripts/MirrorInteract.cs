using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteract : MonoBehaviour
{
    public GameObject targetObject; // วัตถุที่ต้องการ SetActive(true)
    public LayerMask mirrorLayer;  // เลเยอร์ที่กำหนดให้กระจก
    public float raycastDistance = 3f; // ระยะของ Raycast (เช่น 3 เมตร)

    int count = 0;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // ดึงกล้องหลัก
        if (targetObject != null)
        {
            targetObject.SetActive(false); // เริ่มต้นปิดวัตถุไว้
        }
    }

    void Update()
    {
        CheckIfLookingAtMirror();
    }

    private void CheckIfLookingAtMirror()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // ยิง Ray ไปข้างหน้า และตรวจสอบว่าชนกระจกหรือไม่ในระยะ 3 เมตร
        if (Physics.Raycast(ray, out hit, raycastDistance, mirrorLayer) && count == 0)
        {
            if (targetObject != null && !targetObject.activeSelf)
            {
                targetObject.SetActive(true); // เปิดวัตถุ
            }
        }
        else
        {
            if (targetObject != null && targetObject.activeSelf)
            {
                targetObject.SetActive(false); // ปิดวัตถุเมื่อไม่ได้มองกระจก
                count++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 5f; // ระยะตรวจจับ
    public LayerMask noteLayer;
    public int idNote;

    public NoteData noteData; // เก็บข้อมูล ScriptableObject สำหรับโน้ตนี้
    public GameObject particle;

    private void Update()
    {
        CheckForNoteInteraction();
    }
    private void CheckForNoteInteraction()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, noteLayer))
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name}");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Note note = hit.collider.gameObject.GetComponent<Note>();
                if (note != null)
                {
                    note.DeactivateParticle();
                }
                else
                {
                    Debug.LogWarning("No Note component found on the hit object.");
                }
            }
        }
    }
    public void DeactivateParticle()
    {
        if (particle != null)
        {
            particle.SetActive(false);
            Debug.Log($"Particle {particle.name} deactivated.");
        }
    }
}

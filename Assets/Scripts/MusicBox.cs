using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private LayerMask musicBoxLayer;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private bool isOpen = false;

    private GameObject currentMusicBox;

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, musicBoxLayer))
        {
            if (hit.collider.CompareTag("MusicBox") && !isOpen)
            {
                interactionUI.SetActive(true);
                currentMusicBox = hit.collider.gameObject;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleMusic(currentMusicBox);
                }
            }
        }
        else
        {
            interactionUI.SetActive(false);
            currentMusicBox = null;
        }
    }

    public void ToggleMusic(GameObject musicBox)
    {
        AudioSource audio = musicBox.GetComponent<AudioSource>();
        Animator musicBoxAnimator = musicBox.GetComponent<Animator>();
        FirstPersonController playerController = FindObjectOfType<FirstPersonController>();
        FlashLight playerFlashlight = playerController.GetComponent<FlashLight>(); // ดึงไฟฉายจากผู้เล่น

        if (audio != null && playerController != null)
        {
            if (audio.isPlaying)
            {
                Debug.Log("Not Play");
                audio.Pause();
                if (playerFlashlight != null)
                {
                    playerFlashlight.LockFlashlight(false); // ปลดล็อกไฟฉาย
                }
            }
            else
            {
                // ล็อกผู้เล่นให้หันไปที่ MusicBox
                Vector3 directionToMusicBox = (musicBox.transform.position - playerController.transform.position).normalized;
                playerController.LockPlayerUntil(audio.clip.length, directionToMusicBox);

                // ล็อกไฟฉาย
                if (playerFlashlight != null)
                {
                    playerFlashlight.LockFlashlight(true);
                }

                musicBoxAnimator.SetTrigger("OpenBox");
                isOpen = true;
                audio.Play();
            }
        }
    }
}

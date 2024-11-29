using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // ��ҧ�֧ Animator �ͧ��е�
    public string openParameter = "IsOpen"; // ���� Parameter � Animator

    private bool isOpen = false; // ʶҹТͧ��е�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ��Ǩ�ͺ��� Player �������е��������
        {
            isOpen = true;
            doorAnimator.SetBool(openParameter, isOpen); // �������е��Դ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ��Ǩ�ͺ��� Player �͡�ҡ��鹷��
        {
            isOpen = false;
            doorAnimator.SetBool(openParameter, isOpen); // �������еٻԴ
        }
    }
}

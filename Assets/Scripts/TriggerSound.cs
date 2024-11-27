using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource triggerSound; // ���§����ͧ������
    public string targetTag = "Player"; // Tag �ͧ�ѵ�ط�������Դ Trigger (�� Player)

    public int Sound = 0;

    private void OnTriggerEnter(Collider other)
    {
        // ��Ǩ�ͺ��Ҽ�骹�� Tag ����˹��������
        if (other.CompareTag(targetTag) && Sound == 0)
        {
            // ������§����� Trigger �١��
            if (triggerSound != null)
            {
                triggerSound.Play();
                Sound = 1;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // �ѧ��ѹ����Ѻ����¹�ҡ
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // ��Ŵ�ҡ����˹��ª���
    }
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // �ʴ���ͤ���� Console (����Ѻ Debugging)
        Application.Quit(); // �͡�ҡ��
    }
}
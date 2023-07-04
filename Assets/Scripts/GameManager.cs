using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          // �� ���� ���̺귯��

/// <summary>
/// 
/// ���ӸŴ���
/// - ���Ӹ޴�(����ϱ�, ���θ޴���, ��������)
/// - ���ӿ���(����� : ���͸� 0%) -> �������� �ٽ� ����
/// - UI�� ������Ʈ �ϰ� ǥ��
/// 
/// 2021.08.15. KJ
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    // �̱��� ������ ���� �ν��Ͻ�
    // ���ӸŴ����� �ν��Ͻ��� ��� ��������
    private static GameManager instance = null;
    public bool IsGamePaused = false;
    
    void Awake()
    {
        if ( null == instance)
        {
            // �� Ŭ���� �ν��Ͻ��� ������ ��
            // �������� instance�� ���ӸŴ����ν��Ͻ��� ������� �ʴٸ�, 
            // �ڽ��� �־��ش�
            instance = this;

            // �� ��ȯ�� �Ǿ �ı����� �ʰ� �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // ���� ���� ��ȯ�Ǿ��� �� ���ο� ���� ���̾��Ű�� ���ӸŴ����� �������� ����
            // �׷� ��� ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ����
            // �׷��� �̹� ���������� instance�� �ν��Ͻ��� �����ϸ� �ڽ�(���ο� ���� ���ӸŴ���)�� ����
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = 60;
    }

    // ���ӸŴ��� �ν��Ͻ��� ������ �� �ִ� property. static�̹Ƿ� �ٸ� Ŭ�������� �����Ӱ� ȣ�� ����
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    // ���� �Ͻ�����
    public void PauseGame()
    {
        IsGamePaused ^= true;

        if (IsGamePaused == true)
        {
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    // Ÿ��Ʋ ȭ������
    public void GoToTitle()
    {
        SceneManager.LoadScene("0_Title");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // Ʃ�丮���
    public void GoToTutorial()
    {
        SceneManager.LoadScene("1_Tutorial");

        Time.timeScale = 1;
        IsGamePaused = false;
    }
    
    // ��������1����
    public void GoToStage1()
    {
        SceneManager.LoadScene("2_Stage1");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // ��������2��
    public void GoToStage2()
    {
        SceneManager.LoadScene("3_Stage2");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // ���ӿ�������
    public void GoToEnding()
    {
        SceneManager.LoadScene("4_Ending");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // ��������
    public void EndGame()
    {
        Application.Quit();
    }
}

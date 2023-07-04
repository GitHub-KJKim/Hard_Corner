using UnityEngine;

public class StageUIController : MonoBehaviour
{
    // Pause  & Resume
    public void ResumeGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void GoToTitle()
    {
        GameManager.Instance.GoToTitle();
    }

    // Ʃ�丮���
    public void GoToTutorial()
    {
        GameManager.Instance.GoToTutorial();
    }

    // ��������1����
    public void GoToStage1()
    {
        GameManager.Instance.GoToStage1();
    }

    // ��������2��
    public void GoToStage2()
    {
        GameManager.Instance.GoToStage2();
    }

    public void GoToGameEnding()
    {
        GameManager.Instance.GoToEnding();
    }

    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }
}

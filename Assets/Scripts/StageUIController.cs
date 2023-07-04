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

    // 튜토리얼로
    public void GoToTutorial()
    {
        GameManager.Instance.GoToTutorial();
    }

    // 스테이지1으로
    public void GoToStage1()
    {
        GameManager.Instance.GoToStage1();
    }

    // 스테이지2로
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

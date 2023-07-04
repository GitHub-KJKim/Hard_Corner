using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          // 씬 관련 라이브러리

/// <summary>
/// 
/// 게임매니져
/// - 게임메뉴(계속하기, 메인메뉴로, 게임종료)
/// - 게임오버(재시작 : 배터리 0%) -> 스테이지 다시 시작
/// - UI를 업데이트 하고 표시
/// 
/// 2021.08.15. KJ
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    // 싱글턴 패턴을 위한 인스턴스
    // 게임매니저의 인스턴스를 담는 전역변수
    private static GameManager instance = null;
    public bool IsGamePaused = false;
    
    void Awake()
    {
        if ( null == instance)
        {
            // 이 클래스 인스턴스가 생겼을 때
            // 전역변수 instance에 게임매니져인스턴스가 담겨있지 않다면, 
            // 자신을 넣어준다
            instance = this;

            // 씬 전환이 되어도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // 만약 씬이 전환되었을 때 새로운 씬의 하이어라키에 게임매니저가 있을수도 있음
            // 그럴 경우 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많음
            // 그래서 이미 전역변수인 instance에 인스턴스가 존재하면 자신(새로운 씬의 게임매니저)을 삭제
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = 60;
    }

    // 게임매니저 인스턴스에 접근할 수 있는 property. static이므로 다른 클래스에서 자유롭게 호출 가능
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

    // 게임 일시정지
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

    // 타이틀 화면으로
    public void GoToTitle()
    {
        SceneManager.LoadScene("0_Title");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // 튜토리얼로
    public void GoToTutorial()
    {
        SceneManager.LoadScene("1_Tutorial");

        Time.timeScale = 1;
        IsGamePaused = false;
    }
    
    // 스테이지1으로
    public void GoToStage1()
    {
        SceneManager.LoadScene("2_Stage1");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // 스테이지2로
    public void GoToStage2()
    {
        SceneManager.LoadScene("3_Stage2");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // 게임엔딩으로
    public void GoToEnding()
    {
        SceneManager.LoadScene("4_Ending");

        Time.timeScale = 1;
        IsGamePaused = false;
    }

    // 게임종료
    public void EndGame()
    {
        Application.Quit();
    }
}

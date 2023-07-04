using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// 플레이어를 조작한다.
/// 
/// 전후진, 회전, 대쉬
/// 
/// 2021.08.03. KJ
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 로봇 이동
    private Rigidbody playerRigidBody;

    public float moveSpeed = 10f;               // 이동속력
    public float rotSpeed = 100f;               // 회전속력
    public float dashSpeed = 20f;

    // 메뉴
    public GameObject missionList;              // Tab키로 미션현황보기
    public GameObject pauseMenu;                // ESC키로 메뉴보기

    void Start()
    {
        // 게임 오브젝트에서  Rigidbody 컴포넌트를 찾아 playerRigidbody에 할당
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RobotMove();

        ShowSystemMenu();
    }

    void RobotMove()
    {
        // 전진
        if (Input.GetKey(KeyCode.W) == true)
        {
            playerRigidBody.MovePosition(this.transform.position + this.transform.forward * moveSpeed * Time.deltaTime);

            // 전진 중 대쉬
            if (Input.GetKey(KeyCode.Home) == true)
            {
                // 스페이스바를 누르면 평소 속력의 2배로 이동
                playerRigidBody.MovePosition(this.transform.position + this.transform.forward * dashSpeed * Time.deltaTime);
            }
        }

        // 후진
        if (Input.GetKey(KeyCode.S) == true)
        {
            playerRigidBody.MovePosition(this.transform.position + this.transform.forward * -1 * moveSpeed * Time.deltaTime);

            // 후진 중 대쉬
            if (Input.GetKey(KeyCode.Home) == true)
            {
                // 스페이스바를 누르면 평소 속력의 2배로 이동
                playerRigidBody.MovePosition(this.transform.position + this.transform.forward * -1 * dashSpeed * Time.deltaTime);
            }
        }

        // 왼쪽으로 회전
        if (Input.GetKey(KeyCode.A) == true)
        {
            playerRigidBody.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * Time.deltaTime * -rotSpeed));
        }

        // 오른쪽으로 회전
        if (Input.GetKey(KeyCode.D) == true)
        {
            playerRigidBody.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * Time.deltaTime * rotSpeed));
        }
    }

    void ShowSystemMenu()
    {
        // 미션창 활성/비활성화
        if (Input.GetKeyDown(KeyCode.Tab) == true)
        {
            if (missionList.activeSelf.Equals(true))
            {
                missionList.SetActive(false);
            }
            else
            {
                missionList.SetActive(true);
            }
        }

        // Pause 메뉴
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf.Equals(false))
            {
                // 일시정지
                GameManager.Instance.PauseGame();

                SoundManager.Instance.PauseIDLESound();

                // Pause 메뉴 팝업
                pauseMenu.SetActive(true);
            }
            else if (pauseMenu.activeSelf.Equals(true))
            {
                // 재개
                GameManager.Instance.PauseGame();

                SoundManager.Instance.PlayIDLESound("Operate_Vacuum", 0.25f);

                // Pause 메뉴 팝업
                pauseMenu.SetActive(false);
            }
        }
    }
}

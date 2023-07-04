using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// �÷��̾ �����Ѵ�.
/// 
/// ������, ȸ��, �뽬
/// 
/// 2021.08.03. KJ
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    // �κ� �̵�
    private Rigidbody playerRigidBody;

    public float moveSpeed = 10f;               // �̵��ӷ�
    public float rotSpeed = 100f;               // ȸ���ӷ�
    public float dashSpeed = 20f;

    // �޴�
    public GameObject missionList;              // TabŰ�� �̼���Ȳ����
    public GameObject pauseMenu;                // ESCŰ�� �޴�����

    void Start()
    {
        // ���� ������Ʈ����  Rigidbody ������Ʈ�� ã�� playerRigidbody�� �Ҵ�
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RobotMove();

        ShowSystemMenu();
    }

    void RobotMove()
    {
        // ����
        if (Input.GetKey(KeyCode.W) == true)
        {
            playerRigidBody.MovePosition(this.transform.position + this.transform.forward * moveSpeed * Time.deltaTime);

            // ���� �� �뽬
            if (Input.GetKey(KeyCode.Home) == true)
            {
                // �����̽��ٸ� ������ ��� �ӷ��� 2��� �̵�
                playerRigidBody.MovePosition(this.transform.position + this.transform.forward * dashSpeed * Time.deltaTime);
            }
        }

        // ����
        if (Input.GetKey(KeyCode.S) == true)
        {
            playerRigidBody.MovePosition(this.transform.position + this.transform.forward * -1 * moveSpeed * Time.deltaTime);

            // ���� �� �뽬
            if (Input.GetKey(KeyCode.Home) == true)
            {
                // �����̽��ٸ� ������ ��� �ӷ��� 2��� �̵�
                playerRigidBody.MovePosition(this.transform.position + this.transform.forward * -1 * dashSpeed * Time.deltaTime);
            }
        }

        // �������� ȸ��
        if (Input.GetKey(KeyCode.A) == true)
        {
            playerRigidBody.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * Time.deltaTime * -rotSpeed));
        }

        // ���������� ȸ��
        if (Input.GetKey(KeyCode.D) == true)
        {
            playerRigidBody.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * Time.deltaTime * rotSpeed));
        }
    }

    void ShowSystemMenu()
    {
        // �̼�â Ȱ��/��Ȱ��ȭ
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

        // Pause �޴�
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf.Equals(false))
            {
                // �Ͻ�����
                GameManager.Instance.PauseGame();

                SoundManager.Instance.PauseIDLESound();

                // Pause �޴� �˾�
                pauseMenu.SetActive(true);
            }
            else if (pauseMenu.activeSelf.Equals(true))
            {
                // �簳
                GameManager.Instance.PauseGame();

                SoundManager.Instance.PlayIDLESound("Operate_Vacuum", 0.25f);

                // Pause �޴� �˾�
                pauseMenu.SetActive(false);
            }
        }
    }
}

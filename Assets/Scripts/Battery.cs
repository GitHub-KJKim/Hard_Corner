using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // UI ���� ���̺귯��
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// �κ�û�ұ��� ���͸� ���� ��ũ��Ʈ
/// 
/// 2021.08.15. <���ѵ�������> KJ
/// 
/// </summary>
public class Battery : MonoBehaviour
{
    GameObject player;
    public Transform spawnPoint;

    [HideInInspector] public bool IsCharged = false;    // �������ΰ�?
    [HideInInspector] public float batteryGauge;        // ���͸� �ܷ�
    [SerializeField] float maxBattery = 100f;           // ���͸� 100%

    [Range(0.1f, 3.0f)]
    public float dischargingRate = 3f;                  // ���͸� �Ҹ� ������

    /// UI ���� ����
    public Text batteryText;            // UI ���͸� �ܷ� �ؽ�Ʈ
    public Image batteryState;          // UI ���͸� �ܷ� �̹���
    [SerializeField] Sprite[] sprites;

    public GameObject noBatteryPanel;

    // Start is called before the first frame update
    void Start()
    {
        batteryGauge = maxBattery;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBattery();

        KillRobot();
    }

    // ���͸� ���� ������Ʈ
    void UpdateBattery()
    {
        //Debug.Log(string.Format("{0:N0}", batteryGauge));        // ���͸� �ܷ�(�Ҽ��� ����)
        batteryText.text = (int)batteryGauge + " %";

        if (batteryGauge > 80)
        {
            batteryState.sprite = sprites[0];           // 80% �̻�
        }
        else if (batteryGauge > 60)
        {
            batteryState.sprite = sprites[1];           // 60% �̻�
        }
        else if (batteryGauge > 40)
        {
            batteryState.sprite = sprites[2];           // 40% �̻�
        }
        else if (batteryGauge > 20)
        {
            batteryState.sprite = sprites[3];           // 20% �̻�
        }
        else if (batteryGauge >= 1)
        {
            batteryState.sprite = sprites[4];           // 0 ~ 20%

            SoundManager.Instance.PlaySFXSound("Vacuum_Alarm", 0.4f);
        }
        else if (batteryGauge != 0)
        {            
            batteryGauge = 0;

            batteryState.sprite = sprites[5];           // 0% 
                        
            if (noBatteryPanel.activeSelf.Equals(false))
            {
                // AudioSource �ߺ����� ���� ȿ���� play �� Stop��Ų��.
                SoundManager.Instance.StopSFXSound();       
                SoundManager.Instance.PauseIDLESound();
                SoundManager.Instance.PlaySFXSound("Vacuum_TurnOff", 1);
            }           

            noBatteryPanel.SetActive(true);

            Time.timeScale = 0;
            GameManager.Instance.IsGamePaused = true;
        }

        if (IsCharged == true)
        {
            ChargingBattery();
        }
        else
        {
            ConsumeBattery();
        }
    }

    void ChargingBattery()
    {
        if (batteryGauge <= 100)
        {
            batteryGauge += Time.deltaTime * 20;
        }
    }

    public void RechargedComplete()
    {
        StartCoroutine(WaitforSinglePlay());
    }

    private IEnumerator WaitforSinglePlay()
    {
        while (true)
        {
            yield return null;                      // �� ������ ����

            if (IsCharged == false) yield break;    // �ڷ�ƾ ����, yield break�� return ���� �ǹ�

            if (batteryGauge >= 100.0f) break;      // while ����
        }

        SoundManager.Instance.PlaySFXSound("Recharged_Complete", 1);
    }

    void ConsumeBattery()
    {
        // 3�ʿ� 1%�� ���͸� �Ҹ�
        batteryGauge -= Time.deltaTime / dischargingRate;

        // �뽬�� �� ������ 1%�� �Ҹ�
        if (Input.GetKey(KeyCode.Home) == true)
        {
            batteryGauge -= Time.deltaTime * dischargingRate * 3;
        }
    }

    public void OnClickBtnReStart()
    {
        /*
        // ���͸��� ������ ���·� �����
        // ���͸� ����
        batteryGauge = maxBattery;

        // �κ�û�ұ� ������
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        SoundManager.Instance.PlayIDLESound("Operate_Vacuum", 0.25f);
        */
       
        GameManager.Instance.PauseGame();

        Time.timeScale = 1;
        GameManager.Instance.IsGamePaused = false;

        noBatteryPanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void KillRobot()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) == true)
        {
            batteryGauge = 0;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // UI 관련 라이브러리
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// 로봇청소기의 배터리 관련 스크립트
/// 
/// 2021.08.15. <대한독립만세> KJ
/// 
/// </summary>
public class Battery : MonoBehaviour
{
    GameObject player;
    public Transform spawnPoint;

    [HideInInspector] public bool IsCharged = false;    // 충전중인가?
    [HideInInspector] public float batteryGauge;        // 배터리 잔량
    [SerializeField] float maxBattery = 100f;           // 배터리 100%

    [Range(0.1f, 3.0f)]
    public float dischargingRate = 3f;                  // 배터리 소모 빠르기

    /// UI 관련 변수
    public Text batteryText;            // UI 배터리 잔량 텍스트
    public Image batteryState;          // UI 배터리 잔량 이미지
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

    // 배터리 상태 업데이트
    void UpdateBattery()
    {
        //Debug.Log(string.Format("{0:N0}", batteryGauge));        // 배터리 잔량(소숫점 버림)
        batteryText.text = (int)batteryGauge + " %";

        if (batteryGauge > 80)
        {
            batteryState.sprite = sprites[0];           // 80% 이상
        }
        else if (batteryGauge > 60)
        {
            batteryState.sprite = sprites[1];           // 60% 이상
        }
        else if (batteryGauge > 40)
        {
            batteryState.sprite = sprites[2];           // 40% 이상
        }
        else if (batteryGauge > 20)
        {
            batteryState.sprite = sprites[3];           // 20% 이상
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
                // AudioSource 중복으로 다음 효과음 play 전 Stop시킨다.
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
            yield return null;                      // 한 프레임 쉰다

            if (IsCharged == false) yield break;    // 코루틴 종료, yield break는 return 같은 의미

            if (batteryGauge >= 100.0f) break;      // while 종료
        }

        SoundManager.Instance.PlaySFXSound("Recharged_Complete", 1);
    }

    void ConsumeBattery()
    {
        // 3초에 1%씩 배터리 소모
        batteryGauge -= Time.deltaTime / dischargingRate;

        // 대쉬를 쓸 때마다 1%씩 소모
        if (Input.GetKey(KeyCode.Home) == true)
        {
            batteryGauge -= Time.deltaTime * dischargingRate * 3;
        }
    }

    public void OnClickBtnReStart()
    {
        /*
        // 배터리만 충전한 상태로 재시작
        // 배터리 충전
        batteryGauge = maxBattery;

        // 로봇청소기 리스폰
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

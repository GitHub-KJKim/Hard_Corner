using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 스테이지2의 미션을 수행했는지 확인하는 스크립트
/// 
/// </summary>

public class MissionCheck_Stage2 : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // 미션 클리어 체크\

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    public enum mission
    {
        ePumpkin,       // 0   호박 옮기기
        ePot,           // 1   화분 원위치(4개)
        eFootPrint,     // 2   발자국 닦기
        eDust,          // 3   먼지 지우기
        eSoil,          // 4   흙 치우기

        Max             // ?
    }

    public enum pot
    {
        eAloe,          // 0    알로에
        eTree1,         // 1    O 한 개 나무
        eTree2,         // 2    o 세 개 나무
        eCactus,        // 3    선인장

        Max
    }

    // 미션 클리어 체크를 위해 먼지, 부스러기에 붙어있는 스크립트를 가져와 카운팅
    private CollisionDetector_ObjectDestination pumpkin;        // 호박
    private CollisionDetector_ObjectDestination[] pots = 
        new CollisionDetector_ObjectDestination[(int)pot.Max];  // 화분(4개)         
    private DirtsManager footPrint;                         // 발자국
    private DirtsManager dust;                              // 먼지
    private DirtsManager soil;                              // 흙

    // Start is called before the first frame update
    void Start()
    {
        pumpkin = GameObject.Find("Destination_Pumpkin").GetComponent<CollisionDetector_ObjectDestination>();

        pots[(int)pot.eAloe  ] = GameObject.Find("Destination_Aloe").GetComponent<CollisionDetector_ObjectDestination>();
        pots[(int)pot.eTree1 ] = GameObject.Find("Destination_Tree1").GetComponent<CollisionDetector_ObjectDestination>();
        pots[(int)pot.eTree2 ] = GameObject.Find("Destination_Tree2").GetComponent<CollisionDetector_ObjectDestination>();
        pots[(int)pot.eCactus] = GameObject.Find("Destination_Cactus").GetComponent<CollisionDetector_ObjectDestination>();

        footPrint = GameObject.Find("Catwalk_Dust").GetComponent<DirtsManager>();
        dust = GameObject.Find("Corner_Dust").GetComponent<DirtsManager>();
        soil = GameObject.Find("Flower_Soil_Dust").GetComponent<DirtsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToBeCleaned();
        CheckInPlace();
        CheatKey();
        CheckAllMissionClear();
    }

    // 청소가 되었는지 체크
    void CheckToBeCleaned()
    {
        // 발자국 체크
        if (footPrint.Count == 0)
        {
            if (!missionCheck[(int)mission.eFootPrint])
            {
                missionCheck[(int)mission.eFootPrint] = true;
                checkBar[(int)mission.eFootPrint].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }

        // 먼지 체크
        if (dust.Count == 0)
        {
            if (!missionCheck[(int)mission.eDust])
            {
                missionCheck[(int)mission.eDust] = true;
                checkBar[(int)mission.eDust].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }

        // 흙 체크
        if (soil.Count == 0)
        {
            if (!missionCheck[(int)mission.eSoil])
            {
                missionCheck[(int)mission.eSoil] = true;
                checkBar[(int)mission.eSoil].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }
    }

    // 제자리에 있는지 체크
    void CheckInPlace()
    {
        // 호박 옮기기 체크
        if (pumpkin.IsMovedComplete == true)
        {
            if (missionCheck[(int)mission.ePumpkin] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }

            missionCheck[(int)mission.ePumpkin] = true;
            checkBar[(int)mission.ePumpkin].SetActive(true);
        }
        else
        {
            missionCheck[(int)mission.ePumpkin] = false;
            checkBar[(int)mission.ePumpkin].SetActive(false);
        }

        // 화분 위치 체크
        CheckPotInPlace();
    }

    // 모든 화분이 제자리에 있는지 체크
    void CheckPotInPlace()
    {
        int tempCount = 0;

        // 화분(4개)을 순회하며 제자리에 있는지 체크해서
        for (int i = 0; i < (int)pot.Max; i++)
        {
            if (pots[i].IsMovedComplete == true)
            {
                tempCount++;
            }
        }

        // 모두 제자리이면
        if (tempCount == (int)pot.Max)
        {
            if ( missionCheck[(int)mission.ePot] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }

            // 화분 옮기기 미션 완료
            missionCheck[(int)mission.ePot] = true;
            checkBar[(int)mission.ePot].SetActive(true);
        }
        else
        {
            // 하나라도 제자리 아닐 경우엔 미완료
            missionCheck[(int)mission.ePot] = false;
            checkBar[(int)mission.ePot].SetActive(false);
        }
    }

    void CheckAllMissionClear()
    {
        int tempCount = 0;

        for (int i = 0; i < (int)mission.Max; i++)
        {
            if (missionCheck[i] == true)
            {
                tempCount++;
            }
        }

        if (tempCount == (int)mission.Max)
        {
            StageClearPanel.SetActive(true);
        }
    }

    // 스테이지 클리어 치트
    void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            for (int i = 0; i < (int)mission.Max; i++)
            {
                missionCheck[i] = true;
            }
        }
    }
}

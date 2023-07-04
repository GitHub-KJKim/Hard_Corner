using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 스테이지1의 미션을 수행했는지 확인하는 스크립트
///  
/// 2021.08.15. KJ.
/// 
/// </summary>
public class MissionCheck_Stage1 : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // 미션 클리어 체크

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    public enum mission
    {
        eDirts,         // 0   먼지 흡입
        eCrumb,         // 1   부스러기 흡입
        eDirtySpot,     // 2   얼룩 닦기
        eTrash,         // 3   쓰레기 쓰레기통에 넣기
        eSlipper,       // 4   슬리퍼 제자리에 이동시키기

        Max             // ?
    }

    // 미션 클리어 체크를 위해 먼지, 부스러기에 붙어있는 스크립트를 가져와 카운팅
    private DirtsManager dust;              // 먼지
    private DirtsManager crumb;             // 부스러기
    private DirtsManager dirtySpot;         // 얼룩
    private CollisionDetector_ObjectDestination slipper;      // 슬리퍼
    private CollisionDetector_ObjectDestination trash;        // 쓰레기

    // Start is called before the first frame update
    void Start()
    {
        dust = GameObject.Find("DirtyDust").GetComponent<DirtsManager>();
        crumb = GameObject.Find("DitryCrumbs").GetComponent<DirtsManager>();
        dirtySpot = GameObject.Find("DirtySpot").GetComponent<DirtsManager>();

        slipper = GameObject.Find("Destination_Slipper").GetComponent<CollisionDetector_ObjectDestination>();
        trash = GameObject.Find("Destination_Paper").GetComponent<CollisionDetector_ObjectDestination>();
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
        // 먼지 체크
        if (dust.Count == 0)
        {
            if (!missionCheck[(int)mission.eDirts])
            {
                missionCheck[(int)mission.eDirts] = true;
                checkBar[(int)mission.eDirts].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }

        // 부스러기 체크
        if (crumb.Count == 0)
        {
            if (!missionCheck[(int)mission.eCrumb])
            {
                missionCheck[(int)mission.eCrumb] = true;
                checkBar[(int)mission.eCrumb].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }

        // 얼룩 체크
        if (dirtySpot.Count == 0)
        {
            if (!missionCheck[(int)mission.eDirtySpot])
            {
                missionCheck[(int)mission.eDirtySpot] = true;
                checkBar[(int)mission.eDirtySpot].SetActive(true);

                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
        }
    }

    // 제자리에 있는지 체크
    void CheckInPlace()
    {
        if (slipper.IsMovedComplete == true)
        {
            if (missionCheck[(int)mission.eSlipper] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }

            missionCheck[(int)mission.eSlipper] = true;
            checkBar[(int)mission.eSlipper].SetActive(true);
        }
        else
        {
            missionCheck[(int)mission.eSlipper] = false;
            checkBar[(int)mission.eSlipper].SetActive(false);
        }

        if (trash.IsMovedComplete == true)
        {
            if (missionCheck[(int)mission.eTrash] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }
            
            missionCheck[(int)mission.eTrash] = true;
            checkBar[(int)mission.eTrash].SetActive(true);
        }
        else
        {
            missionCheck[(int)mission.eTrash] = false;
            checkBar[(int)mission.eTrash].SetActive(false);
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

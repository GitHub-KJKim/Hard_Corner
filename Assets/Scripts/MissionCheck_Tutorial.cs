using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCheck_Tutorial : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // 미션 클리어 체크

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    private UIStatusMessage statusText;

    public enum mission
    {
        eTrash,         // 1   쓰레기 버리기
        eDirts,         // 2   먼지 흡입
        eDirtySpot,     // 3   얼룩 닦기

        Max             // ?
    }

    // 튜토리얼 전용 ( 활성화 및 비활성화를 위함)
    public GameObject paper;                // 종이(쓰레기)
    public GameObject trashCan;             // 쓰레기통  

    public GameObject dirts;                // 먼지s

    public GameObject towel;                // 타월
    public GameObject paints;               // 물감(얼룩)


    // 미션 클리어 체크를 위해 먼지, 부스러기에 붙어있는 스크립트를 가져와 카운팅
    private CollisionDetector_ObjectDestination trash;      // 쓰레기
    private DirtsManager dust;              // 먼지
    private DirtsManager dirtySpot;         // 얼룩

    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<UIStatusMessage>();

        trash = GameObject.Find("Destination_Trash").GetComponent<CollisionDetector_ObjectDestination>();
        dust = GameObject.Find("DirtyDust").GetComponent<DirtsManager>();
        dirtySpot = GameObject.Find("DirtySpot").GetComponent<DirtsManager>();

        // 쓰레기와 쓰레기통을 제외한 오브젝트 비활성화
        //paper.SetActive(true);
        //trashCan.SetActive(true);

        dirts.SetActive(false);

        towel.SetActive(false);
        paints.SetActive(false);

        statusText.BeforeTrashTouch();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInPlace();
        CheckToBeCleaned();
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

                Step2Clear();
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
        if (trash.IsMovedComplete == true)
        {
            if (missionCheck[(int)mission.eTrash] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
                Step1Clear();
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

    // 튜토리얼 단계
    void Step1Clear()
    {
        paper.SetActive(false);
        trashCan.SetActive(false);

        dirts.SetActive(true);              // 먼지만 보임

        statusText.BeforeDirtTouch();
    }

    void Step2Clear()
    {
        dirts.SetActive(false);

        towel.SetActive(true);             // 타월 및 얼룩만 보임
        paints.SetActive(true);

        statusText.BeforePaintTouch();
    }


}

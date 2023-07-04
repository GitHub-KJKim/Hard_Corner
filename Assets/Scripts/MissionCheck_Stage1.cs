using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ��������1�� �̼��� �����ߴ��� Ȯ���ϴ� ��ũ��Ʈ
///  
/// 2021.08.15. KJ.
/// 
/// </summary>
public class MissionCheck_Stage1 : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // �̼� Ŭ���� üũ

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    public enum mission
    {
        eDirts,         // 0   ���� ����
        eCrumb,         // 1   �ν����� ����
        eDirtySpot,     // 2   ��� �۱�
        eTrash,         // 3   ������ �������뿡 �ֱ�
        eSlipper,       // 4   ������ ���ڸ��� �̵���Ű��

        Max             // ?
    }

    // �̼� Ŭ���� üũ�� ���� ����, �ν����⿡ �پ��ִ� ��ũ��Ʈ�� ������ ī����
    private DirtsManager dust;              // ����
    private DirtsManager crumb;             // �ν�����
    private DirtsManager dirtySpot;         // ���
    private CollisionDetector_ObjectDestination slipper;      // ������
    private CollisionDetector_ObjectDestination trash;        // ������

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

    // û�Ұ� �Ǿ����� üũ
    void CheckToBeCleaned()
    {
        // ���� üũ
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

        // �ν����� üũ
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

        // ��� üũ
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

    // ���ڸ��� �ִ��� üũ
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

    // �������� Ŭ���� ġƮ
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

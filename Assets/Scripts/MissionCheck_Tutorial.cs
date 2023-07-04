using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCheck_Tutorial : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // �̼� Ŭ���� üũ

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    private UIStatusMessage statusText;

    public enum mission
    {
        eTrash,         // 1   ������ ������
        eDirts,         // 2   ���� ����
        eDirtySpot,     // 3   ��� �۱�

        Max             // ?
    }

    // Ʃ�丮�� ���� ( Ȱ��ȭ �� ��Ȱ��ȭ�� ����)
    public GameObject paper;                // ����(������)
    public GameObject trashCan;             // ��������  

    public GameObject dirts;                // ����s

    public GameObject towel;                // Ÿ��
    public GameObject paints;               // ����(���)


    // �̼� Ŭ���� üũ�� ���� ����, �ν����⿡ �پ��ִ� ��ũ��Ʈ�� ������ ī����
    private CollisionDetector_ObjectDestination trash;      // ������
    private DirtsManager dust;              // ����
    private DirtsManager dirtySpot;         // ���

    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<UIStatusMessage>();

        trash = GameObject.Find("Destination_Trash").GetComponent<CollisionDetector_ObjectDestination>();
        dust = GameObject.Find("DirtyDust").GetComponent<DirtsManager>();
        dirtySpot = GameObject.Find("DirtySpot").GetComponent<DirtsManager>();

        // ������� ���������� ������ ������Ʈ ��Ȱ��ȭ
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

                Step2Clear();
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

    // Ʃ�丮�� �ܰ�
    void Step1Clear()
    {
        paper.SetActive(false);
        trashCan.SetActive(false);

        dirts.SetActive(true);              // ������ ����

        statusText.BeforeDirtTouch();
    }

    void Step2Clear()
    {
        dirts.SetActive(false);

        towel.SetActive(true);             // Ÿ�� �� ��踸 ����
        paints.SetActive(true);

        statusText.BeforePaintTouch();
    }


}

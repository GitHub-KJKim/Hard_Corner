using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ��������2�� �̼��� �����ߴ��� Ȯ���ϴ� ��ũ��Ʈ
/// 
/// </summary>

public class MissionCheck_Stage2 : MonoBehaviour
{
    private bool[] missionCheck = new bool[(int)mission.Max];   // �̼� Ŭ���� üũ\

    public GameObject[] checkBar = new GameObject[(int)mission.Max];

    public GameObject StageClearPanel;

    public enum mission
    {
        ePumpkin,       // 0   ȣ�� �ű��
        ePot,           // 1   ȭ�� ����ġ(4��)
        eFootPrint,     // 2   ���ڱ� �۱�
        eDust,          // 3   ���� �����
        eSoil,          // 4   �� ġ���

        Max             // ?
    }

    public enum pot
    {
        eAloe,          // 0    �˷ο�
        eTree1,         // 1    O �� �� ����
        eTree2,         // 2    o �� �� ����
        eCactus,        // 3    ������

        Max
    }

    // �̼� Ŭ���� üũ�� ���� ����, �ν����⿡ �پ��ִ� ��ũ��Ʈ�� ������ ī����
    private CollisionDetector_ObjectDestination pumpkin;        // ȣ��
    private CollisionDetector_ObjectDestination[] pots = 
        new CollisionDetector_ObjectDestination[(int)pot.Max];  // ȭ��(4��)         
    private DirtsManager footPrint;                         // ���ڱ�
    private DirtsManager dust;                              // ����
    private DirtsManager soil;                              // ��

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

    // û�Ұ� �Ǿ����� üũ
    void CheckToBeCleaned()
    {
        // ���ڱ� üũ
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

        // ���� üũ
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

        // �� üũ
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

    // ���ڸ��� �ִ��� üũ
    void CheckInPlace()
    {
        // ȣ�� �ű�� üũ
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

        // ȭ�� ��ġ üũ
        CheckPotInPlace();
    }

    // ��� ȭ���� ���ڸ��� �ִ��� üũ
    void CheckPotInPlace()
    {
        int tempCount = 0;

        // ȭ��(4��)�� ��ȸ�ϸ� ���ڸ��� �ִ��� üũ�ؼ�
        for (int i = 0; i < (int)pot.Max; i++)
        {
            if (pots[i].IsMovedComplete == true)
            {
                tempCount++;
            }
        }

        // ��� ���ڸ��̸�
        if (tempCount == (int)pot.Max)
        {
            if ( missionCheck[(int)mission.ePot] == false)
            {
                SoundManager.Instance.StopSFXSound();
                SoundManager.Instance.PlaySFXSound("Mission_Complete", 1);
            }

            // ȭ�� �ű�� �̼� �Ϸ�
            missionCheck[(int)mission.ePot] = true;
            checkBar[(int)mission.ePot].SetActive(true);
        }
        else
        {
            // �ϳ��� ���ڸ� �ƴ� ��쿣 �̿Ϸ�
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

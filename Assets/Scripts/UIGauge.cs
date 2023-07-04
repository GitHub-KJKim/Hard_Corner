using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    Image fillGauge;                        // ������ ��
    float nowProgress = 0f;                 // ���� �����Ȳ
    int totalCount;                         // �����ؾ��� �� ����
    int nowCount;

    private DirtsManager dirts;             // ����, �ν�����, ��� ��
    public GameObject DirtType;
    public Text progressGauge;              // ���� �����
    private float nowPercent = 0;

    // Start is called before the first frame update
    void Start()
    {
        fillGauge = GetComponent<Image>();

        // ���� �����Ȳ�� ����, �ν�����, ��� ���� ������ %�� ȯ���ؼ� �������ش�.
        dirts = DirtType.GetComponent<DirtsManager>();
        totalCount = dirts.Count;
    }

    // Update is called once per frame
    void Update()
    {
        nowCount = dirts.Count;

        nowProgress = 1 - nowCount / (float) totalCount;
        fillGauge.fillAmount = nowProgress;

        nowPercent = nowProgress * 100;
        progressGauge.text = (int)nowPercent + " %";
    }
}

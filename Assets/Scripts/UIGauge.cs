using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    Image fillGauge;                        // 게이지 바
    float nowProgress = 0f;                 // 현재 진행상황
    int totalCount;                         // 제거해야할 총 갯수
    int nowCount;

    private DirtsManager dirts;             // 먼지, 부스러기, 얼룩 등
    public GameObject DirtType;
    public Text progressGauge;              // 현재 진행률
    private float nowPercent = 0;

    // Start is called before the first frame update
    void Start()
    {
        fillGauge = GetComponent<Image>();

        // 현재 진행상황에 먼지, 부스러기, 얼룩 등의 개수를 %로 환산해서 대입해준다.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStatusMessage : MonoBehaviour
{
    public Text statusText;             // 로봇 상태창

    // Step1.5
    // 3. 배터리가 20% 가 된다
    //    배터리 100% 충전되면 "충전 완료"

    public void BeforeTrashTouch()
    {
        statusText.text = "휴지를 치우러 가자";
    }

    public void DuringTrashTouch()
    {
        statusText.text = "저기로 쓰레기를 옮기자";
    }

    public void BeforeDirtTouch()
    {
        statusText.text = "저 먼지도 치워야겠다";
    }

    public void DuringDirtTouch()
    {
        statusText.text = "미션을 해결할 때마다 진행도가 늘어나";
    }

    public void BeforePaintTouch()
    {
        statusText.text = "저기 얼룩도 지워야겠다";
    }

    public void DuringPaintDouch()
    {
        statusText.text = "물걸레로 닦아야겠군";
    }
}

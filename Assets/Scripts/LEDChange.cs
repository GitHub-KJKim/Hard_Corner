using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 로봇청소기 충전시 LED 색을 변경하는 클래스
/// 
/// 2021.08.11. KJ
/// 
/// </summary>
public class LEDChange : MonoBehaviour
{
    // 1. 하부의 구성 오브젝트를 직접 건드린다. 재질만.
    public GameObject ColorParts;               // 일반 상태일 때 보여져야 할 부분
    public Material NormalColorMaterial;        // 일반 상태일 때의 재질
    public Material ChargingColorMaterial;      // 충전 상태일 때의 재질

    /// 충전 상태일 때 LED 색을 바꿔준다.
    public void OnCharge()
    {
        ChangeColor_Material(true);
    }

    /// 충전 상태에서 벗어났을 때, LED 색을 바꿔준다.
    public void OnReleaseCharging()
    {
        ChangeColor_Material(false);
    }

    public void ChangeColor_Material(bool IsCharging)
    {
        if (IsCharging == true)
        {
            ColorParts.GetComponent<MeshRenderer>().material = ChargingColorMaterial;
        }
        else
        {
            ColorParts.GetComponent<MeshRenderer>().material = NormalColorMaterial;
        }
    }
}

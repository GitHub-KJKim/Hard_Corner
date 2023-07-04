using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �κ�û�ұ� ������ LED ���� �����ϴ� Ŭ����
/// 
/// 2021.08.11. KJ
/// 
/// </summary>
public class LEDChange : MonoBehaviour
{
    // 1. �Ϻ��� ���� ������Ʈ�� ���� �ǵ帰��. ������.
    public GameObject ColorParts;               // �Ϲ� ������ �� �������� �� �κ�
    public Material NormalColorMaterial;        // �Ϲ� ������ ���� ����
    public Material ChargingColorMaterial;      // ���� ������ ���� ����

    /// ���� ������ �� LED ���� �ٲ��ش�.
    public void OnCharge()
    {
        ChangeColor_Material(true);
    }

    /// ���� ���¿��� ����� ��, LED ���� �ٲ��ش�.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �κ�û�ұ� ������ ��ó�� �κ�û�ұⰡ �������� ��
/// - Ư�� �κ��� ���� ����
/// - ���͸��� ���� ��.
/// 
/// 2021.08.13. KJ
/// </summary>
public class CollisonDetector_Charger : MonoBehaviour
{
    private Battery Robot;

    private void Start()
    {
        Robot = GetComponent<Battery>();
    }

    // �κ�û�ұⰡ �������� ������ ���� ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Robot.IsCharged.Equals(false))
        {
            Debug.Log("�κ�û�ұ� ������...");

            // ���͸� ����
            Robot.IsCharged = true;

            // ���� �Ϸ�
            Robot.RechargedComplete();

            // LED ���� ����
            GetComponent<LEDChange>().OnCharge();                           // ������
            other.gameObject.GetComponent<LEDChange>().OnCharge();          // �κ�
        }
    }

    // �κ�û�ұⰡ �������� ������ ��� ��
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�κ�û�ұ� ��������");

        Robot.IsCharged = false;
        GetComponent<LEDChange>().OnReleaseCharging();                      // ������

        if ( other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<LEDChange>().OnReleaseCharging(); // �κ�
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 로봇청소기 충전기 근처로 로봇청소기가 접근했을 때
/// - 특정 부분의 색을 변경
/// - 배터리가 충전 됨.
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

    // 로봇청소기가 충전기의 영역에 들어올 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Robot.IsCharged.Equals(false))
        {
            Debug.Log("로봇청소기 충전중...");

            // 배터리 충전
            Robot.IsCharged = true;

            // 충전 완료
            Robot.RechargedComplete();

            // LED 색을 변경
            GetComponent<LEDChange>().OnCharge();                           // 충전기
            other.gameObject.GetComponent<LEDChange>().OnCharge();          // 로봇
        }
    }

    // 로봇청소기가 충전기의 영역을 벗어날 때
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("로봇청소기 충전중지");

        Robot.IsCharged = false;
        GetComponent<LEDChange>().OnReleaseCharging();                      // 충전기

        if ( other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<LEDChange>().OnReleaseCharging(); // 로봇
        }
    }
}

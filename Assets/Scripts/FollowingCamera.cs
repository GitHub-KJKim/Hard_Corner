using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 플레이어(로봇청소기)를 따라다니는 카메라 클래스
/// 
/// 2021.08.03. KJ
/// 
/// </summary>
public class FollowingCamera : MonoBehaviour
{    
    [SerializeField] Transform cameraArm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));           // 마우스가 움직인 수치와
        Vector3 cameraAnlge = cameraArm.rotation.eulerAngles;                                           // 카메라 암의 회전값을 오일러값으로 변환한 값을 더해서

        cameraArm.rotation = Quaternion.Euler(cameraAnlge.x - mouseDelta.y, cameraAnlge.y + mouseDelta.x, cameraAnlge.z);          // 카메라 암의 회전값을 구함.
    }
}

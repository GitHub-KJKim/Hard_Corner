using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �÷��̾�(�κ�û�ұ�)�� ����ٴϴ� ī�޶� Ŭ����
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
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));           // ���콺�� ������ ��ġ��
        Vector3 cameraAnlge = cameraArm.rotation.eulerAngles;                                           // ī�޶� ���� ȸ������ ���Ϸ������� ��ȯ�� ���� ���ؼ�

        cameraArm.rotation = Quaternion.Euler(cameraAnlge.x - mouseDelta.y, cameraAnlge.y + mouseDelta.x, cameraAnlge.z);          // ī�޶� ���� ȸ������ ����.
    }
}

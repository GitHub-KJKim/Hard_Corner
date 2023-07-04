using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �κ�û�ұ��� '���� �ű��' �̼Ǽ��� ���θ� �����ϴ� ��ũ��Ʈ
/// 
/// 2021.08.14. KJ
/// 
/// </summary>
public class CollisionDetector_ObjectDestination : MonoBehaviour
{
    private bool IsMoved = false;

    public GameObject targetObject;             // �̵��Ǿ��� ������Ʈ

    public bool IsMovedComplete { get { return IsMoved; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == targetObject.name)
        {
            IsMoved = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == targetObject.name)
        {
            IsMoved = false;
        }
    }
}



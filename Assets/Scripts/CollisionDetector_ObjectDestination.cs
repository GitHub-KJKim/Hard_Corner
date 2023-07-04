using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 로봇청소기의 '물건 옮기기' 미션수행 여부를 판정하는 스크립트
/// 
/// 2021.08.14. KJ
/// 
/// </summary>
public class CollisionDetector_ObjectDestination : MonoBehaviour
{
    private bool IsMoved = false;

    public GameObject targetObject;             // 이동되어질 오브젝트

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



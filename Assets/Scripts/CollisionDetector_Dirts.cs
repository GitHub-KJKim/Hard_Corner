using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 바닥의 먼지와 로봇청소기의 충돌을 처리하는 스크립트
/// : 지나가면 먼지 흡입. 얼룩지우는 것과 내용이 똑같다.
/// 
/// 2021.08.15. KJ
/// 
/// </summary>

public class CollisionDetector_Dirts : MonoBehaviour
{
    Collider playerCollider;
    Collider myCollider;
    float playerSize;               // 로봇 콜라이더의 크기를 저장할 변수
    float myColliderSize;           // 얼룩 콜라이더의 크기를 저장할 변수
    float sizeDiff;                 // 위의 두 콜라이더의 크기 차이

    private DirtsManager dirts;

    private void Start()
    {
        // 얼룩의 콜라이더
        myCollider = GetComponent<BoxCollider>();
        myColliderSize = Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y);

        dirts = this.transform.GetComponentInParent<DirtsManager>();      // 부모의 스크립트에 접근
    }

    private void Update()
    {
        if (playerCollider != null)
        {
            // 로봇청소기의 위치에서 얼룩의 위치를 뺀 벡터로
            Vector3 tempdistance = playerCollider.transform.position - this.transform.position;

            // 거리를 구한다
            float distance = tempdistance.magnitude;

            //Debug.Log("청소기와 얼룩의 거리 : " + distance);

            // 그 거리가 일정거리 이하면
            if (distance <= sizeDiff)
            {
                // 비활성화 시킨다 (또는 삭제한다)
                this.gameObject.SetActive(false);
                dirts.Count--;

                //SoundManager.Instance.StopSFXSound();

                SoundManager.Instance.PlaySFXSound("Suction_Crumbs", 0.7f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 로봇청소기가 먼지 위를 지나갈 때
        if (other.gameObject.tag == "Player")
        {
            // 로봇청소기의 콜라이더 정보를 받아
            playerCollider = other;

            // 로봇청소기의 사이즈를 구하고
            playerSize = Mathf.Min(playerCollider.bounds.extents.x, playerCollider.bounds.extents.z);

            // 로봇청소기와 얼룩의 콜라이더 박스의 크기 차이를 구한다
            sizeDiff = playerSize - myColliderSize;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollider = null;
        }
    }
}

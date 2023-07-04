using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 바닥의 얼룩(object)과 로봇청소기의 충돌을 처리하는 스크립트
/// : 지나가면 얼룩이나 먼지가 사라짐
/// 
/// 2021.08.06. KJ
/// 
/// </summary>

public class CollisionDetector_DirtySpot : MonoBehaviour
{
    Collider eraserCollider;
    Collider myCollider;
    float eraserSize;               // 로봇 콜라이더의 크기를 저장할 변수
    float myColliderSize;           // 얼룩 콜라이더의 크기를 저장할 변수
    float sizeDiff;                    // 위의 두 콜라이더의 크기 차이

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
        if (eraserCollider != null)
        {
            // 로봇청소기의 위치에서 얼룩의 위치를 뺀 벡터로
            Vector3 tempdistance = eraserCollider.transform.position - this.transform.position;

            // 거리를 구한다
            float distance = tempdistance.magnitude;

            //Debug.Log("청소기와 얼룩의 거리 : " + distance);

            // 그 거리가 일정거리 이하면
            if (distance <= sizeDiff)
            {
                // 비활성화 시킨다 (또는 삭제한다)
                this.gameObject.SetActive(false);
                dirts.Count--;

                //Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 로봇청소기가 얼룩의 위를 지나갈 때
        if (other.gameObject.tag == "Towel")
        {
            // 로봇청소기의 콜라이더 정보를 받아
            eraserCollider = other;

            // 로봇청소기의 사이즈를 구하고
            eraserSize = Mathf.Min(eraserCollider.bounds.extents.x, eraserCollider.bounds.extents.z);

            // 로봇청소기와 얼룩의 콜라이더 박스의 크기 차이를 구한다
            sizeDiff = eraserSize - myColliderSize;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Towel")
        {
            eraserCollider = null;
        }
    }
}
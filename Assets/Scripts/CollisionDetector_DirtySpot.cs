using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �ٴ��� ���(object)�� �κ�û�ұ��� �浹�� ó���ϴ� ��ũ��Ʈ
/// : �������� ����̳� ������ �����
/// 
/// 2021.08.06. KJ
/// 
/// </summary>

public class CollisionDetector_DirtySpot : MonoBehaviour
{
    Collider eraserCollider;
    Collider myCollider;
    float eraserSize;               // �κ� �ݶ��̴��� ũ�⸦ ������ ����
    float myColliderSize;           // ��� �ݶ��̴��� ũ�⸦ ������ ����
    float sizeDiff;                    // ���� �� �ݶ��̴��� ũ�� ����

    private DirtsManager dirts;

    private void Start()
    {
        // ����� �ݶ��̴�
        myCollider = GetComponent<BoxCollider>();
        myColliderSize = Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y);

        dirts = this.transform.GetComponentInParent<DirtsManager>();      // �θ��� ��ũ��Ʈ�� ����
    }

    private void Update()
    {
        if (eraserCollider != null)
        {
            // �κ�û�ұ��� ��ġ���� ����� ��ġ�� �� ���ͷ�
            Vector3 tempdistance = eraserCollider.transform.position - this.transform.position;

            // �Ÿ��� ���Ѵ�
            float distance = tempdistance.magnitude;

            //Debug.Log("û�ұ�� ����� �Ÿ� : " + distance);

            // �� �Ÿ��� �����Ÿ� ���ϸ�
            if (distance <= sizeDiff)
            {
                // ��Ȱ��ȭ ��Ų�� (�Ǵ� �����Ѵ�)
                this.gameObject.SetActive(false);
                dirts.Count--;

                //Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �κ�û�ұⰡ ����� ���� ������ ��
        if (other.gameObject.tag == "Towel")
        {
            // �κ�û�ұ��� �ݶ��̴� ������ �޾�
            eraserCollider = other;

            // �κ�û�ұ��� ����� ���ϰ�
            eraserSize = Mathf.Min(eraserCollider.bounds.extents.x, eraserCollider.bounds.extents.z);

            // �κ�û�ұ�� ����� �ݶ��̴� �ڽ��� ũ�� ���̸� ���Ѵ�
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
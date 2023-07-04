using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �ٴ��� ������ �κ�û�ұ��� �浹�� ó���ϴ� ��ũ��Ʈ
/// : �������� ���� ����. �������� �Ͱ� ������ �Ȱ���.
/// 
/// 2021.08.15. KJ
/// 
/// </summary>

public class CollisionDetector_Dirts : MonoBehaviour
{
    Collider playerCollider;
    Collider myCollider;
    float playerSize;               // �κ� �ݶ��̴��� ũ�⸦ ������ ����
    float myColliderSize;           // ��� �ݶ��̴��� ũ�⸦ ������ ����
    float sizeDiff;                 // ���� �� �ݶ��̴��� ũ�� ����

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
        if (playerCollider != null)
        {
            // �κ�û�ұ��� ��ġ���� ����� ��ġ�� �� ���ͷ�
            Vector3 tempdistance = playerCollider.transform.position - this.transform.position;

            // �Ÿ��� ���Ѵ�
            float distance = tempdistance.magnitude;

            //Debug.Log("û�ұ�� ����� �Ÿ� : " + distance);

            // �� �Ÿ��� �����Ÿ� ���ϸ�
            if (distance <= sizeDiff)
            {
                // ��Ȱ��ȭ ��Ų�� (�Ǵ� �����Ѵ�)
                this.gameObject.SetActive(false);
                dirts.Count--;

                //SoundManager.Instance.StopSFXSound();

                SoundManager.Instance.PlaySFXSound("Suction_Crumbs", 0.7f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �κ�û�ұⰡ ���� ���� ������ ��
        if (other.gameObject.tag == "Player")
        {
            // �κ�û�ұ��� �ݶ��̴� ������ �޾�
            playerCollider = other;

            // �κ�û�ұ��� ����� ���ϰ�
            playerSize = Mathf.Min(playerCollider.bounds.extents.x, playerCollider.bounds.extents.z);

            // �κ�û�ұ�� ����� �ݶ��̴� �ڽ��� ũ�� ���̸� ���Ѵ�
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

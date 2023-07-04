using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ����, ��� ������Ʈ�� �����ϴ� ��ũ��Ʈ
/// 
/// 2021.08.19. KJ w/ ������.
/// 
/// </summary>
public class DirtsManager : MonoBehaviour
{
    // ����, ����� ������ ����Ʈ
    public List<Transform> GetDirts { get { return dirts; } }
    private List<Transform> dirts;

    // ���� �Ǵ� ����� ������Ʈ ��
    public int Count { get { return count; } set { count = value; } }
    private int count;

    // Start is called before the first frame update
    void Awake()
    {
        dirts = new List<Transform>();

        var allChildObjects = this.transform.GetComponentsInChildren<Transform>();      // �ڽ��� ������Ʈ�� �� �������.

        foreach (var item in allChildObjects)
        {
            dirts.Add(item);
        }

        count = dirts.Count - 1;        // ������ ����(�θ��� ���� 1�� ����)
       
        // Debug.Log(count);
    }
}

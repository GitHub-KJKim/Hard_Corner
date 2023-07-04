using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 먼지, 얼룩 오브젝트를 관리하는 스크립트
/// 
/// 2021.08.19. KJ w/ 송정훈.
/// 
/// </summary>
public class DirtsManager : MonoBehaviour
{
    // 먼지, 얼룩을 관리할 리스트
    public List<Transform> GetDirts { get { return dirts; } }
    private List<Transform> dirts;

    // 먼지 또는 얼룩의 오브젝트 수
    public int Count { get { return count; } set { count = value; } }
    private int count;

    // Start is called before the first frame update
    void Awake()
    {
        dirts = new List<Transform>();

        var allChildObjects = this.transform.GetComponentsInChildren<Transform>();      // 자식의 오브젝트를 다 집어넣음.

        foreach (var item in allChildObjects)
        {
            dirts.Add(item);
        }

        count = dirts.Count - 1;        // 먼지의 개수(부모의 개수 1개 뺀다)
       
        // Debug.Log(count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForTutorial : MonoBehaviour
{
    // 튜토리얼 전용 변수
    private UIStatusMessage statusText;

    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<UIStatusMessage>();
    }
}

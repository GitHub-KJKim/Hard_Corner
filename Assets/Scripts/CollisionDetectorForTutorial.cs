using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForTutorial : MonoBehaviour
{
    // Ʃ�丮�� ���� ����
    private UIStatusMessage statusText;

    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<UIStatusMessage>();
    }
}

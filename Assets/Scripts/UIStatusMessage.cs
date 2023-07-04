using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStatusMessage : MonoBehaviour
{
    public Text statusText;             // �κ� ����â

    // Step1.5
    // 3. ���͸��� 20% �� �ȴ�
    //    ���͸� 100% �����Ǹ� "���� �Ϸ�"

    public void BeforeTrashTouch()
    {
        statusText.text = "������ ġ�췯 ����";
    }

    public void DuringTrashTouch()
    {
        statusText.text = "����� �����⸦ �ű���";
    }

    public void BeforeDirtTouch()
    {
        statusText.text = "�� ������ ġ���߰ڴ�";
    }

    public void DuringDirtTouch()
    {
        statusText.text = "�̼��� �ذ��� ������ ���൵�� �þ";
    }

    public void BeforePaintTouch()
    {
        statusText.text = "���� ��赵 �����߰ڴ�";
    }

    public void DuringPaintDouch()
    {
        statusText.text = "���ɷ��� �۾ƾ߰ڱ�";
    }
}

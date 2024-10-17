using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr
{
    public Action keyAction = null;

    public void OnUpdate()
    {
        // �Է¹��� Ű�� �ƹ��͵� ���ٸ� ����
        if (Input.anyKey == false) return;

        // Ư�� Ű�� ���Դٸ� keyaction���� �̺�Ʈ�� �߻������� ����
        if(keyAction != null)
        {
            keyAction.Invoke();
        }
    }
}

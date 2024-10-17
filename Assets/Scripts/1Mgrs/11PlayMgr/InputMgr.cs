using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr
{
    public Action keyAction = null;

    public void OnUpdate()
    {
        // 입력받은 키가 아무것도 없다면 종료
        if (Input.anyKey == false) return;

        // 특정 키가 들어왔다면 keyaction에서 이벤트가 발생했음을 전파
        if(keyAction != null)
        {
            keyAction.Invoke();
        }
    }
}

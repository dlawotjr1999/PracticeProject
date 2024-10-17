using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorAim : MonoBehaviour
{

    private void SetRotation()
    {
        Vector3 target = PlayMgr.PlayerTarget;
        Vector3 offset = target - transform.position;
        float deg = FunctionDefine.VecToDeg(new(offset.x, offset.z));
        transform.eulerAngles = new(0, deg, 0);
    }

    private void Update()
    {
        SetRotation();
    }
}

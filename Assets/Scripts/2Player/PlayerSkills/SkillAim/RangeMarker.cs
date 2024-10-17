using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMarker : MonoBehaviour
{
    public void SetRadius(ESkillName _skill)
    {
        if (SkillValues.GetSkillInfo(_skill) is not AroundSkillInfo info) { Debug.LogError("�߸��� ��ų ���� �Է�"); return; }
        float radius = info.Radius;
        transform.localScale = new(radius, radius);
    }
}

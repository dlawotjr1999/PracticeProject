using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMarker : MonoBehaviour
{
    public void SetRadius(ESkillName _skill)
    {
        if (SkillValues.GetSkillInfo(_skill) is not AroundSkillInfo info) { Debug.LogError("잘못된 스킬 정보 입력"); return; }
        float radius = info.Radius;
        transform.localScale = new(radius, radius);
    }
}

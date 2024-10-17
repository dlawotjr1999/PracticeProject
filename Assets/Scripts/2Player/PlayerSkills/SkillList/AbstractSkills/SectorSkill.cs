using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSkill : PlayerSkill
{
    private float m_radius;             // 스킬 반지름
    private float m_lastTime;           // 유지 시간
    private float m_degree;             // 사이각
        
    public override void SetSkillInfo(SkillInfo _info)
    {
        SectorSkillInfo info = (SectorSkillInfo)_info;
        base.SetSkillInfo(info);
        m_radius = info.Radius;
        m_lastTime = info.LastTime;
        m_degree = info.Degree;
    }

    public override void SetAim(Vector3 _aim)
    {
        float deg = FunctionDefine.VecToDeg(new(_aim.x, _aim.z));
        transform.eulerAngles = new(0, deg, 0);
    }


    private void Start()
    {
        Destroy(gameObject, m_lastTime);
    }
}

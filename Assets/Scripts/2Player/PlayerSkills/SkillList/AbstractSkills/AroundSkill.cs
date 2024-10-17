using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundSkill : PlayerSkill
{
    private float m_radius;             // ��ų ������
    private float m_lastTime;           // ���� �ð�
        
    public override void SetSkillInfo(SkillInfo _info)
    {
        AroundSkillInfo info = (AroundSkillInfo)_info;
        base.SetSkillInfo(info);
        m_radius = info.Radius;
        m_lastTime = info.LastTime;
    }




    private void Start()
    {
        Destroy(gameObject, m_lastTime);
    }
}

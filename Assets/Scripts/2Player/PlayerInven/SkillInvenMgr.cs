using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenMgr : MonoBehaviour
{
    private bool[] m_haveSkill = new bool[(int)ESkillName.LAST];        // ��� ��ų ���� ����
    public bool HaveSkill(ESkillName _skill) { return m_haveSkill[(int)_skill]; }  // ��ų ���� ����

    public void ObtainSkill(ESkillName _skill)                                                              // ��ų ȹ��
    { 
        if (HaveSkill(_skill)) { Debug.LogError("��ų �̹� ������"); return; }
        m_haveSkill[(int)_skill] = true; 
    }





    private void InitInventory()            // �κ��丮 �ʱ� ����
    {
        ObtainSkill(ESkillName.SHOOT_NONE); // (�⺻ ���� ���)
    }

    private void Start()
    {
        InitInventory();
    }
}
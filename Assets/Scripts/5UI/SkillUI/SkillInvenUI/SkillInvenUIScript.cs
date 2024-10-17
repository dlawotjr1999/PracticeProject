using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenUIScript : DragDropUI, IToggleUI
{
    private SkillInvenElm[] m_holders;
    private int CurIdx { get; set; } = 0;

    public void AddSkill(ESkillName _skill)
    {
        if(m_holders.Length <= CurIdx) { Debug.LogError("�κ��丮 ���� �ʰ�"); return; }

        m_holders[CurIdx++].SetSkill(_skill);
    }

    public void SlotChanged()
    {
        ESkillName[] skillSlot = PlayMgr.PlayerSkillSlot;
        for (int i = 0; i<m_holders.Length; i++)
        {
            bool selected = false;
            ESkillName invenSkill = m_holders[i].CurSkill;
            for (int j = 0; j<skillSlot.Length; j++)
            {
                if (invenSkill == skillSlot[j])
                {
                    selected = true;
                    break;
                }
            }
            m_holders[i].SetSelect(selected);
        }
    }



    public void ToggleOn() { if (!gameObject.activeSelf) gameObject.SetActive(true); }
    public void ToggleOff() { if (gameObject.activeSelf) gameObject.SetActive(false); }

    public override void SetComps()
    {
        base.SetComps();
        m_holders = GetComponentsInChildren<SkillInvenElm>();
    }

    private void Start()
    {
        ToggleOff();
    }
}

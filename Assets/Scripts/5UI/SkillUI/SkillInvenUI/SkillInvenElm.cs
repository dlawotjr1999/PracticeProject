using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillInvenElm : MonoBehaviour       // ������
{
    private SkillInvenUIScript m_parent;                // �Ҽӵ� UI
    public void SetParent(SkillInvenUIScript _parent) { m_parent = _parent; }

    private RectTransform m_rect;
    private EventTrigger m_eventTrigger;

    public ESkillName CurSkill { get; private set; } = ESkillName.LAST;     // �ش��ϴ� ��ų
    private Image m_skillIcon;                                              // ��ų ������ �̹���

    public bool Selected { get; set; }

    public void SetSelect(bool _select)
    {
        if (CurSkill == ESkillName.LAST) { return; }

        if (_select)
        {
            m_skillIcon.color = Color.red;
            Selected = true;
        }
        else
        {
            m_skillIcon.color = Color.white;
            Selected = false;
        }
    }

    public void SetSkill(ESkillName _skill)                     // �ش��ϴ� ��ų ǥ��
    {
        CurSkill = _skill;
        Sprite skillIcon = PlayMgr.GetSkillIcon(CurSkill);
        m_skillIcon.sprite = skillIcon;
        m_skillIcon.color = Color.white;
    }
    public void CreateCarrier(PointerEventData _data)               // ���� ��Ͽ� ����� ������Ʈ ����
    {
        if (Selected) { return; }
        GameObject carrier = Instantiate(PlayMgr.SkillCarrierPrefab, PlayMgr.ControlCanvasTrans);
        carrier.GetComponent<RectTransform>().position = m_rect.position;               // ���� ��ġ��
        carrier.GetComponent<SkillCarrier>().SetCarrier(CurSkill);                      // ����� ��ų ����
    }


    private void SetComps()
    {
        m_rect = GetComponent<RectTransform>();
        m_eventTrigger = GetComponent<EventTrigger>();
        m_skillIcon = GetComponentInChildren<Image>();
    }

    private void SetEvent()
    {
        FunctionDefine.AddEvent(m_eventTrigger, EventTriggerType.PointerDown, CreateCarrier);
    }

    private void Awake()
    {
        SetComps();
        SetEvent();
    }
}

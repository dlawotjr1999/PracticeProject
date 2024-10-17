using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SkillCarrier : MonoBehaviour               // ��ų �κ� => ���� ��Ͽ� ���Ǵ� ������Ʈ�� �ٴ� Ŭ����
{
    private SkillInvenElm m_parent;                                                  // �Ҽӵ� Ȧ��
    public void SetParent(SkillInvenElm _parent) { m_parent = _parent; }

    private RectTransform m_rect;

    private int CurAdj = -1;

    private const float AdjDist = 33;

    private Vector2 BeginPos { get; set; }
    private Vector2 BeginMouse { get; set; }

    private ESkillName CurSkill { get; set; }
    public void SetCarrier(ESkillName _skill)
    {
        CurSkill = _skill;
        GetComponent<Image>().sprite = PlayMgr.GetSkillIcon(CurSkill);
    }

    private void BeginDrag()
    {
        BeginPos = m_rect.position;
        BeginMouse = Input.mousePosition;
    }

    private void OnDrag()
    {
        Vector2 mouse = Input.mousePosition;
        Vector2 offset = mouse - BeginMouse;
        m_rect.position = BeginPos + offset;
        AdjPos();
    }

    private void EndDrag()
    {
        if (CurAdj >= 0)
        {
            // ������ ��ġ��� ��ų ���
            PlayMgr.SetSkillSlot(CurSkill, CurAdj);
        }
        Destroy(gameObject);
    }

    private void AdjPos()
    {
        CurAdj = -1;
        float minDist = AdjDist + 1;
        for (int i = 0; i<3; i++)
        {
            RectTransform slot = PlayMgr.SlotTrans[i];
            float dist = (slot.position - m_rect.position).magnitude;
            if (dist <= AdjDist && dist < minDist)
            {
                m_rect.position = slot.position;
                minDist = dist;
                CurAdj = i;
            }
        }
    }

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        BeginDrag();
    }

    private void Update()
    {
        OnDrag();
        if(Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingListElm : MonoBehaviour
{
    private SetShootingUI m_parent;
    public void SetParent(SetShootingUI _parent) { m_parent = _parent; }

    private Button m_btn;
    private Image m_skillState;

    [SerializeField]
    private ESkillElement m_element;                        // 스킬 속성

    private Color IdleColor = new(0, 0, 0, 0);              // 해제 상태 색깔
    private Color SelColor = new(54f, 255f, 0, 0);          // 선택 상태 색깔

    public bool CanUse { get; private set; } = true;                // 사용 가능한지
    public bool Selected { get; private set; }              // 선택됨 여부

    public void ObtainShooting()
    {
        CanUse = true;
        m_skillState.color = IdleColor;
    }
    public void SelectShooting()
    {
        if (!CanUse || Selected) return;

        m_parent.SelectShooting(m_element);
    }

    public void SetCurElm()
    {
        Selected = true;
        m_skillState.color = SelColor;
    }
    public void ReleaseCurElm()
    {
        Selected = false;
        m_skillState.color = IdleColor;
    }


    private void Awake()
    {
        m_btn = GetComponent<Button>();
        m_btn.onClick.AddListener(SelectShooting);
        m_skillState = GetComponentsInChildren<Image>()[2];
    }
}

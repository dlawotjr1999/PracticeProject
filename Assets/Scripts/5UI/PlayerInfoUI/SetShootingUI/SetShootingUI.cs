using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetShootingUI : MonoBehaviour
{
    private Animator m_listAnim;

    private CurShootingUI m_curShooting;
    private ShootingListElm[] m_shootingElms = new ShootingListElm[(int)ESkillElement.LAST];

    public bool Opened { get; private set; }

    private ESkillElement CurElement { get; set; } = ESkillElement.LAST;

    public void OpenList() { m_listAnim.SetTrigger("OPEN"); Opened = true; }
    public void CloseList() { m_listAnim.SetTrigger("CLOSE"); Opened = false; }

    public void ObtainShooting(ESkillElement _element)
    {
        m_shootingElms[(int)_element].ObtainShooting();
    }
    public void SelectShooting(ESkillElement _element)
    {
        if(CurElement != ESkillElement.LAST) { m_shootingElms[(int)CurElement].ReleaseCurElm(); }
        CurElement = _element;
        PlayMgr.SetCurAttack(CurElement);
        m_curShooting.SetCurShooting(CurElement);
        m_shootingElms[(int)CurElement].SetCurElm();
        if(Opened)
            CloseList();
    }

    public void StartShootingCoolTime(float _coolTime) { m_curShooting.UseShooting(_coolTime); }

    private void SetComps()
    {
        m_listAnim = GetComponentInChildren<Animator>();
        m_curShooting = GetComponentInChildren<CurShootingUI>();
        m_curShooting.SetParent(this);
        ShootingListElm[] elms = GetComponentsInChildren<ShootingListElm>();
        for (int i = 0; i<elms.Length; i++)
        {
            m_shootingElms[i] = elms[elms.Length - i - 1];
        }
        foreach(ShootingListElm elm in m_shootingElms) { elm.SetParent(this); }
    }

    private void Awake()
    {
        SetComps();
    }

    private void Start()
    {
        ObtainShooting(ESkillElement.NONE);
        SelectShooting(ESkillElement.NONE);
    }
}

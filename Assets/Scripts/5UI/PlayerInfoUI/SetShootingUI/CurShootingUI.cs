using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurShootingUI : MonoBehaviour
{
    private SetShootingUI m_parent;
    public void SetParent(SetShootingUI _parent) { m_parent = _parent; }

    private Button m_btn;

    private Image m_curShootingImg;
    private Image m_coolTimeImg;

    private float MaxCoolTime { get; set; }
    private float CurCoolTime { get; set; }


    private void ClickUI()
    {
        if(m_parent.Opened)
        {
            m_parent.CloseList();
        }
        else
        {
            m_parent.OpenList();
        }
    }

    public void SetCurShooting(ESkillElement _element)
    {
        ESkillName skill = (ESkillName)((int)_element);
        m_curShootingImg.sprite = PlayMgr.GetSkillIcon(skill);
    }

    public void UseShooting(float _coolTime)
    {
        MaxCoolTime = _coolTime;
        CurCoolTime = _coolTime;
        StartCoroutine(CoolTimeCnt());
    }

    private IEnumerator CoolTimeCnt()
    {
        while (CurCoolTime > 0)
        {
            CurCoolTime -= Time.deltaTime;
            SetCoolTimeImg(CurCoolTime);
            yield return null;
        }
        CurCoolTime = 0;
        SetCoolTimeImg(CurCoolTime);
    }

    private void SetCoolTimeImg(float _cur)
    {
        m_coolTimeImg.fillAmount = _cur / MaxCoolTime;
    }


    private void Awake()
    {
        m_btn = GetComponent<Button>();
        m_btn.onClick.AddListener(ClickUI);

        Image[] imgs = GetComponentsInChildren<Image>();
        m_curShootingImg = imgs[1];
        m_coolTimeImg = imgs[2];
    }

}

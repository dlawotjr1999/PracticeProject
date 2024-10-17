using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotElm : MonoBehaviour
{
    public RectTransform m_rect { get; private set; }

    private Image m_iconImg;
    private Image m_coolTimeImg;

    private float CurCoolTime { get; set; } = 0;

    public void SetSkillSlot(Sprite _icon)
    {
        m_iconImg.sprite = _icon;
        m_iconImg.color = Color.white;
    }

    public void StartCoolTime(float _coolTime) { SetCoolTime(1); StartCoroutine(CoolTimeProc(_coolTime)); }
    public void CoolTimeDone() { SetCoolTime(0); }

    private IEnumerator CoolTimeProc(float _coolTime)
    {
        CurCoolTime = _coolTime;
        while(CurCoolTime > 0)
        {
            CurCoolTime -= Time.deltaTime;
            SetCoolTime(CurCoolTime / _coolTime);
            yield return null;
        }
        CoolTimeDone();
    }

    private void SetCoolTime(float _per)
    {
        m_coolTimeImg.fillAmount = _per;
    }

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        Image[] imgs = GetComponentsInChildren<Image>();
        m_iconImg = imgs[0];
        m_coolTimeImg = imgs[2];
    }
}

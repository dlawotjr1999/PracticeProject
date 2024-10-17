using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanelScript : MonoBehaviour
{
    private RewardPopupScript m_parent;                                             // 속한 팝업 UI
    public void SetParent(RewardPopupScript _parent) { m_parent = _parent; }        // ㄴ 설정

    private Image m_iconImg;                    // 스킬 아이콘
    private TextMeshProUGUI m_nameTmp;          // 스킬 이름
    private TextMeshProUGUI m_descTmp;          // 스킬 설명

    private RewardInfo CurReward { get; set; }

    public void SelectReward()
    {
        m_parent.SelectReward(CurReward);
    }

    public void SetSkillPanel(RewardInfo _reward)
    {
        CurReward = _reward;
        m_iconImg.sprite = PlayMgr.GetRewardIcon(_reward);
        m_nameTmp.text = CurReward.RewardName;
        m_descTmp.text = CurReward.RewardDesc;
    }

    private void SetComps()
    {
        Image[] imgs = GetComponentsInChildren<Image>();
        m_iconImg = imgs[2];
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        m_nameTmp = tmps[0];
        m_descTmp = tmps[1];
    }

    private void SetButton()
    {
        GetComponent<Button>().onClick.AddListener(SelectReward);
    }

    private void Awake()
    {
        SetComps();
        SetButton();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPopupScript : MonoBehaviour
{
    private RewardPanelScript[] m_panels;   // 각 패널들

    private RewardInfo[] m_curRewards;      // 현재 보상 (보상 중복 방지용)

    public void Popup(bool _skill)          // UI 등장 시 각 보상 패널 설정
    {
        m_curRewards = new RewardInfo[m_panels.Length];
        for(int i=0;i<m_panels.Length;i++)
        {
            RewardInfo randomInfo;
            do
            {
                if (_skill)
                    randomInfo = PlayMgr.GetRandomSkillInfo();
                else
                    randomInfo = PlayMgr.GetRandomStatInfo();
            } while(!ChkReward(randomInfo));
            m_panels[i].SetSkillPanel(randomInfo);
            m_curRewards[i] = randomInfo;
        }
    }
    private bool ChkReward(RewardInfo _reward)
    {
        if (m_curRewards[0] == null) return true;
        if (m_curRewards[0] != null && m_curRewards[1] == null && m_curRewards[0].RewardName != _reward.RewardName) return true;
        if (m_curRewards[0] != null && m_curRewards[1] != null && m_curRewards[0].RewardName != _reward.RewardName &&
            m_curRewards[1].RewardName != _reward.RewardName) return true;
        return false;
    }

    public void SelectReward(RewardInfo _reward)
    {
        PlayMgr.SelectReward(_reward); // 보상 선택 결과 함수

        ClosePopup();
        GameMgr.ResumeGame();
    }

    private void ClosePopup()
    {
        // 팝업 닫기
        Destroy(gameObject);
    }

    private void SetComps()
    {
        m_panels = GetComponentsInChildren<RewardPanelScript>();
        foreach(RewardPanelScript panel in m_panels) { panel.SetParent(this); }
    }

    private void Awake()
    {
        SetComps();
    }

    private void Start()
    {
        GameMgr.PauseGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPopupScript : MonoBehaviour
{
    private RewardPanelScript[] m_panels;   // �� �гε�

    private RewardInfo[] m_curRewards;      // ���� ���� (���� �ߺ� ������)

    public void Popup(bool _skill)          // UI ���� �� �� ���� �г� ����
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
        PlayMgr.SelectReward(_reward); // ���� ���� ��� �Լ�

        ClosePopup();
        GameMgr.ResumeGame();
    }

    private void ClosePopup()
    {
        // �˾� �ݱ�
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

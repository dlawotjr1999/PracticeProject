using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RewardInfo                // ���� ���� ���� Ŭ����
{
    public string RewardName { get; private set; }      // ���� �̸� (���� ���� â�� ǥ�õ�)
    public string RewardDesc { get; private set; }      // ���� ����
    public int Prob { get; private set; }               // ���� ���� Ȯ�� (���� ���࿡ ���� ���� ����?)
    public void SetProb(int _prob) { Prob = _prob; }
    public int CurLevel { get; private set; }           // ������ ���� ���� (���� ��� �� �÷��̾� �������� ������)
    public bool IsSkill { get; protected set; }         // ��ų ��������
    public void SetCurLevel(int _level) { CurLevel = _level; }
    public RewardInfo(string _name, int _prob, string _desc) { RewardName = _name; Prob = _prob; RewardDesc = _desc; }
}

public class StatRewardInfo : RewardInfo        // ���� ���� ���� Ŭ����
{
    public EPlayerStat StatName { get; private set; }   // ���� enum
    public StatRewardInfo(EPlayerStat _stat, string _name, int _prob, string _desc) : base(_name, _prob, _desc) { StatName = _stat; IsSkill = false; }
}

public class SkillRewardInfo : RewardInfo       // ��ų ���� ���� Ŭ����
{
    public ESkillName SkillName { get; private set; }   // ��ų enum
    public SkillRewardInfo(ESkillName _skill, string _name, int _prob, string _desc) : base(_name, _prob, _desc) { SkillName = _skill; IsSkill = true; }
}

public class RewardMgr : MonoBehaviour
{
    private RewardInfo[] m_rewardsInfos = new RewardInfo[]                  // ��ü ���� ����
        {
            // ���� ���� ������
            new StatRewardInfo(EPlayerStat.DAMAGE, "������ ����", 25,
                "���� ��������\r\n10% �����մϴ�."),
            new StatRewardInfo(EPlayerStat.MOVESPEED, "�̵� �ӵ� ����", 25,
                "�̵��ӵ���\r\n10% �����մϴ�."),
            new StatRewardInfo(EPlayerStat.ATTACKSPEED, "���� �ӵ� ����", 25,
                "�⺻������\r\n��Ÿ���� 10%\r\n�����մϴ�."),
            new StatRewardInfo(EPlayerStat.MAXHP, "�ִ� ü�� ����", 25,
                "�ִ� ü����\r\n10 �����մϴ�."),
            new StatRewardInfo(EPlayerStat.CRITICAL, "ġ��Ÿ�� ����", 25,
                "ġ��Ÿ����\r\n10% �����մϴ�."),
            new StatRewardInfo(EPlayerStat.EVASION, "ȸ���� ����", 25,
                "ȸ������\r\n10% �����մϴ�."),

            new SkillRewardInfo(ESkillName.SHOOT_FIRE, "�⺻ ����-�ҼӼ�" , 6,
                "[�ҼӼ� �⺻����]�� ����ϴ�.\r\n������: 35" ),
            new SkillRewardInfo(ESkillName.SHOOT_POISON, "�⺻ ����-���Ӽ�" , 6,
                "[���Ӽ� �⺻����]�� ����ϴ�.\r\n������: 30" ),
            new SkillRewardInfo(ESkillName.SHOOT_ELECTRIC, "�⺻ ����-����Ӽ�" , 6,
                "[����Ӽ� �⺻����]�� ����ϴ�.\r\n������: 30" ),
            new SkillRewardInfo(ESkillName.SHOOT_ICE, "�⺻ ����-�����Ӽ�" , 6,
                "[�����Ӽ� �⺻����]�� ����ϴ�.\r\n������: 30" ),

            new SkillRewardInfo(ESkillName.THROW_NONE, "��ô ��ų-���Ӽ�" , 6,
                "[��ô_���Ӽ�]��\r\n����ϴ�.\r\n������: 40" ),
            new SkillRewardInfo(ESkillName.THROW_FIRE, "��ô ��ų-�ҼӼ�" , 6,
                "[��ô_�ҼӼ�]��\r\n����ϴ�.\r\n������: 45" ),
            new SkillRewardInfo(ESkillName.THROW_POISON, "��ô ��ų-���Ӽ�" , 6,
                "[��ô_���Ӽ�]��\r\n����ϴ�.\r\n������: 45" ),
            new SkillRewardInfo(ESkillName.THROW_ELECTRIC, "��ô ��ų-����Ӽ�" , 6,
                "[��ô_����Ӽ�]��\r\n����ϴ�.\r\n������: 45" ),
            new SkillRewardInfo(ESkillName.THROW_ICE, "��ô ��ų-�����Ӽ�" , 6,
                "[��ô_�����Ӽ�]��\r\n����ϴ�.\r\n������: 45" ),

            new SkillRewardInfo(ESkillName.AROUND_NONE, "�ֺ� ��ų-���Ӽ�", 6,
                "[�ֺ�_���Ӽ�]��\r\n����ϴ�.\r\n������: 50"),
            new SkillRewardInfo(ESkillName.AROUND_POISON, "�ֺ� ��ų-���Ӽ�", 6,
                "[�ֺ�_���Ӽ�]��\r\n����ϴ�.\r\n������: 50"),
            new SkillRewardInfo(ESkillName.AROUND_ICE, "�ֺ� ��ų-�����Ӽ�", 6,
                "[�ֺ�_�����Ӽ�]��\r\n����ϴ�.\r\n������: 50"),

            new SkillRewardInfo(ESkillName.SECTOR_FIRE, "���� ��ų-�ҼӼ�", 6,
                "[����_�ҼӼ�]��\r\n����ϴ�.\r\n������: 50"),
            new SkillRewardInfo(ESkillName.AROUND_ELECTRIC, "�ֺ� ��ų-����Ӽ�", 6,
                "[�ֺ�_����Ӽ�]��\r\n����ϴ�.\r\n������: 50"),

            new SkillRewardInfo(ESkillName.BUFF, "���� ��ų", 8,
                "[������ų]��\r\n����ϴ�."),
            new SkillRewardInfo(ESkillName.SUMMON, "��ȯ ��ų", 8,
                "[��ȯ��ų]��\r\n����ϴ�."),
        };

    private int TotalProb = 0;      // ���� �������� Ȯ�� ��
    private int SkillProb = 0;      // ��ų ���� Ȯ�� ��
    private int StatProb = 0;

    public void SelectReward(RewardInfo _reward)                        // ���� �� �� ���� �Լ� (RewardPopupScript���� ȣ��)
    {
        if (_reward.IsSkill)
        {
            ESkillName skill = ((SkillRewardInfo)_reward).SkillName;
            if (skill <= ESkillName.SHOOT_ICE)
            {
                PlayMgr.ObtainShooting(skill);
            }
            else
            {
                PlayMgr.ObtainSkill(skill);
            }
        }
        else
        {
            EPlayerStat stat = ((StatRewardInfo)_reward).StatName;
            PlayMgr.UpgradePlayerStat(stat);
        }
    }


    public RewardInfo GetRandomRewardInfo()     // ���� ���� ���� �̱�
    {
        int prob = Random.Range(0, TotalProb);
        return ProbToReward(prob);
    }
    public RewardInfo GetRandomSkillInfo()
    {
        RewardInfo reward;
        ESkillName skill;
        do
        {
            int prob = Random.Range(0, SkillProb) + StatProb;
            reward = ProbToReward(prob);
            skill = ((SkillRewardInfo)reward).SkillName;
        } while (PlayMgr.HaveSkill(skill));
        return reward;
    }
    public RewardInfo GetRandomStatInfo()
    {
        int prob = Random.Range(0, StatProb);
        return ProbToReward(prob);
    }
    private RewardInfo ProbToReward(int _prob)
    {
        int curProb = 0;
        for (int i = 0; i<m_rewardsInfos.Length; i++)
        {
            curProb += m_rewardsInfos[i].Prob;
            if (_prob < curProb)
            {
                return m_rewardsInfos[i];
            }
        }

        Debug.LogError("��ü Ȯ�� �� ���� �߸���");
        return null;
    }

    private void SetTotalProb()                // Ȯ�� �� �ʱ� ����
    {
        foreach (RewardInfo info in m_rewardsInfos)
        {
            TotalProb += info.Prob;
            if (info.IsSkill) SkillProb += info.Prob;
            else StatProb += info.Prob;
        }
    }

    private void Awake()
    {
        SetTotalProb();
    }
}

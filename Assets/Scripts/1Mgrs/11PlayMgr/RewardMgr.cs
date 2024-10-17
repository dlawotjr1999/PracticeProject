using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RewardInfo                // 보상 정보 가상 클래스
{
    public string RewardName { get; private set; }      // 보상 이름 (보상 선택 창에 표시될)
    public string RewardDesc { get; private set; }      // 보상 설명
    public int Prob { get; private set; }               // 보상 등장 확률 (게임 진행에 따라 조정 가능?)
    public void SetProb(int _prob) { Prob = _prob; }
    public int CurLevel { get; private set; }           // 보상의 현재 레벨 (보상 띄울 때 플레이어 정보에서 가져옴)
    public bool IsSkill { get; protected set; }         // 스킬 보상인지
    public void SetCurLevel(int _level) { CurLevel = _level; }
    public RewardInfo(string _name, int _prob, string _desc) { RewardName = _name; Prob = _prob; RewardDesc = _desc; }
}

public class StatRewardInfo : RewardInfo        // 스탯 보상 정보 클래스
{
    public EPlayerStat StatName { get; private set; }   // 스탯 enum
    public StatRewardInfo(EPlayerStat _stat, string _name, int _prob, string _desc) : base(_name, _prob, _desc) { StatName = _stat; IsSkill = false; }
}

public class SkillRewardInfo : RewardInfo       // 스킬 보상 정보 클래스
{
    public ESkillName SkillName { get; private set; }   // 스킬 enum
    public SkillRewardInfo(ESkillName _skill, string _name, int _prob, string _desc) : base(_name, _prob, _desc) { SkillName = _skill; IsSkill = true; }
}

public class RewardMgr : MonoBehaviour
{
    private RewardInfo[] m_rewardsInfos = new RewardInfo[]                  // 전체 보상 정보
        {
            // 스탯 보상 정보들
            new StatRewardInfo(EPlayerStat.DAMAGE, "데미지 증가", 25,
                "최종 데미지가\r\n10% 증가합니다."),
            new StatRewardInfo(EPlayerStat.MOVESPEED, "이동 속도 증가", 25,
                "이동속도가\r\n10% 증가합니다."),
            new StatRewardInfo(EPlayerStat.ATTACKSPEED, "공격 속도 증가", 25,
                "기본공격의\r\n쿨타임이 10%\r\n감소합니다."),
            new StatRewardInfo(EPlayerStat.MAXHP, "최대 체력 증가", 25,
                "최대 체력이\r\n10 증가합니다."),
            new StatRewardInfo(EPlayerStat.CRITICAL, "치명타율 증가", 25,
                "치명타율이\r\n10% 증가합니다."),
            new StatRewardInfo(EPlayerStat.EVASION, "회피율 증가", 25,
                "회피율이\r\n10% 증가합니다."),

            new SkillRewardInfo(ESkillName.SHOOT_FIRE, "기본 공격-불속성" , 6,
                "[불속성 기본공격]을 얻습니다.\r\n데미지: 35" ),
            new SkillRewardInfo(ESkillName.SHOOT_POISON, "기본 공격-독속성" , 6,
                "[독속성 기본공격]을 얻습니다.\r\n데미지: 30" ),
            new SkillRewardInfo(ESkillName.SHOOT_ELECTRIC, "기본 공격-전기속성" , 6,
                "[전기속성 기본공격]을 얻습니다.\r\n데미지: 30" ),
            new SkillRewardInfo(ESkillName.SHOOT_ICE, "기본 공격-얼음속성" , 6,
                "[얼음속성 기본공격]을 얻습니다.\r\n데미지: 30" ),

            new SkillRewardInfo(ESkillName.THROW_NONE, "투척 스킬-무속성" , 6,
                "[투척_무속성]을\r\n얻습니다.\r\n데미지: 40" ),
            new SkillRewardInfo(ESkillName.THROW_FIRE, "투척 스킬-불속성" , 6,
                "[투척_불속성]을\r\n얻습니다.\r\n데미지: 45" ),
            new SkillRewardInfo(ESkillName.THROW_POISON, "투척 스킬-독속성" , 6,
                "[투척_독속성]을\r\n얻습니다.\r\n데미지: 45" ),
            new SkillRewardInfo(ESkillName.THROW_ELECTRIC, "투척 스킬-전기속성" , 6,
                "[투척_전기속성]을\r\n얻습니다.\r\n데미지: 45" ),
            new SkillRewardInfo(ESkillName.THROW_ICE, "투척 스킬-얼음속성" , 6,
                "[투척_얼음속성]을\r\n얻습니다.\r\n데미지: 45" ),

            new SkillRewardInfo(ESkillName.AROUND_NONE, "주변 스킬-무속성", 6,
                "[주변_무속성]을\r\n얻습니다.\r\n데미지: 50"),
            new SkillRewardInfo(ESkillName.AROUND_POISON, "주변 스킬-독속성", 6,
                "[주변_독속성]을\r\n얻습니다.\r\n데미지: 50"),
            new SkillRewardInfo(ESkillName.AROUND_ICE, "주변 스킬-얼음속성", 6,
                "[주변_얼음속성]을\r\n얻습니다.\r\n데미지: 50"),

            new SkillRewardInfo(ESkillName.SECTOR_FIRE, "원뿔 스킬-불속성", 6,
                "[원뿔_불속성]을\r\n얻습니다.\r\n데미지: 50"),
            new SkillRewardInfo(ESkillName.AROUND_ELECTRIC, "주변 스킬-전기속성", 6,
                "[주변_전기속성]을\r\n얻습니다.\r\n데미지: 50"),

            new SkillRewardInfo(ESkillName.BUFF, "버프 스킬", 8,
                "[버프스킬]을\r\n얻습니다."),
            new SkillRewardInfo(ESkillName.SUMMON, "소환 스킬", 8,
                "[소환스킬]을\r\n얻습니다."),
        };

    private int TotalProb = 0;      // 보상 정보들의 확률 합
    private int SkillProb = 0;      // 스킬 보상 확률 합
    private int StatProb = 0;

    public void SelectReward(RewardInfo _reward)                        // 보상 고를 시 실행 함수 (RewardPopupScript에서 호출)
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


    public RewardInfo GetRandomRewardInfo()     // 랜덤 보상 정보 뽑기
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

        Debug.LogError("전체 확률 합 설정 잘못됨");
        return null;
    }

    private void SetTotalProb()                // 확률 합 초기 설정
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

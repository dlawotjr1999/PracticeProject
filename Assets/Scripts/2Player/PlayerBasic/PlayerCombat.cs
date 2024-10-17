using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillKey
{
    ASkill,
    SSkill,
    DSkill,
    LAST
}

public partial class PlayerController
{
    private const int HealAmount = 25;                                  // 포션 회복량 (여기 있는게 이상하긴 함)

    private AimMgr m_aimMgr;                                            // 조준점 관리자

    public ESkillName CurSkill { get; private set; } = ESkillName.SHOOT_NONE;                           // 현재 시전 중인 스킬
    public ESkillKey CurKey { get; private set; } = ESkillKey.LAST;                                     // ㄴ의 키
    public bool IsShootingSkill { get { return (CurSkill <= ESkillName.SHOOT_ICE); } }                  // 시전 중인 스킬이 기본 공격인지

    public ESkillName ShootingSkillType { get; private set; } = ESkillName.SHOOT_NONE;                  // 현재 설정된 기본 공격
    public ESkillName[] CurSkillSlot { get; private set; } = new ESkillName[] 
    {ESkillName.LAST, ESkillName.LAST, ESkillName.LAST };                                               // 현재 설정된 스킬 3개

    private const float MaxShootingCoolTime = 1;                                                        // 기본공격 쿨타임
    private float[] SkillCoolTime { get; set; } = new float[] { 0, 0, 0 };                              // 스킬 3개 쿨타임
    private float CurShootingCoolTime { get; set; }

    public void SkillKeyChk()
    {
        for (int i = 0; i<(int)ESkillKey.LAST; i++) 
        {
            KeyCode chkKey = SkillToKey((ESkillKey)i);
            if (Input.GetKeyDown(chkKey))
            {
                SetCurSkill(chkKey);
            }
        }
    }                               // A,S,D, 입력 체크
    public ESkillKey KeyToSkill(KeyCode _key)
    {
        if (_key == KeyCode.A) return ESkillKey.ASkill;
        else if (_key == KeyCode.S) return ESkillKey.SSkill;
        else if (_key == KeyCode.D) return ESkillKey.DSkill;

        Debug.LogError("잘못된 키 코드 입력"); return ESkillKey.LAST;
    }               // A,S,D => enum 변환
    public KeyCode SkillToKey(ESkillKey _skill)
    {
        if (_skill == ESkillKey.ASkill) return KeyCode.A;
        else if (_skill == ESkillKey.SSkill) return KeyCode.S;
        else if (_skill == ESkillKey.DSkill) return KeyCode.D;

        Debug.LogError("잘못된 스킬 코드 입력"); return KeyCode.None;
    }             // enum => A,S,D 변환

    private void AimOn(ESkillName _skill) { m_aimMgr.AimOn(_skill); }       // 조준점 on
    private void AimOff() { m_aimMgr.AimOff(); }                            // 조준점 off


    private void Heal(int _heal)              // 체력 회복
    {
        if (IsDead) { return; }

        CurHP += _heal;
        if (CurHP > MaxHP)
            CurHP = MaxHP;
        PlayMgr.SetCurHP(CurHP);

        PlayMgr.CreatePlayerEffect(EPlayerEffect.HEALED, transform);
    }

    private void GetDamage(float _damage)      // 데미지 받음
    {
        if (IsDead) { return; }
        if (CurState == m_dash) { return; }
        if (EvadeChk()) { EvadeAction(); return; }

        CurHP -= (int)_damage;
        if (CurHP <= 0 && !IsDead)
        {
            CurHP = 0;
            DiePlayer();
        }
        else if(CurState != m_hit)
        {
            HitPlayer();
        }
        PlayMgr.SetCurHP(CurHP);
    }

    private bool EvadeChk()
    {
        int prob = Random.Range(0, 100);
        if (prob < Evasion)
            return true;
        return false;
    }
    private void EvadeAction()
    {
        GameObject evasionUI = Instantiate(PlayMgr.EvasionUIPreafb, PlayMgr.IngameCanvasTrans);
        evasionUI.GetComponent<IngameTrackUI>().SetPosition(transform.position + Vector3.up * 3);
    }


    public void SelectShooting(ESkillElement _element)                  // 기본 공격 설정
    {
        ESkillName curSkill = ShootingSkillType;
        switch (_element)
        {
            case ESkillElement.NONE:
                ShootingSkillType = ESkillName.SHOOT_NONE;
                break;
            case ESkillElement.FIRE:
                ShootingSkillType = ESkillName.SHOOT_FIRE;
                break;
            case ESkillElement.POISON:
                ShootingSkillType = ESkillName.SHOOT_POISON;
                break;
            case ESkillElement.ELECTRIC:
                ShootingSkillType = ESkillName.SHOOT_ELECTRIC;
                break;
            case ESkillElement.ICE:
                ShootingSkillType = ESkillName.SHOOT_ICE;
                break;
        }
        if (curSkill != ShootingSkillType)
        {
            ResetSkill();
        }
    }


    private void UseSummon(int _key)        // 소환수 소환
    {
        StartSkillCoolTime(_key, SkillValues.GetSkillInfo(ESkillName.SUMMON).CoolTime);     // 쿨타임 적용

    }

    private void UseBuff(int _key)
    {
        PlayMgr.CreatePlayerEffect(EPlayerEffect.BUFF, transform);                          // 이펙트
        StartSkillCoolTime(_key, SkillValues.GetSkillInfo(ESkillName.BUFF).CoolTime);       // 쿨타임
        BuffOn = true;                                                                      // 버프 온
        StartCoroutine(BuffDone());                                                         // 꺼지기 코루틴
    }
    private IEnumerator BuffDone()
    {
        yield return new WaitForSeconds(15);
        BuffOn = false;
    }

    public void ChkNAttack()                                       // 기본 공격 쿨타임 확인 후 공격
    {
        if (CurSkill <= ESkillName.SHOOT_ICE && CurShootingCoolTime > 0) return;
        AttackPlayer();
    }

    public void ResetSkill()                                        // 기본 공격으로 설정
    {
        CurSkill = ShootingSkillType;
        CurKey = ESkillKey.LAST;
        AimOff();
    }
    public void SetCurSkill(KeyCode _key)                           // 키 코드 입력 받아서 스킬 설정
    {
        ESkillName skill = CurSkillSlot[(int)KeyToSkill(_key)];
        if (skill == ESkillName.LAST) return;

        ESkillKey key = KeyToSkill(_key);
        if (CurSkillSlot[(int)key] == ESkillName.LAST || SkillCoolTime[(int)key] > 0) return;

        if (skill == ESkillName.BUFF) { UseBuff((int)key); return; }
        if (skill == ESkillName.SUMMON) { UseSummon((int)key); return; }
        CurSkill = skill;
        CurKey = key;
        AimOn(CurSkill);
    }

    public void SetSkillSlot(ESkillName _skill, int _idx)               // 스킬 슬롯 설정
    {
        if (CurSkillSlot[_idx] != ESkillName.LAST)
            SkillCoolTime[_idx] = SkillValues.GetSkillInfo(_skill).CoolTime;
        else
            SkillCoolTime[_idx] = 0;
        CurSkillSlot[_idx] = _skill;
    }

    private void StartShootingCoolTime()            // 기본 공격 쿨타임
    {
        CurShootingCoolTime = MaxShootingCoolTime * (1 / CurAttackSpeed);
        PlayMgr.StartShootingCoolTime(CurShootingCoolTime);
        StartCoroutine(ShootingCoolTimeProc());
    }
    private IEnumerator ShootingCoolTimeProc()      // 기본 공격 쿨타임
    {
        while (CurShootingCoolTime > 0)
        {
            CurShootingCoolTime -= Time.deltaTime;
            yield return null;
        }
        CurShootingCoolTime = 0;
    }

    private void StartSkillCoolTime(int _idx, float _coolTime)          // 스킬 쿨타임 시작
    {
        float coolTime = _coolTime * (1 / CurAttackSpeed);
        SkillCoolTime[_idx] = coolTime;
        PlayMgr.StartSkillCoolTime(_idx, coolTime);
        StartCoroutine(CoolTimeProc(_idx));
    }
    private IEnumerator CoolTimeProc(int _idx)                          // 쿨타임 감소 코루틴
    {
        while (SkillCoolTime[_idx] > 0)
        {
            SkillCoolTime[_idx] -= Time.deltaTime;
            yield return null;
        }
        AbleSkill(_idx);
    }
    private void AbleSkill(int _idx)                                    // 쿨타임 끝
    {
        SkillCoolTime[_idx] = 0;
        // 쿨타임 끝 이펙트
    }

    public void UseSkill(ESkillName _skill, Vector3 _aim, Vector3 _target, ESkillKey _key)        // 스킬 사용
    {
        GameObject skillObj = Instantiate(PlayMgr.GetSkillPrefab(_skill));
        PlayerSkill script = skillObj.GetComponent<PlayerSkill>();
        SkillInfo info = SkillValues.GetSkillInfo(_skill);
        skillObj.transform.position = transform.position + _aim.normalized * info.Offset + Vector3.up * info.CreationHeight;
        script.SetTarget(_target);
        script.SetSkillInfo(info);
        script.SetAim(_aim);
        script.SetCritical(Critical);

        if(_key != ESkillKey.LAST) { StartSkillCoolTime((int)_key, info.CoolTime); }
        else { StartShootingCoolTime(); }
    }
}
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
    private const int HealAmount = 25;                                  // ���� ȸ���� (���� �ִ°� �̻��ϱ� ��)

    private AimMgr m_aimMgr;                                            // ������ ������

    public ESkillName CurSkill { get; private set; } = ESkillName.SHOOT_NONE;                           // ���� ���� ���� ��ų
    public ESkillKey CurKey { get; private set; } = ESkillKey.LAST;                                     // ���� Ű
    public bool IsShootingSkill { get { return (CurSkill <= ESkillName.SHOOT_ICE); } }                  // ���� ���� ��ų�� �⺻ ��������

    public ESkillName ShootingSkillType { get; private set; } = ESkillName.SHOOT_NONE;                  // ���� ������ �⺻ ����
    public ESkillName[] CurSkillSlot { get; private set; } = new ESkillName[] 
    {ESkillName.LAST, ESkillName.LAST, ESkillName.LAST };                                               // ���� ������ ��ų 3��

    private const float MaxShootingCoolTime = 1;                                                        // �⺻���� ��Ÿ��
    private float[] SkillCoolTime { get; set; } = new float[] { 0, 0, 0 };                              // ��ų 3�� ��Ÿ��
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
    }                               // A,S,D, �Է� üũ
    public ESkillKey KeyToSkill(KeyCode _key)
    {
        if (_key == KeyCode.A) return ESkillKey.ASkill;
        else if (_key == KeyCode.S) return ESkillKey.SSkill;
        else if (_key == KeyCode.D) return ESkillKey.DSkill;

        Debug.LogError("�߸��� Ű �ڵ� �Է�"); return ESkillKey.LAST;
    }               // A,S,D => enum ��ȯ
    public KeyCode SkillToKey(ESkillKey _skill)
    {
        if (_skill == ESkillKey.ASkill) return KeyCode.A;
        else if (_skill == ESkillKey.SSkill) return KeyCode.S;
        else if (_skill == ESkillKey.DSkill) return KeyCode.D;

        Debug.LogError("�߸��� ��ų �ڵ� �Է�"); return KeyCode.None;
    }             // enum => A,S,D ��ȯ

    private void AimOn(ESkillName _skill) { m_aimMgr.AimOn(_skill); }       // ������ on
    private void AimOff() { m_aimMgr.AimOff(); }                            // ������ off


    private void Heal(int _heal)              // ü�� ȸ��
    {
        if (IsDead) { return; }

        CurHP += _heal;
        if (CurHP > MaxHP)
            CurHP = MaxHP;
        PlayMgr.SetCurHP(CurHP);

        PlayMgr.CreatePlayerEffect(EPlayerEffect.HEALED, transform);
    }

    private void GetDamage(float _damage)      // ������ ����
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


    public void SelectShooting(ESkillElement _element)                  // �⺻ ���� ����
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


    private void UseSummon(int _key)        // ��ȯ�� ��ȯ
    {
        StartSkillCoolTime(_key, SkillValues.GetSkillInfo(ESkillName.SUMMON).CoolTime);     // ��Ÿ�� ����

    }

    private void UseBuff(int _key)
    {
        PlayMgr.CreatePlayerEffect(EPlayerEffect.BUFF, transform);                          // ����Ʈ
        StartSkillCoolTime(_key, SkillValues.GetSkillInfo(ESkillName.BUFF).CoolTime);       // ��Ÿ��
        BuffOn = true;                                                                      // ���� ��
        StartCoroutine(BuffDone());                                                         // ������ �ڷ�ƾ
    }
    private IEnumerator BuffDone()
    {
        yield return new WaitForSeconds(15);
        BuffOn = false;
    }

    public void ChkNAttack()                                       // �⺻ ���� ��Ÿ�� Ȯ�� �� ����
    {
        if (CurSkill <= ESkillName.SHOOT_ICE && CurShootingCoolTime > 0) return;
        AttackPlayer();
    }

    public void ResetSkill()                                        // �⺻ �������� ����
    {
        CurSkill = ShootingSkillType;
        CurKey = ESkillKey.LAST;
        AimOff();
    }
    public void SetCurSkill(KeyCode _key)                           // Ű �ڵ� �Է� �޾Ƽ� ��ų ����
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

    public void SetSkillSlot(ESkillName _skill, int _idx)               // ��ų ���� ����
    {
        if (CurSkillSlot[_idx] != ESkillName.LAST)
            SkillCoolTime[_idx] = SkillValues.GetSkillInfo(_skill).CoolTime;
        else
            SkillCoolTime[_idx] = 0;
        CurSkillSlot[_idx] = _skill;
    }

    private void StartShootingCoolTime()            // �⺻ ���� ��Ÿ��
    {
        CurShootingCoolTime = MaxShootingCoolTime * (1 / CurAttackSpeed);
        PlayMgr.StartShootingCoolTime(CurShootingCoolTime);
        StartCoroutine(ShootingCoolTimeProc());
    }
    private IEnumerator ShootingCoolTimeProc()      // �⺻ ���� ��Ÿ��
    {
        while (CurShootingCoolTime > 0)
        {
            CurShootingCoolTime -= Time.deltaTime;
            yield return null;
        }
        CurShootingCoolTime = 0;
    }

    private void StartSkillCoolTime(int _idx, float _coolTime)          // ��ų ��Ÿ�� ����
    {
        float coolTime = _coolTime * (1 / CurAttackSpeed);
        SkillCoolTime[_idx] = coolTime;
        PlayMgr.StartSkillCoolTime(_idx, coolTime);
        StartCoroutine(CoolTimeProc(_idx));
    }
    private IEnumerator CoolTimeProc(int _idx)                          // ��Ÿ�� ���� �ڷ�ƾ
    {
        while (SkillCoolTime[_idx] > 0)
        {
            SkillCoolTime[_idx] -= Time.deltaTime;
            yield return null;
        }
        AbleSkill(_idx);
    }
    private void AbleSkill(int _idx)                                    // ��Ÿ�� ��
    {
        SkillCoolTime[_idx] = 0;
        // ��Ÿ�� �� ����Ʈ
    }

    public void UseSkill(ESkillName _skill, Vector3 _aim, Vector3 _target, ESkillKey _key)        // ��ų ���
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
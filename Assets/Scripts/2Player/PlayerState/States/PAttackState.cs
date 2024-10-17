using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    private ESkillName CurSkill { get; set; }
    private Vector3 CurTarget { get; set; }
    private Vector3 CurAim { get; set; }
    private ESkillKey CurKey { get; set; }

    public void ChangeTo(PlayerController _player)
    {
        if (!m_player) { m_player = _player; }

        m_player.StopMove();

        // �⺻ ���� ����
        CurTarget = m_player.MouseTarget;
        CurAim = m_player.SkillAim;
        m_player.SetRotation(new(CurAim.x, CurAim.z));
        CurSkill = m_player.CurSkill;
        CurKey = m_player.CurKey;

        // ��ų ��� ������ ����
        SkillInfo info = SkillValues.GetSkillInfo(CurSkill);
        float preDelay = info.PreDelay;
        float totalDelay = info.TotalDelay;
        StartCoroutine(UseSkill(preDelay));
        StartCoroutine(ToIdle(totalDelay));

        PlayAttackAnim(info);               // �ִϸ��̼�
        if (!m_player.IsShootingSkill)
            m_player.ResetSkill();          // �⺻���� �ƴϸ� ��ų ���� ����
    }

    private void PlayAttackAnim(SkillInfo _skill)
    {
        switch (_skill.Type)
        {
            case ESkillType.SHOOT:
                m_player.AttackAnim();
                break;
            case ESkillType.THROW:
                m_player.AttackThrowAnim();
                break;
            case ESkillType.RANGE_AROUND:
                m_player.AttackAroundAnim();
                break;
            case ESkillType.RANGE_SECTOR:
                m_player.AttackSectorAnim();
                break;
        }
    }

    private IEnumerator UseSkill(float _preDelay)       // predelay �� ��ų ����
    {
        yield return new WaitForSeconds(_preDelay);
        m_player.UseSkill(CurSkill, CurAim, CurTarget, CurKey);
    }
    private IEnumerator ToIdle(float _totalDelay)       // totaldelay �� ������ ��
    {
        yield return new WaitForSeconds(_totalDelay);
        m_player.IdlePlayer();
    }

    public void Proceed()
    {
        if (Input.GetKeyDown(KeyCode.Space))            // ��ų ��� �� �����̽��� ���� �� ĵ�� ����
        {
            StopAllCoroutines();
            m_player.DashPlayer();
            return;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private void OnTriggerEnter(Collider _other)
    {
        switch (_other.tag)
        {
            case ValueDefine.MonsterSkillTag:
                GetDamage(10);
                break;
            case ValueDefine.SpikeTrapTag:
                GetDamage(25);
                break;
            case ValueDefine.HealingTag:
                Heal(HealAmount);
                break;
        }
    }

    private void OnTriggerStay(Collider _other)
    {
        
    }

    private void OnTriggerExit(Collider _other)
    {
        
    }

    private void OnCollisionEnter(Collision _collision)
    {
        switch (_collision.gameObject.tag)
        {
            case ValueDefine.WallTag:
                IdlePlayer();
                break;
            case ValueDefine.MonsterTag:
                {
                    InCombat = true;
                    // 소환수가 소환중이라면 
                    // 소환수 가져오기
                    // summonSkill.SummonAttack(_collision.gameObject);
                }
                break;
        }
    }

    private void OnCollisionStay(Collision _collision)
    {

    }

    private void OnCollisionExit(Collision _collision)
    {
        
    }
}

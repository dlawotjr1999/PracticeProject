using BossMonster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossMonster
{
    public class BossMonster : MonoBehaviour
    {
        [SerializeField]
        GameObject m_attackTarget;
        [SerializeField]
        GameObject m_attackObj;

        bool OnSkill = false;

        Queue<ESkillName> SkillOrder = new Queue<ESkillName>();

        void Start()
        {
            SkillOrder.Enqueue(ESkillName.RANGE_A);
            SkillOrder.Enqueue(ESkillName.RANGE_B);
            SkillOrder.Enqueue(ESkillName.SUMMON);

            foreach (ESkillName queue in SkillOrder)
            {
                StartCoroutine(Skill_A());
                UseSkill(queue);
            }
        }
        void UseSkill(ESkillName _skillName)
        {
            switch (_skillName)
            {
                case ESkillName.RANGE_A:
                    Skill_A();
                    break;
                case ESkillName.RANGE_B:
                    Skill_B();
                    break;
                case ESkillName.SUMMON:
                    Skill_C();
                    break;
            }
        }


        IEnumerator Skill_A()   // 범위스킬1 [랜덤 원형 마법진 공격]
        {
            Vector3[] attackPos = new Vector3[3];
            attackPos = SetSkill_A();
            foreach (Vector3 pos in attackPos)
            {
                // 공격 생성
                Instantiate(m_attackObj,pos,Quaternion.identity);
            }
            yield return new WaitUntil(() => OnSkill);
        }
        Vector3[] SetSkill_A()
        {
            Vector3[] _attackPos = new Vector3[3];
            do{
                _attackPos[0] = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            } while (Vector3.Distance(_attackPos[0], transform.position) >= 7);
            do{
                _attackPos[1] = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            } while (Vector3.Distance(_attackPos[1], transform.position) >= 7 && Vector3.Distance(_attackPos[0], _attackPos[1])>10);
            do{
                _attackPos[2] = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            } while(Vector3.Distance(_attackPos[2], transform.position) >= 7 && Vector3.Distance(_attackPos[0], _attackPos[2]) > 10 && Vector3.Distance(_attackPos[1],_attackPos[2])>10);
            return _attackPos;
        }

        IEnumerator Skill_B()   // 범위스킬2 [방 전체 즉발 공격]
        {
           // Instantiate() 방 전체 공격
            yield return new WaitUntil(() => OnSkill);
        }
        IEnumerator Skill_C()   // 소환스킬
        {

            yield return new WaitUntil(() => OnSkill);
        }
        IEnumerable SkillTimer(float _lastTime)
        {
            yield return new WaitForSecondsRealtime(_lastTime);
        }
        private void Update()
        {

        }

    }

    public enum ESkillName
    {
        RANGE_A,
        RANGE_B,

        SUMMON,

        LAST
    }

    public class BossMonsterSkill
    {
        // 스킬범위 damage radius predelay totaldelay lasttime cooltime
        public Vector3 SkillRangeFrom { get; private set; }
        public Vector3 SkillRangeTo { get; private set; }
        public float Damage { get; private set; }
        public float Radious { get; private set; }
        public float PreDelay { get; private set; }
        public float TotalDelay { get; private set; }
        public float LastTime { get; private set; }
        public float CoolTime { get; private set; }

        public BossMonsterSkill(Vector3 _skillRangeFrom, Vector3 _skillRangeTo, float _damage, float _radious, float _preDelay,
            float _totalDelay, float _lastTime, float _coolTime)
        {
            SkillRangeFrom = _skillRangeFrom; SkillRangeTo = _skillRangeTo; Damage = _damage; Radious = _radious; PreDelay = _preDelay;
            TotalDelay = _totalDelay; LastTime = _lastTime; CoolTime = _coolTime;
        }
    }
    public class BossMonsterSkill_A : BossMonsterSkill
    {
        public int SkillAmount { get; private set; }
        public float SkillRangeGap { get; private set; }
        public float SkillMonsterGap { get; private set; }
        public BossMonsterSkill_A(Vector3 _skillRangeFrom, Vector3 _skillRangeTo, float _damage, float _radious, float _preDelay,
            float _totalDelay, float _lastTime, float _coolTime)
            : base(_skillRangeFrom, _skillRangeTo, _damage, _radious, _preDelay, _totalDelay, _lastTime, _coolTime)
        { }
    }
    public class BossMonsterSkill_B : BossMonsterSkill
    {
        public BossMonsterSkill_B(Vector3 _skillRangeFrom, Vector3 _skillRangeTo, float _damage, float _radious, float _preDelay,
            float _totalDelay, float _lastTime, float _coolTime)
            : base(_skillRangeFrom, _skillRangeTo, _damage, _radious, _preDelay, _totalDelay, _lastTime, _coolTime)
        { }
    }
    public class BossMonsterSkill_C : BossMonsterSkill
    {
        public BossMonsterSkill_C(Vector3 _skillRangeFrom, Vector3 _skillRangeTo, float _damage, float _radious, float _preDelay,
            float _totalDelay, float _lastTime, float _coolTime)
            : base(_skillRangeFrom, _skillRangeTo, _damage, _radious, _preDelay, _totalDelay, _lastTime, _coolTime)
        { }
    }
    public static class BossMonsterSkillValues
    {
        public static Dictionary<ESkillName, BossMonsterSkill> m_BossSkillInfo = new()
        {
            { ESkillName.RANGE_A, new BossMonsterSkill_A(new Vector3(-30, 0, -30), new Vector3(30, 0, 30), 30f, 10f, 5f, 0, 0, 5f) },
            { ESkillName.RANGE_B, new BossMonsterSkill_B(new Vector3(-30, 0, -30), new Vector3(30, 0, 30), 50, 0, 5f, 0f, 0.2f, 5f) }
        };
    };
}

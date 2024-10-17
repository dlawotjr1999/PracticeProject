using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonSkill : PlayerSkill
{
    // General parameters
    [Header("General parameters")]
    public GameObject m_player;
    public NavMeshAgent m_navAgent;
    public Animator m_animator;
    public GameObject summonAttack;

    // Summon parameters
    [Header("Static Summon parameters")]
    [SerializeField] float maxDistance = 10f;             // max distance between player summon
    [SerializeField] float minDistance = 3f;
    
    [Space(10)]
    [SerializeField] float maxHP = 200f;
    [SerializeField] float damage = 15f;
    [SerializeField] float speed = 5f;

    [Space(10)]
    [SerializeField] float checkEnemyRange = 3f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float attackSpeed = 3f;

    // Combat parameters
    [Header("Summon State")]
    public GameObject target;
    public float currentDistance;
    public float currentHP;
    public bool attackTimer = false;

    public override void SetSkillInfo(SkillInfo _info)
    {
        ShootingSkillInfo info = (ShootingSkillInfo)_info;
        base.SetSkillInfo(info);
    }
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()        // �ʱ�ȭ, �Ҵ�
    {
        currentHP = maxHP;
        m_player = GameObject.Find("PlayerController");
        m_navAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 playerPos = m_player.transform.position;
        currentDistance = Vector3.Distance(transform.position, playerPos);

        SummonAlgorithm();
    }

    public void SummonAlgorithm()
    {
        if (CheckEnemy(m_player.transform.position, checkEnemyRange) != null)            // ĳ���Ͱ� ������ ���� �ν� �ϸ�
        {
            CombatAlgorithm();
        }
        else
        {
            MoveAlgorithm();
        }

        if (currentHP <= 0)                                                     // hp�� 0�� �Ǹ�
        {
            DestroySummon();                                                // ��ȯ ���� ( �״� �ִϸ��̼� )
        }
    }

    public void CombatAlgorithm()
    {
        GameObject enemy = CheckEnemy(m_player.transform.position, checkEnemyRange);
        if (enemy != null)                                              // Ÿ�ٰŸ� < �νĹ���
        {
            target = enemy;                                             // Ÿ�� = �νĹ��� ���� ���� ����� ����
            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            {
                MoveTo(target);                                         // Ÿ������ �̵� (ȸ��, �ִϸ��̼� �߰��ؾ���)                   
            }
            else SummonAttack(target);
        }
    }
    public void MoveAlgorithm()
    {
        if (currentDistance > maxDistance)                                  //  ĳ���Ϳ��� �Ÿ��� �ִ�Ÿ����� �ָ�
        {
            TeleportTo(m_player);                                       // �����̵�
        }
        if (currentDistance > minDistance)
        {
            MoveTo(m_player);                                           // �̵� ( �̵� �ִϸ��̼� )
        }
    }

    public GameObject CheckEnemy(Vector3 _checkAround, float _chkEnemyDis)
    {
        Collider[] colls = Physics.OverlapSphere(_checkAround, _chkEnemyDis);
        foreach (Collider e in colls)
        {
            if (e.CompareTag(ValueDefine.MonsterTag))
            {
                return e.gameObject;
            }
        }

        return null;
    }
    IEnumerator AttackTimer()
    {
        if (attackTimer)
        {
            yield return new WaitForSeconds(attackSpeed);
            attackTimer = false;
        }
    }
    IEnumerator IsAttacking()
    {
        yield return new WaitForSeconds(1f);
        summonAttack.SetActive(false);
    }
    public void SummonAttack(GameObject _target)    // ��������
    {
        if (!attackTimer)
        {
            attackTimer = true;
            // Ÿ�� �������� ȸ��
            TurnTo(_target);
            m_animator.SetTrigger("Attack1");           // ���� �ִϸ��̼� Ʈ����
            summonAttack.SetActive(true);               // ���� �ڵ�
            StartCoroutine(IsAttacking());
            StartCoroutine(AttackTimer());              // ���� ��Ÿ��
        }
    }
    private void MoveTo(GameObject _target)
    {
        m_navAgent.SetDestination(_target.transform.position);
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Run Forward", true);
        StartCoroutine(CheckArrived(m_navAgent, minDistance));
    }
    private void TurnTo(GameObject _target)
    {
        transform.LookAt(_target.transform);
    }
    private void TeleportTo(GameObject _target)
    {
        transform.position = _target.transform.position + new Vector3(-2, 0, 0);
    }
    private void DestroySummon()
    {
        Destroy(gameObject);
    }
    IEnumerator CheckArrived(NavMeshAgent _navAgent, float _minDistance)
    {
        yield return new WaitUntil(() => currentDistance <= _minDistance);
        //_navAgent.ResetPath();
        m_animator.SetBool("Run Forward", false);
        m_animator.SetBool("Idle", true);
    }
}

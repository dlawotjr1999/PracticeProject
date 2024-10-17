using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class MonsterScript : MonoBehaviour
{
    private SpawnRoom m_parent;                                                 // 소환된 방 관리자
    public void SetParent(SpawnRoom _parent) { m_parent = _parent; }            // 관리자 설정

    [SerializeField]
    protected EMonsterName m_name;                                              // 몬스터 이름 (프리펍에 설정)

    private NavMeshAgent m_navAgent;                                            // 네비매쉬
    protected Animator m_anim;                                                    // 애니메이터
    private MonsterHPBar m_hpBar;                                                 // 체력바
    private MonsterStateMgr m_stateMgr;                                        // 상태 관리자

    protected IMonsterState m_idle, m_roaming, m_approach, m_attack, m_hit, m_die;// 상태 클래스들
    public IMonsterState CurState { get { return m_stateMgr.CurState; } }       // 현재 상태

    public void IdleAnim() { m_anim.SetTrigger("IDLE"); }               // IDLE 애니메이션
    public void MoveAnim() { m_anim.SetTrigger("MOVE"); }               // ROAMING, APPROACH 애니메이션
    public void AttackAnim() { m_anim.SetTrigger("ATTACK"); }           // ATTACK 애니메이션
    public void HitAnim() { m_anim.SetTrigger("HIT"); }                 // HIT 애니메이션
    public void DieAnim() { m_anim.SetTrigger("DIE"); }                 // DIE 애니메이션
    public float DieAnimLength { get; protected set; }                  // ㄴ의 길이

    public void IdleMonster() { m_stateMgr.ChangeTo(m_idle); }                  // IDLE로
    public void RoamingMonster() { m_stateMgr.ChangeTo(m_roaming); }            // ROAMING으로
    public virtual void ApproachMonster() { m_stateMgr.ChangeTo(m_approach); }          // APPROACH로
    public void AttackMonster() { m_stateMgr.ChangeTo(m_attack); }              // ATTACK으로
    public void AttackDefenseMonster() { m_stateMgr.AttackDefense(m_attack); }  // ATTACK인데 방어 대상 공격
    public void HitMonster() { m_stateMgr.ChangeTo(m_hit); }                    // HIT로
    public void DieMonster() { m_stateMgr.ChangeTo(m_die); }                    // DIE로


    private int MaxHP { get; set; }                                             // 최대 HP
    public float Damage { get; set; }                                           // 데미지
    private float RoamingSpeed { get; set; }                                    // 평상시 속도
    private float ApproachSpeed { get; set; }                                   // 플레이어에게 접근 시 속도
    public float AttackRange { get; private set; }                              // 공격 범위
    private float DetectRange { get; set; }                                     // 감지 범위
    public float AttackSpeed { get; private set; }                              // 공격 속도
    private float CriticalRate { get; set; }                                    // 치명타율
    private float EvasionRate { get; set; }                                     // 회피율

    public int CurHP { get; set; }                                              // 현재 HP
    private float CurSpeed { set { m_navAgent.speed = value; } }                // 현재 속도 (네비에게 속도 설정하는 용도)
    private bool IsDead { get; set; }                                           // 살아있는지

    private const float ArriveDist = 0.25f;                                     // 도착 판정 거리
    // 도착 여부
    public bool Arrived { get { if (m_navAgent.pathPending) return false; if (m_navAgent.remainingDistance >= ArriveDist) return false; return true; } }


    private float DistToPlayer { get { return (PlayMgr.PlayerPos - transform.position).magnitude; } }           // 플레이어와 거리
    public bool Detecting { get { return DistToPlayer <= DetectRange; } }                                      // 감지 중
    private bool PrevDetecting { get; set; }                                                                    // 이전 프레임 감지 여부

    public float AttackCoolCnt { get; set; } = 0;                                                               // 공격 쿨타임
    public bool CanAttack { get { if (DistToPlayer <= AttackRange) return true; return false; } }               // 공격 사정거리 이내


    private bool Aranging { get; set; }                                         // 정리 중인지
    public void ArangeOn() { Aranging = true; Aranged = false; }                // 정리 on (정리 방에서 생성됨)

    public bool Defensing { get; private set; }                                 // 방어전 방인지
    public void DefenseOn()                                                     // 방어전 on (방어전 방에서 생성됨)
    {
        Defensing = true;
        m_roaming = gameObject.AddComponent<MDefenseState>();
    }



    public void SetDestination(Vector3 _destination)    // 목적지 설정
    {
        m_navAgent.SetDestination(_destination);
    }
    public void StopMove()                              // 움직임 멈추기
    {
        m_navAgent.ResetPath();                         // 경로 초기화
        m_navAgent.velocity = Vector3.zero;             // 속도 = 0
    }

    public void SetSpeed(bool _romaing)                 // 로밍, 접근 속도 설정
    {
        if(_romaing) { CurSpeed = RoamingSpeed; }
        else { CurSpeed = ApproachSpeed; }
    }

    public void SetDead() 
    {
        m_hpBar.DestroyUI();
        IsDead = true; 
        if(m_parent) m_parent.MonsterDead(this); 
    }            // 사망 설정

    public void WatchPlayer()                           // 플레이어 쳐다보기
    {
        Vector3 toPlayer = PlayMgr.PlayerPos - transform.position;
        float deg = FunctionDefine.VecToDeg(new(toPlayer.x, toPlayer.z));
        transform.eulerAngles = new(0, deg, 0);
    }

    private void ChkDetect()                            // 플레이어 접근 확인
    {
        if (PrevDetecting == Detecting) return;

        if (Detecting)
            ApproachMonster();
        else
            IdleMonster();
    }



    private void SetAttackCool()
    {
        if (AttackCoolCnt <= 0)
            return;

        AttackCoolCnt -= Time.deltaTime;
        if (AttackCoolCnt < 0)
            AttackCoolCnt = 0;
    }

    private void OnTriggerEnter(Collider _other)
    {
        switch (_other.tag)
        {
            case ValueDefine.PlayerSkillTag:
                PlayerSkill skill = _other.gameObject.GetComponent<PlayerSkill>();
                float damage = skill.Damage;
                GetDamage(damage);              // 임시 데미지 받기 함수
                break;
        }
    }

    public virtual void SetInfos()                      // MonsterValues로부터 기본 정보 설정
    {
        IsDead = false;
        MonsterInfo myInfo = MonsterValues.GetMonsterInfo(m_name);
        MaxHP = myInfo.MaxHP;
        Damage = myInfo.Damage;
        RoamingSpeed = myInfo.RoamingSpeed;
        ApproachSpeed = myInfo.ApproachSpeed;
        AttackRange = myInfo.AttackRange;
        DetectRange = myInfo.DetectRange;
        AttackSpeed = myInfo.AttackSpeed;
        CriticalRate = myInfo.CriticalRate;
        EvasionRate = myInfo.EvasionRate;
    }

    public virtual void SetStates()                     // 상태 인터페이스들 설정
    {
        m_idle = gameObject.AddComponent<MIdleState>();
        m_roaming = gameObject.AddComponent<MRoamingState>();
        m_approach = gameObject.AddComponent<MApproachState>();
        m_attack = gameObject.AddComponent<MAttackState>();
        m_hit = gameObject.AddComponent<MHitState>();
        m_die = gameObject.AddComponent<MDieState>();
    }

    public virtual void CreateHPBar()                   // HP바 생성
    {
        GameObject HPBar = Instantiate(PlayMgr.MonsterHPBarPrefab, PlayMgr.IngameCanvasTrans);
        m_hpBar = HPBar.GetComponent<MonsterHPBar>();
        m_hpBar.SetTrack(transform);
        m_hpBar.InitHPBar(MaxHP);
    }

    public virtual void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_anim = GetComponentInChildren<Animator>();
        m_stateMgr = new MonsterStateMgr(this);
        SetInfos();
        SetStates();
    }

    public virtual void Start()
    {
        CurHP = MaxHP;
        CurSpeed = RoamingSpeed;
        IdleMonster();                  // IDLE로
        CreateHPBar();
    }

    public virtual void Update()
    {
        if (!IsDead)             // 죽은 상태가 아니면
        {
            ChkDetect();        // DETECT 설정
            SetAttackCool();    // 공격 쿨타임 설정
        }
        CurState.Proceed();     // 현재 상태의 Proceed 함수 업데이트 (사망도 여기에 포함되므로 isdead 체크 안함)
    }

    private void LateUpdate()
    {
        if(!IsDead)
            PrevDetecting = Detecting;      // PrevDetecting 설정
    }
}
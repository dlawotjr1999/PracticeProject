using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpawnRoom : GameRoomScript
{
    private SpawnAreaScript[] m_spawnAreas;

    protected List<MonsterScript> m_monsterList = new();          // ?????? ?????? ??????


    private bool Spawned { get; set; } = false;

    public override void EnterRoom()
    {
        base.EnterRoom();
        if (!Spawned)
        {
            SpawnMonster();
        }
    }

    public virtual void SpawnMonster()                  // ?????? ????
    {
        for (int i = 0; i<m_spawnAreas.Length; i++)
        {
            SpawnMonster(i);
        }
        Spawned = true;
    }

    public virtual void SpawnMonster(int _idx)
    {
        SpawnAreaScript area = m_spawnAreas[_idx];

        ESpawnType type = area.SpawnType;           // 소환하는 몬스터 타입
        List<GameObject> curMonster = new();

        switch (type)                   // 종류에 따른 몬스터 추가
        {
            case ESpawnType.MINI:
                for (int i = 0; i<3; i++)
                {
                    EMonsterName mini;
                    do
                    {
                        mini = (EMonsterName)Random.Range((int)EMonsterName.MIMIC, (int)EMonsterName.CAULDRON);
                    } while (mini == EMonsterName.SPIDER || mini == EMonsterName.ELEMENTAL);
                    GameObject miniMon = PlayMgr.GetMonsterPrefab(mini);
                    curMonster.Add(miniMon);
                }
                break;
            case ESpawnType.SPIDER:
                GameObject group = Instantiate(PlayMgr.GroupMonsterPrefab);
                GroupMonsterScript groupScript = group.GetComponent<GroupMonsterScript>();
                for (int i = 0; i<4; i++)
                {
                    GameObject spider = PlayMgr.GetMonsterPrefab(EMonsterName.SPIDER);
                    CreateMonster(spider, area, groupScript);
                }
                break;
            case ESpawnType.MID:
                EMonsterName mid = (EMonsterName)Random.Range((int)EMonsterName.CAULDRON, (int)EMonsterName.GHOST);
                GameObject midMon = PlayMgr.GetMonsterPrefab(mid);
                curMonster.Add(midMon);
                break;
            case ESpawnType.CLOCK:
                GameObject clock = PlayMgr.GetMonsterPrefab(EMonsterName.CLOCK);
                curMonster.Add(clock);
                break;
            case ESpawnType.CAULDRON:
                GameObject cauldron = PlayMgr.GetMonsterPrefab(EMonsterName.CAULDRON);
                curMonster.Add(cauldron);
                break;
            case ESpawnType.GOLEM:
                GameObject golem = PlayMgr.GetMonsterPrefab(EMonsterName.GOLEM);
                curMonster.Add(golem);
                break;
            default:
                Debug.LogError("스폰 구역 타입 설정 안됨");
                break;
        }

        if (type != ESpawnType.SPIDER)
        {
            foreach (GameObject mons in curMonster)
            {
                CreateMonster(mons, area);         // 추가된 몬스터들 설정
            }
        }
        area.SpawnDone();                   // 소환 완료
    }

    private MonsterScript CreateMonster(GameObject _mons, SpawnAreaScript _area)                         // 몬스터 개별 설정
    {
        Vector3 pos = _area.GetRandomPos();
        GameObject mons = Instantiate(_mons, pos, Quaternion.identity);
        _mons.transform.eulerAngles = new(0, Random.Range(0, 360f), 0);                         // 랜덤 방향 바라보기

        MonsterScript monsSciprt = mons.GetComponent<MonsterScript>();
        m_monsterList.Add(monsSciprt);                                                          // 리스트에 추가
        monsSciprt.SetParent(this);                                                             // 몬스터 관리자 설정
        return monsSciprt;
    }
    private void CreateMonster(GameObject _mons, SpawnAreaScript _area, GroupMonsterScript _group)      // 거미 전용
    {
        MonsterScript spider = CreateMonster(_mons, _area);
        _group.AddMonster(spider);
        ((SpiderMonster)spider).SetGroup(_group);
    }

    public void RegisterElemental(GroupMonsterScript _group)                                    // 엘리멘탈 등록용
    {
        foreach (MonsterScript monster in _group.MonsterList)
        {
            m_monsterList.Add(monster);
            monster.SetParent(this);
        }
    }


    public virtual void MonsterDead(MonsterScript _monster)
    {
        m_monsterList.Remove(_monster);
        if (m_monsterList.Count == 0)
        {
            NormalClearRoom();
        }
    }

    public override void Awake()
    {
        base.Awake();
        m_spawnAreas = GetComponentsInChildren<SpawnAreaScript>();
    }
}

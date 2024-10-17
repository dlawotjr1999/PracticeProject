using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESpawnType
{
    MINI,
    SPIDER,
    MID,
    CLOCK,
    CAULDRON,
    GOLEM,
    LAST
}

public class SpawnAreaScript : MonoBehaviour
{
    [SerializeField]
    private ESpawnType m_spawnType;
    public ESpawnType SpawnType { get { return m_spawnType; } }

    [SerializeField]
    private float m_spawnRange;                     // 소환 범위
    public float SpawnRange { get { return m_spawnRange; } }

    private List<Vector2> m_spawnedPos = new();     // 좌표 충돌 체크용 리스트

    private const float ChkDist = 2;                // 충돌 판정 거리

    private bool ChkPos(Vector2 _pos)               // 충돌 확인 함수
    {
        foreach (Vector2 pos in m_spawnedPos)
        {
            if (Vector2.Distance(_pos, pos) < ChkDist)
                return false;
        }
        return true;
    }

    public Vector3 GetRandomPos()                   // 충돌하지 않는 좌표 반환
    {
        Vector2 pos;
        do
        {
            float deg = Random.Range(0, 360f);
            float dist = Random.Range(0, m_spawnRange);
            pos = new(Mathf.Sin(deg), Mathf.Cos(deg));
            pos *= dist;
        } while (!ChkPos(pos));
        Vector3 offset = new(pos.x, 0, pos.y);
        return transform.position + offset;
    }

    public void SpawnDone()                         // 충돌 판정 리스트 초기화
    {
        m_spawnedPos.Clear();
    }
}

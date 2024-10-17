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
    private float m_spawnRange;                     // ��ȯ ����
    public float SpawnRange { get { return m_spawnRange; } }

    private List<Vector2> m_spawnedPos = new();     // ��ǥ �浹 üũ�� ����Ʈ

    private const float ChkDist = 2;                // �浹 ���� �Ÿ�

    private bool ChkPos(Vector2 _pos)               // �浹 Ȯ�� �Լ�
    {
        foreach (Vector2 pos in m_spawnedPos)
        {
            if (Vector2.Distance(_pos, pos) < ChkDist)
                return false;
        }
        return true;
    }

    public Vector3 GetRandomPos()                   // �浹���� �ʴ� ��ǥ ��ȯ
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

    public void SpawnDone()                         // �浹 ���� ����Ʈ �ʱ�ȭ
    {
        m_spawnedPos.Clear();
    }
}

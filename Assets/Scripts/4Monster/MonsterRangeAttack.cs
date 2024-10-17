using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeAttack : MonoBehaviour
{
    [SerializeField]
    private float DestroyTime = 1;

    private void OnTriggerEnter(Collider _other)
    {
        switch (_other.tag)
        {
            case ValueDefine.WallTag:
            case ValueDefine.PlayerTag:
            case ValueDefine.DefenseObjTag:
            case ValueDefine.CollideObjTag:
                Destroy(gameObject);
                break;
        }
    }

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float DestroyTime = 0.25f;

    private void OnTriggerEnter(Collider _other)
    {
        switch (_other.tag)
        {
            case ValueDefine.PlayerTag:
                Destroy(gameObject);
                break;
        }
    }

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}

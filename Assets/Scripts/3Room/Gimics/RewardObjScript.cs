using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObjScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag(ValueDefine.PlayerTag))
        {
            PlayMgr.PopupReward(true);
            Destroy(gameObject);
        }
    }
}
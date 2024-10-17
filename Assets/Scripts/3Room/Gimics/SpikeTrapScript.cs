using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    private Animator m_anim;

    private const float OnTime = 1;
    private const float OffTime = 2;


    private IEnumerator TrapOff()
    {
        m_anim.SetTrigger("DOWN");
        yield return new WaitForSeconds(OffTime);
        StartCoroutine(TrapOn());
    }
    private IEnumerator TrapOn()
    {
        m_anim.SetTrigger("UP");
        yield return new WaitForSeconds(OnTime);
        StartCoroutine(TrapOff());
    }


    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(TrapOn());
    }
}

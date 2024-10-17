using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : GameRoomScript
{
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(TempClear());
    }

    private IEnumerator TempClear()
    {
        yield return new WaitForSeconds(1);
        NormalClearRoom();
    }
}

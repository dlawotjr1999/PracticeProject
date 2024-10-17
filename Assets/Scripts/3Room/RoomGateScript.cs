using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGateScript : MonoBehaviour
{
    private GameRoomScript m_parent;        // ���� �Ҽӵ� ��
    public void SetParent(GameRoomScript _parent) { m_parent = _parent; }       // �� ����

    [SerializeField]
    private GameObject m_door;              // �� ������Ʈ

    private const float OpenAngle = 90;     // ������ �� ����
    private const float OpenTime = 1;       // �����µ� �ɸ��� �ð�

    public void OpenDoor()
    {
        StartCoroutine(OpenCoroutine());
    }
    private IEnumerator OpenCoroutine()
    {
        while(m_door.transform.localEulerAngles.y < OpenAngle)
        {
            RotateDoor(1);
            yield return null;
        }
    }

    public void CloseDoor()
    {
        StartCoroutine(CloseCoroutine());
    }
    private IEnumerator CloseCoroutine()
    {
        while(m_door.transform.localEulerAngles.y > 0)
        {
            RotateDoor(-1);
            yield return null;
        }
    }

    private void RotateDoor(int _rot)
    {
        Vector3 rotation = m_door.transform.localEulerAngles;
        rotation.y += (OpenAngle * _rot / OpenTime) * Time.deltaTime;
        if (rotation.y >= OpenAngle)
            rotation.y = OpenAngle;
        else if (rotation.y <= 0)
            rotation.y = 0;
        m_door.transform.localEulerAngles = rotation;
    }
}

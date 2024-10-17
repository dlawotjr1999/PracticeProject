using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameTrackUI : MonoBehaviour
{
    private RectTransform m_rect;
    private Transform TrackTrans { get; set; }
    private Vector3 Position { get; set; }
    public void SetTrack(Transform _transform) { TrackTrans = _transform; }
    public void SetPosition(Vector3 _position) { Position = _position; }

    public void DestroyUI()
    {
        Destroy(gameObject);
    }

    public virtual void TrackPos(Vector3 _pos)
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(_pos);
        m_rect.position = pos;
    }

    public virtual void Awake()
    {
        m_rect = GetComponent<RectTransform>();
    }

    public virtual void Start()
    {
        if (TrackTrans != null) TrackPos(TrackTrans.position);
        else TrackPos(Position);
    }

    public virtual void Update()
    {
        if (TrackTrans != null) TrackPos(TrackTrans.position);
        else TrackPos(Position);
    }
}

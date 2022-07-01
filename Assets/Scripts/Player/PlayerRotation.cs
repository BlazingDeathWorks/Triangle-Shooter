using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    public float Angle { get; private set; } = 0;
    [SerializeField] Camera _camera = null;
    Transform _transform = null;
    Vector2 _mousePos;

    private void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        //Rotation
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Direction = _mousePos - (Vector2)_transform.position;
        Angle = Mathf.Rad2Deg * Mathf.Atan2(Direction.y, Direction.x);

        _transform.localEulerAngles = new Vector3(0, 0, Angle - 90);
    }
}

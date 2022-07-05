using System;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    [SerializeField] private ActionChannel _leveledUpEventHandler;
    [SerializeField] Camera _camera = null;
    [SerializeField] LineRenderer _lineRenderer = null;
    private static Transform[] _corners = new Transform[2];
    private static float lineBoundsOffset = -0.106f;
    private Vector3 _screenVector => _camera.ScreenToWorldPoint(_screenVectorRaw);
    private Vector3 _screenVectorRaw = new Vector3(Screen.width, Screen.height, 0);
    private EdgeCollider2D _edgeCollider = null;
    private Vector2[] _boundPoints;
    private Vector3 _originalTopCornerPos;
    private const int LEVELS_TO_UPGRADE = 3;
    private int _levelsUntilUpgrade = 3;

    private void Awake()
    {
        _levelsUntilUpgrade = LEVELS_TO_UPGRADE;
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _leveledUpEventHandler?.AddAction(UpgradeBounds);
        SetCorners();
        SetCornerPositions();
        SetBoundPoints();
        _originalTopCornerPos = _corners[0].position;
    }

    private void SetCorners()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            _corners[i] = child;
            i++;
        }
    }

    private void SetCornerPositions()
    {
        _corners[0].position = _screenVector + new Vector3(-0.1f, 0.2f);
        _corners[1].position = _screenVector * -1 + new Vector3(0.1f, -0.2f);
    }

    private void SetBoundPoints()
    {
        _boundPoints = new Vector2[]
        {
            new Vector2(_corners[0].position.x, _corners[1].position.y),
            new Vector2(_corners[1].position.x, _corners[1].position.y),
            new Vector2(_corners[1].position.x, _corners[0].position.y),
            new Vector2(_corners[0].position.x, _corners[0].position.y),
            new Vector2(_corners[0].position.x, _corners[1].position.y),
        };
        _edgeCollider.points = _boundPoints;
        DisplayLines();

        #region Display Lines
        void DisplayLines()
        {
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                _lineRenderer.SetPosition(i, _boundPoints[i]);
            }
            var endBoundPos = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, new Vector3(endBoundPos.x, endBoundPos.y + lineBoundsOffset));
        }
        #endregion
    }

    private void DoubleTopCornerBounds()
    {
        _corners[0].position += _originalTopCornerPos;
        _corners[0].position = new Vector3((float)Math.Round(_corners[0].position.x, 8), (float)Math.Round(_corners[0].position.y, 8));
        SetBoundPoints();
    }

    private void UpgradeBounds()
    {
        if (--_levelsUntilUpgrade > 0) return;
        _levelsUntilUpgrade = LEVELS_TO_UPGRADE;
        DoubleTopCornerBounds();
    }

    public static Vector3 GetCornerPosition(int index)
    {
        if (index >= _corners.Length) return new Vector3(0, 0, 0);
        return _corners[index].position;
    }
}

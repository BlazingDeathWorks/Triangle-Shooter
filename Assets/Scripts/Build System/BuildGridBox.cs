using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridBox : MonoBehaviour
{
    //Variables
    public Color GridColor => _sr.color;
    private bool _rightMouseHeld = false;
    private bool _canSpawnBlock = false;

    //Events
    [SerializeField] private ActionChannel _rightMouseUpEventHandler;

    //References
    public IObjectPoolable<NormalPoolableObject> CurrentBuildBlockGhost { private get; set; }
    private BuildGridBoxManager _buildGridBoxManager;
    private Transform _gridContainer;
    private Transform _transform;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _transform = transform;
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gridContainer = SceneReferenceManager.GetReference("Grid Container").transform;
        _buildGridBoxManager = SceneReferenceManager.GetReference("Build Grid Box Manager").GetComponent<BuildGridBoxManager>();
    }

    private void Update()
    {
        _rightMouseHeld = Input.GetKey(KeyCode.Mouse1);
        if (!Input.GetKeyUp(KeyCode.Mouse1) || _rightMouseUpEventHandler == null) return;
        _rightMouseUpEventHandler.CallAction();
    }

    private void OnMouseOver()
    {
        if (CurrentBuildBlockGhost == null) _buildGridBoxManager.PoolBlockGhost(this, _transform.position);
        if (_rightMouseHeld && _canSpawnBlock == false)
        {
            _canSpawnBlock = true;
            _rightMouseUpEventHandler.AddAction(OnRightMouseUp);
        }
    }

    private void OnMouseExit()
    {
        if (_rightMouseHeld || CurrentBuildBlockGhost == null) return;
        ObjectPool.Return(CurrentBuildBlockGhost);
        CurrentBuildBlockGhost = null;
        _canSpawnBlock = false;
        _rightMouseUpEventHandler.RemoveAction(OnRightMouseUp);
    }

    private void OnRightMouseUp()
    {
        ObjectPool.Return(CurrentBuildBlockGhost);
        CurrentBuildBlockGhost = null;
        _canSpawnBlock = false;
        _rightMouseUpEventHandler.RemoveAction(OnRightMouseUp);
    }
}

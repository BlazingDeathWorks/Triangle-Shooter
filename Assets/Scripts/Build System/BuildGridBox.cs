using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridBox : MonoBehaviour, IObjectPooler<NormalPoolableObject>
{
    //Object Pooler Params
    public NormalPoolableObject Prefab => _buildBlockGhost;
    public Queue<NormalPoolableObject> Pool { get; } = new Queue<NormalPoolableObject>();
    [SerializeField] private NormalPoolableObject _buildBlockGhost;

    //Variables
    public Color GridColor => _sr.color;
    private bool _rightMouseHeld = false;
    private bool _canSpawnBlock = false;

    //Events
    [SerializeField] private ActionChannel _rightMouseUpEventHandler;

    //References
    public IObjectPoolable<NormalPoolableObject> CurrentBuildBlockGhost { get; set; }
    private Transform _gridContainer;
    private Transform _buildBlockGhostContainer;
    private SpriteRenderer _sr;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gridContainer = SceneReferenceManager.GetReference("Grid Container").transform;
        _buildBlockGhostContainer = SceneReferenceManager.GetReference("Build Block Ghost Container").transform;
    }

    private void Update()
    {
        _rightMouseHeld = Input.GetKey(KeyCode.Mouse1);
        if (!Input.GetKeyUp(KeyCode.Mouse1) || _rightMouseUpEventHandler == null) return;
        _rightMouseUpEventHandler.CallAction();
    }

    private void OnMouseOver()
    {
        if (CurrentBuildBlockGhost == null) ObjectPool.Pool(this);
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

    public void OnPooled(NormalPoolableObject instance)
    {
        if (!_buildBlockGhostContainer) return;
        instance.gameObject.SetActive(true);
        CurrentBuildBlockGhost = instance;
        instance.transform.parent = _buildBlockGhostContainer;
        instance.transform.position = _transform.position;
    }
}

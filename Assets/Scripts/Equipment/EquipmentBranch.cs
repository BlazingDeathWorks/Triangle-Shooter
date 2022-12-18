using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentBranch<T> : Equipment where T : EquipmentBullet<T>
{
    [SerializeField] private string _sceneReferenceKey;
    protected override string SceneReferenceKey => _sceneReferenceKey;

    [SerializeField] private Vector2[] _firePoints;
    [SerializeField] [Tooltip("-1: Shoots to the right\n1: Shoots to the left")] private int[] _offsetFactor;
    [SerializeField] private float _fireRate = 5;
    private List<EquipmentBullet<T>> _bulletInstances = new List<EquipmentBullet<T>>();
    private EquipmentLauncher<T> _launcher;
    private PlayerRotation _playerRotation;
    private float _timeSinceLastFire;
    private int _redeployTime;
    private ScaleTween _scaleTween;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _scaleTween = GetComponent<ScaleTween>();
        _redeployTime = (int)_fireRate / 2;
    }

    protected override void Start()
    {
        base.Start();
        _playerRotation = SceneReferenceManager.GetReference(PLAYER).GetComponent<PlayerRotation>();
        _launcher = SceneReferenceManager.GetReference("Rocket Launcher").GetComponent<EquipmentLauncher<T>>();
        Deploy();
    }

    private void Update()
    {
        _timeSinceLastFire += Time.deltaTime;
        if (_timeSinceLastFire >= _fireRate)
        {
            _timeSinceLastFire = 0;
            for (int i = 0; i < _bulletInstances.Count; i++)
            {
                if (i <= _offsetFactor.Length - 1)
                {
                    _bulletInstances[i].ReleaseEquipmentBulletPerpendicular(_playerRotation, _offsetFactor[i]);
                    continue;
                }
                _bulletInstances[i].ReleaseEquipmentBullet(_playerRotation);
            }
            _bulletInstances.Clear();
            _scaleTween.ScaleObject();
            StartCoroutine(ReDeploy());
        }
    }

    private IEnumerator ReDeploy()
    {
        yield return new WaitForSeconds(_redeployTime);
        Deploy();
    }

    private void Deploy()
    {
        for (int i = 0; i < _firePoints.Length; i++)
        {
            _launcher.DeployRocket(_transform, _firePoints[i]);
            _bulletInstances.Add(_launcher.GetInstance());
        }
    }
}

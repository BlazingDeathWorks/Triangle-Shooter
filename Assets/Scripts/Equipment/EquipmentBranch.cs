using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentBranch<T> : Equipment where T : EquipmentBullet<T>
{
    [SerializeField] private string _sceneReferenceKey;
    protected override string SceneReferenceKey => _sceneReferenceKey;

    [SerializeField] private Vector2 _firePoint = new Vector2(0.22f, 0.495f);
    [SerializeField] private float _fireRate = 5;
    private EquipmentBullet<T> _bulletInstance;
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
        _launcher.DeployRocket(_transform, _firePoint);
        _bulletInstance = _launcher.GetInstance();
    }

    private void Update()
    {
        _timeSinceLastFire += Time.deltaTime;
        if (_timeSinceLastFire >= _fireRate)
        {
            _timeSinceLastFire = 0;
            _bulletInstance.ReleaseEquipmentBullet(_playerRotation);
            _scaleTween.ScaleObject();
            StartCoroutine(ReDeploy());
        }
    }

    private IEnumerator ReDeploy()
    {
        yield return new WaitForSeconds(_redeployTime);
        _launcher.DeployRocket(_transform, _firePoint);
        _bulletInstance = _launcher.GetInstance();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RocketBranch : EquipmentBranch
{
    [SerializeField] private RocketLauncher _launcher;
    [SerializeField] private Vector2 _firePoint;
    [SerializeField] private float _fireRate = 30;
    private RocketHead _rocketHeadInstance;
    private ScaleTween _scaleTween;
    private Transform _transform;
    private float _timeSinceLastFire;

    private void Awake()
    {
        _transform = transform;
        _scaleTween = GetComponent<ScaleTween>();
        _launcher.DeployRocket(_transform, _firePoint);
        _rocketHeadInstance = _launcher.GetInstance();
    }

    private void Update()
    {
        _timeSinceLastFire += Time.deltaTime;
        if (_timeSinceLastFire >= _fireRate)
        {
            _timeSinceLastFire = 0;
            //Shoot
            _scaleTween.ScaleObject();
            StartCoroutine(ReDeploy());
        }
    }

    private IEnumerator ReDeploy()
    {
        yield return new WaitForSeconds(2f);
        _launcher.DeployRocket(_transform, _firePoint);
    }
}
